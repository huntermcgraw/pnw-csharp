using DotNetEnv;
using PnW.City;

namespace PnWWrapper
{
    public class TestWrapper
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
                    //Console.WriteLine($"Nation: {cityData.nation} (ID: {cityData.nation_id})");
                    Dictionary<string, int> mil = cityData.GetMilitary();
                    foreach (KeyValuePair<string, int> pair in mil)
                    {
                        Console.WriteLine($"{pair.Key}: {pair.Value}");
                    }
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
}