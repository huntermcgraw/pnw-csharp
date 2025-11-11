using System.Text.Json.Serialization;

namespace PnW.Models
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
}
