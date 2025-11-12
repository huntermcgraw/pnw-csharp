using PnW.Models;

namespace PnW.Client
{
    public class QueryClient : PnWBaseClient
    {
        private readonly Dictionary<Type, string> _map = new()
        {
            [typeof(City)] = "cities",
            [typeof(Nation)] = "nations"
        };

        public QueryClient(string apiKey, string? botKey = null, string? botKeyApiKey = null)
            : base(apiKey, botKey, botKeyApiKey) { }

        public async Task<T> GetQueryAsync<T>(string targetId, List<string> fieldList)
        {
            if (!_map.TryGetValue(typeof(T), out string? queryType))
                throw new InvalidOperationException($"Type {typeof(T).Name} is not mapped to a GraphQL resource.");

            string queryTemplate = LoadResourceQuery("GetResource");
            string fields = string.Join("\n", fieldList);
            string finalQuery = string.Format(queryTemplate, queryType, targetId, fields);

            return await ExecuteGraphQLQueryAsync<T>(finalQuery, queryType, targetId);
        }
    }
}
