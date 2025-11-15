using PnW.Models;

namespace PnW.Client.Mutations
{
    public class BankDepositClient : PnWBaseClient
    {
        public BankDepositClient(string apiKey, string? botKey = null, string? botKeyApiKey = null)
            : base(apiKey, botKey, botKeyApiKey) { }

        public async Task<Bankrec> DepositAsync(BankDepositInput input)
        {
            string mutationTemplate = LoadResourceQuery("BankDeposit");
            var response = await ExecuteMutationAsync<Dictionary<string, Bankrec>>(
                mutationTemplate,
                input
            );

            if (response.TryGetValue("bankDeposit", out var record))
                return record;

            throw new Exception("Bank deposit succeeded but response record was not found.");
        }
    }
}
