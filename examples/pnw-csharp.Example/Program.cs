// Reminder that automated trading/banking/etc are not allowed

using DotNetEnv;
using PnW.Models;
using PnW.Client;

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

                var api = new PnWAPIClient(apiKey);
                City data = await api.GetQuery<City>(targetCityId, ["name", "barracks", "factory", "hangar", "drydock"]);
                if (data != null)
                {
                    Console.WriteLine("\n--- Data Retrieved ---");
                    Console.WriteLine($"Name: {data.name}");
                    Dictionary<string, int?> mil = data.GetMilitary();
                    foreach (KeyValuePair<string, int?> pair in mil)
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