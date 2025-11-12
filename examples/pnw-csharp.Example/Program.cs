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
            string? botKey = Environment.GetEnvironmentVariable("PNW_BOT_KEY");
            string? botKeyApiKey = Environment.GetEnvironmentVariable("PNW_BOT_KEY_API_KEY");

            if (string.IsNullOrEmpty(apiKey))
            {
                Console.WriteLine("ERROR: PNW_API_KEY environment variable is not set. Cannot run the application.");
                return;
            }

            const string targetCityId = "1332734";
            const string targetNationId = "698069";

            BankDepositInput newDeposit = new BankDepositInput();
            newDeposit.Money = 100000;
            newDeposit.Note = "safekeeping";

            try
            {
                var pnw = new PnWClient(apiKey, botKey, botKeyApiKey);
                var city = await pnw.Query.GetQueryAsync<City>(targetCityId, new() { "name", "barracks", "factory", "hangar", "drydock" });
                //var deposit = await pnw.BankDeposit.DepositAsync(newDeposit);

                if (city != null)
                {
                    Console.WriteLine("\n--- Data Retrieved ---");
                    Console.WriteLine($"Name: {city.name}");
                    Dictionary<string, int?> mil = city.GetMilitary();
                    foreach (KeyValuePair<string, int?> pair in mil)
                    {
                        Console.WriteLine($"{pair.Key}: {pair.Value}");
                    }
                }
                else
                {
                    Console.WriteLine("Failed to retrieve city data or City object was null.");
                }

                /*Console.WriteLine($"Deposit Record ID: {deposit.Id}");
                Console.WriteLine($"Amount Deposited: ${deposit.Money}");
                Console.WriteLine($"Date: {deposit.Date}");
                Console.WriteLine($"Note: {deposit.Note}");*/
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}