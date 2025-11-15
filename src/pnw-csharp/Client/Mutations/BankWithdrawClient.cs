using PnW.Models;

namespace PnW.Client.Mutations
{
    public class BankWithdrawClient : PnWBaseClient
    {
        public BankWithdrawClient(string apiKey, string? botKey = null, string? botKeyApiKey = null)
            : base(apiKey, botKey, botKeyApiKey) { }

        public async Task<Bankrec> WithdrawAsync(BankWithdrawInput input)
        {
            string mutationTemplate = LoadResourceQuery("BankWithdraw");
            var response = await ExecuteMutationAsync<Dictionary<string, Bankrec>>(
                mutationTemplate,
                input
            );

            if (response.TryGetValue("bankWithdraw", out var record))
                return record;

            throw new Exception("Bank withdrawal succeeded but response record was not found.");
        }
    }
}
