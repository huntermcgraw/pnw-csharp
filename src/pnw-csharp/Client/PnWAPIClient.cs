using RestSharp;
using PnW.Models;
using System.Reflection;

namespace PnW.Client
{

    public class PnWAPIClient
    {
        private readonly RestClient _client;

        private readonly string GraphQLUrl;

        private readonly Dictionary<Type, string> _map = new()
        {
            [typeof(City)] = "cities",
            [typeof(Nation)] = "nations"
        };

        public PnWAPIClient(string apiKey)
        {
            GraphQLUrl = $"https://api.politicsandwar.com/graphql?api_key={apiKey}";
            _client = new RestClient(GraphQLUrl);
        }

        private string LoadResourceQuery(string resourceName)
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

        public async Task<T> ExecuteGraphQLQueryAsync<T>(string query, string queryType, string id)
        {
            var request = new RestRequest("/", Method.Post);
            request.AddJsonBody(new { query = query });

            RestResponse<GraphQLResponse<Dictionary<string, PaginatorData<T>>>> response =
                await _client.ExecuteAsync<GraphQLResponse<Dictionary<string, PaginatorData<T>>>>(request);

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

        public async Task<T> GetQuery<T>(string targetId, List<string> fieldList)
        {
            if (!_map.TryGetValue(typeof(T), out string? queryType))
            {
                 throw new InvalidOperationException($"Type {typeof(T).Name} is not mapped to a GraphQL resource.");
            }
            string queryTemplate = LoadResourceQuery("GetResource");
            string fields = string.Join("\n", fieldList);
            
            string finalQuery = string.Format(queryTemplate, queryType, targetId, fields);

            return await ExecuteGraphQLQueryAsync<T>(finalQuery, queryType, targetId);
        }
    }

}