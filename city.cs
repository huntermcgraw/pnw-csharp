using RestSharp;
using System.Text.Json.Serialization;

namespace PnW.City
{

    public class CityPaginatorData
    {

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
        {// there are way more nation fields
            string query = $@"
                query {{
                cities(id: [{cityId}]) {{
                    data {{
                        id
                        nation_id
                        nation {{
                            id
                            alliance_id
                            alliance_position
                            alliance_position_id


                            nation_name
                            leader_name
                            continent
                            war_policy
                            war_policy_turns
                            domestic_policy
                            domestic_policy_turns
                            color
                            num_cities

                            score
                            update_tz
                            population
                            flag
                            vacation_mode_turns
                            beige_turns
                            espionage_available
                            last_active
                            date
                            soldiers
                            tanks
                            aircraft
                            ships
                            missiles
                            nukes
                            spies

                        }}
                        name
                        date
                        infrastructure
                        land
                        powered
                        oil_power
                        wind_power
                        coal_power
                        nuclear_power
                        coal_mine
                        oil_well
                        uranium_mine
                        barracks
                        farm
                        police_station
                        hospital
                        recycling_center
                        subway
                        supermarket
                        bank
                        shopping_mall
                        stadium
                        lead_mine
                        iron_mine
                        bauxite_mine
                        oil_refinery
                        aluminum_refinery
                        steel_mill
                        munitions_factory
                        factory
                        hangar
                        drydock
                        nuke_date
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
        public string? id { get; set; }
        public string? nation_id { get; set; }
        public Object? nation { get; set; }
        public string? name { get; set; }
        public string? date { get; set; }
        public float? infrastructure { get; set; }
        public float? land { get; set; }
        public bool? powered { get; set; }
        public int? oil_power { get; set; }
        public int? wind_power { get; set; }
        public int? coal_power { get; set; }
        public int? nuclear_power { get; set; }
        public int? coal_mine { get; set; }
        public int? oil_well { get; set; }
        public int? uranium_mine { get; set; }
        public int barracks { get; set; }
        public int? farm { get; set; }
        public int? police_station { get; set; }
        public int? hospital { get; set; }
        public int? recycling_center { get; set; }
        public int? subway { get; set; }
        public int? supermarket { get; set; }
        public int? bank { get; set; }
        public int? shopping_mall { get; set; }
        public int? stadium { get; set; }
        public int? lead_mine { get; set; }
        public int? iron_mine { get; set; }
        public int? bauxite_mine { get; set; }
        public int? oil_refinery { get; set; }
        public int? aluminum_refinery { get; set; }
        public int? steel_mill { get; set; }
        public int? munitions_factory { get; set; }
        public int factory { get; set; }
        public int hangar { get; set; }
        public int drydock { get; set; }
        public string? nuke_date { get; set; }

        public Dictionary<string, int> GetMilitary()
        {
            return new Dictionary<string, int>
            {
                {"Barracks", this.barracks },
                {"Factory", this.factory },
                {"Hangar", this.hangar },
                {"Drydock", this.drydock}
            };
        }
    }
}