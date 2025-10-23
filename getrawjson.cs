using System.Text;
using System.Text.Json;
using DotNetEnv;

class Program
{
  static async Task GetRawJson()
  {

    Env.Load();
    string? apiKey = Environment.GetEnvironmentVariable("PNW_API_KEY");
    string graphqlEndpoint = $"https://api.politicsandwar.com/graphql?api_key={apiKey}";

    var query = @"
        query {
          __schema {
            types {
              name
              kind
              fields {
                name
                type {
                  name
                  kind
                }
              }
            }
          }
        }";

    using var client = new HttpClient();

    var requestBody = new
    {
      query = query
    };

    var json = JsonSerializer.Serialize(requestBody);
    var content = new StringContent(json, Encoding.UTF8, "application/json");

    var response = await client.PostAsync(graphqlEndpoint, content);
    var responseString = await response.Content.ReadAsStringAsync();

    string projectDir = AppContext.BaseDirectory; // /bin folder
    string fileName = "raw.json";
    string path = Path.Combine(projectDir, fileName);
    File.WriteAllText(path, responseString);
    Console.WriteLine(responseString);
  }
}