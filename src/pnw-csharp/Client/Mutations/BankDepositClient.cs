using PnW.Models;

namespace PnW.Client.Mutations
{
    public class BankDepositClient : PnWBaseClient
    {
        public BankDepositClient(string apiKey, string? botKey = null)
            : base(apiKey, botKey) { }

        public async Task<BankDepositRecord> DepositAsync(BankDepositInput input)
        {
            string mutationTemplate = LoadResourceQuery("BankDeposit");
            var response = await ExecuteMutationAsync<Dictionary<string, BankDepositRecord>>(
                mutationTemplate,
                input
            );

            if (response.TryGetValue("bankDeposit", out var record))
                return record;

            throw new Exception("Bank deposit succeeded but response record was not found.");
        }
    }
}
