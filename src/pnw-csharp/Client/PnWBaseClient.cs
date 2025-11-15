using RestSharp;
using PnW.Models;
using System.Reflection;

namespace PnW.Client
{

    public abstract class PnWBaseClient
    {
        public int? RateLimitRemaining { get; private set; } // make private somehow?

        protected readonly RestClient _client;
        protected readonly string GraphQLUrl;

        protected PnWBaseClient(string apiKey, string? botKey = null, string? botKeyApiKey = null)
        {
            GraphQLUrl = $"https://api.politicsandwar.com/graphql?api_key={apiKey}";
            _client = new RestClient(GraphQLUrl);

            if (!string.IsNullOrEmpty(botKey))
                _client.AddDefaultHeader("x-bot-key", botKey);

            if (!string.IsNullOrEmpty(botKeyApiKey))
                _client.AddDefaultHeader("x-api-key", botKeyApiKey);
        }

        protected string LoadResourceQuery(string resourceName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceFullName = $"PnW.Resources.{resourceName}.graphql";

            using Stream? stream = assembly.GetManifestResourceStream(resourceFullName);

            if (stream == null)
            {
                throw new InvalidOperationException($"Could not find embedded resource: {resourceFullName}.");
            }

            using var reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }

        protected async Task<T> ExecuteGraphQLQueryAsync<T>(string query, string queryType, string id)
        {
            var request = new RestRequest("/", Method.Post);
            request.AddJsonBody(new { query });

            var response = await _client.ExecuteAsync<GraphQLResponse<Dictionary<string, PaginatorData<T>>>>(request);

            if (response == null)
                throw new Exception("No response from GraphQL API.");

            // put in try block?
            var header = response?.Headers?.FirstOrDefault(h => string.Equals(h.Name, "x-ratelimit-remaining", StringComparison.OrdinalIgnoreCase));
            if (header != null && int.TryParse(header.Value?.ToString(), out var remaining))
                RateLimitRemaining = remaining;

            if (response != null && response.IsSuccessful && response.Data?.Data?.Count > 0)
            {
                var root = response.Data.Data;
                if (root.TryGetValue(queryType, out var paginator) && paginator?.Data?.Count > 0)
                {
                    return paginator.Data[0];
                }
            }

            var status = response?.StatusCode.ToString() ?? "(no response)";
            var error = response?.ErrorMessage ?? response?.Content;
            throw new Exception($"GraphQL Call Failed (City/Nation ID: {id}). Status: {status}. Error: {error}");
        }

        protected async Task<T> ExecuteMutationAsync<T>(string mutation, object variables) where T : class
        {
            var request = new RestRequest("/", Method.Post);

            request.AddJsonBody(new { query = mutation, variables });

            var response = await _client.ExecuteAsync<GraphQLResponse<T>>(request);

            if (response == null)
                throw new Exception("No response from GraphQL API (mutation).");

            // put in try block?
            var header = response?.Headers?.FirstOrDefault(h => string.Equals(h.Name, "x-ratelimit-remaining", StringComparison.OrdinalIgnoreCase));
            if (header != null && int.TryParse(header.Value?.ToString(), out var remaining))
                RateLimitRemaining = remaining;

            if (response != null && response.IsSuccessful && response.Data != null && response.Data.Data != null)
            {
                return response.Data.Data;
            }

            var status = response?.StatusCode.ToString() ?? "(no response)";
            var error = response?.ErrorMessage ?? response?.Content;
            throw new Exception($"Mutation Failed. Status: {status}. Error: {error}");
        }
    }
}