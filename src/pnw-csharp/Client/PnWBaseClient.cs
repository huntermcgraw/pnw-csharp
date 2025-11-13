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
                _client.AddDefaultHeader("X-Bot-Key", botKey);
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

            RestResponse<GraphQLResponse<Dictionary<string, PaginatorData<T>>>> response =
                await _client.ExecuteAsync<GraphQLResponse<Dictionary<string, PaginatorData<T>>>>(request);

            // put in try block?
            var header = response?.Headers?.FirstOrDefault(h => string.Equals(h.Name, "x-ratelimit-remaining", StringComparison.OrdinalIgnoreCase));
            if (header != null && int.TryParse(header.Value?.ToString(), out var remaining))
                RateLimitRemaining = remaining;

            if (response.IsSuccessful && response.Data?.Data?.Count > 0)
            {
                var root = response.Data.Data;
                if (root.TryGetValue(queryType, out var paginator) && paginator?.Data?.Count > 0)
                {
                    return paginator.Data[0];
                }
            }

            throw new Exception($"GraphQL Call Failed (City/Nation ID: {id}). Status: {response.StatusCode}. Error: {response.ErrorMessage ?? response.Content}");
        }

        protected async Task<T> ExecuteMutationAsync<T>(string mutation, object variables) where T : class
        {
            var request = new RestRequest("/", Method.Post);

            request.AddJsonBody(new { query = mutation, variables });

            var response = await _client.ExecuteAsync<GraphQLResponse<T>>(request);

            // put in try block?
            var header = response?.Headers?.FirstOrDefault(h => string.Equals(h.Name, "x-ratelimit-remaining", StringComparison.OrdinalIgnoreCase));
            if (header != null && int.TryParse(header.Value?.ToString(), out var remaining))
                RateLimitRemaining = remaining;

            if (response != null && response.IsSuccessful && response.Data != null && response.Data.Data != null)
            {
                return response.Data.Data;
            }

            throw new Exception($"Mutation Failed. Status: {response.StatusCode}. Error: {response.ErrorMessage ?? response.Content}");
        }
    }
}