using RestSharp;
using System.Text.Json.Serialization;

using PnW.Classes;



namespace PnW.Query
{
    public class PaginatorData<T>
    {

        [JsonPropertyName("data")]
        public List<T> Data { get; set; } = new List<T>();
    }

    public class GraphQLResponse<T>
    {
        [JsonPropertyName("data")]
        public T? Data { get; set; }
    }

    public class API
    {
        private readonly RestClient _client;

        private readonly string GraphQLUrl;

        private readonly Dictionary<Type, string> _map = new()
        {
            [typeof(City)] = "cities",
            [typeof(Nation)] = "nations"
        };

        public API(string apiKey)
        {
            GraphQLUrl = $"https://api.politicsandwar.com/graphql?api_key={apiKey}";
            _client = new RestClient(GraphQLUrl);
        }

        public async Task<T> GetData<T>(string queryType, string id, List<string> fieldList)
        {
            string fields = string.Join("\n", fieldList);
            string query = $@"
            query {{
            {queryType}(id: [{id}]) {{
                data {{
                    {fields}
                }}
            }}
            }}";

            var request = new RestRequest("/", Method.Post);
            request.AddJsonBody(new { query = query });

            RestResponse<GraphQLResponse<Dictionary<string, PaginatorData<T>>>> response =
                await _client.ExecuteAsync<GraphQLResponse<Dictionary<string, PaginatorData<T>>>>(request);

            if (response.IsSuccessful && response.Data?.Data?.Count > 0)
            {
                var root = response.Data?.Data;
                if (root != null && root.TryGetValue(queryType, out var paginator) && paginator?.Data?.Count > 0)
                {
                    return paginator.Data[0];
                }

            }

            throw new Exception($"GraphQL Call Failed (City/Nation ID: {id}). Status: {response.StatusCode}. Error: {response.ErrorMessage ?? response.Content}");
        }

        public async Task<T> GetQuery<T>(string targetId, List<string> fieldList)
        {
            string queryType = _map[typeof(T)];
            Console.WriteLine(queryType);
            return await GetData<T>(queryType, targetId, fieldList);
        }
    }

}