using RestSharp;
using System.Text.Json.Serialization;
using DotNetEnv;

namespace PoliticsAndWarAPIExamples
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            Env.Load();
            
            string? apiKey = Environment.GetEnvironmentVariable("PNW_API_KEY");

            if (string.IsNullOrEmpty(apiKey))
            {
                Console.WriteLine("ERROR: PNW_API_KEY environment variable is not set. Cannot run the application.");
                return;
            }
            
            const string targetCityId = "1332734";

            try
            {
                Console.WriteLine($"Fetching data for City ID: {targetCityId}...");

                var api = new CityAPI(apiKey);
                City cityData = await api.GetCity(targetCityId);

                if (cityData != null)
                {
                    Console.WriteLine("\n--- City Data Retrieved ---");
                    Console.WriteLine($"Name: {cityData.name}");
                    Console.WriteLine($"Nation: {cityData.nation} (ID: {cityData.nation_id})");
                    Console.WriteLine($"Population: {cityData.population:N0}");
                    Console.WriteLine($"Infrastructure: {cityData.infrastructure}");
                }
                else
                {
                    Console.WriteLine("Failed to retrieve city data or City object was null.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }

    public class CityPaginatorData {

    [JsonPropertyName("data")]
    public List<City> Cities { get; set; } = new List<City>();
}

    public class CityListResponse
    {
        [JsonPropertyName("cities")]
        public CityPaginatorData Cities { get; set; } = default!;
        }
    public class GraphQLResponse<T>
    {
        [JsonPropertyName("data")]
        public T? Data { get; set; }
    }
    
    public class CityAPI
    {
        private readonly RestClient _client;
        
        private readonly string GraphQLUrl;

        public CityAPI(string apiKey)
        {
            GraphQLUrl = $"https://api.politicsandwar.com/graphql?api_key={apiKey}";
            _client = new RestClient(GraphQLUrl);
        }

        public async Task<City> GetCity(string cityId)
        {

            string query = $@"
                query {{
                cities(id: [{cityId}]) {{
                    data {{
                        name
                        nation_id
                        coalpower
                        oilpower
                        nuclearpower
                        windpower
                        coalmine
                        oilwell
                        ironmine
                        bauxitemine
                        leadmine
                        uramine
                        farm
                        gasrefinery
                        steelmill
                        aluminumrefinery
                        munitionsfactory
                        policestation
                        hospital
                        recyclingcenter
                        subway
                        supermarket
                        bank
                        mall
                        stadium
                        barracks
                        factory
                        hangar
                        drydock
                    }}
                }}
                }}";

            var request = new RestRequest("/", Method.Post);
            request.AddJsonBody(new { query = query });

            RestResponse<GraphQLResponse<CityListResponse>> response = 
                await _client.ExecuteAsync<GraphQLResponse<CityListResponse>>(request);

            if (response.IsSuccessful && response.Data?.Data?.Cities?.Cities?.Count > 0)
            {
                return response.Data.Data.Cities.Cities[0];
            }
            
            throw new Exception($"GraphQL Call Failed (City ID: {cityId}). Status: {response.StatusCode}. Error: {response.ErrorMessage ?? response.Content}");
        }
    }

    public class City
    {
        public bool success { get; set; }
        public string? cityid { get; set; }
        public string? url { get; set; }
        public string? nation_id { get; set; }
        public string? name { get; set; }
        public string? nation { get; set; }
        public string? leader { get; set; }
        public string? continent { get; set; }
        public string? founded { get; set; }
        public int? age { get; set; }
        public string? powered { get; set; }
        public string? infrastructure { get; set; }
        public string? land { get; set; }
        public double? population { get; set; }
        public double? popdensity { get; set; }
        public int? crime { get; set; }
        public double? disease { get; set; }
        public int? commerce { get; set; }
        public double? avgincome { get; set; }
        public int? pollution { get; set; }
        public int? nuclearpollution { get; set; }
        public int? basepop { get; set; }
        public double? basepopdensity { get; set; }
        public double? minimumwage { get; set; }
        public double? poplostdisease { get; set; }
        public int? poplostcrime { get; set; }
        public string? imp_coalpower { get; set; }
        public string? imp_oilpower { get; set; }
        public string? imp_nuclearpower { get; set; }
        public string? imp_windpower { get; set; }
        public string? imp_coalmine { get; set; }
        public string? imp_oilwell { get; set; }
        public string? imp_ironmine { get; set; }
        public string? imp_bauxitemine { get; set; }
        public string? imp_leadmine { get; set; }
        public string? imp_uramine { get; set; }
        public string? imp_farm { get; set; }
        public string? imp_gasrefinery { get; set; }
        public string? imp_steelmill { get; set; }
        public string? imp_aluminumrefinery { get; set; }
        public string? imp_munitionsfactory { get; set; }
        public string? imp_policestation { get; set; }
        public string? imp_hospital { get; set; }
        public string? imp_recyclingcenter { get; set; }
        public string? imp_subway { get; set; }
        public string? imp_supermarket { get; set; }
        public string? imp_bank { get; set; }
        public string? imp_mall { get; set; }
        public string? imp_stadium { get; set; }
        public string? imp_barracks { get; set; }
        public string? imp_factory { get; set; }
        public string? imp_hangar { get; set; }
        public string? imp_drydock { get; set; }
    }
}