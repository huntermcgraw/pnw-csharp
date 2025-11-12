using PnW.Client.Mutations;

namespace PnW.Client
{
    public class PnWClient
    {
        public QueryClient Query { get; }
        public BankDepositClient BankDeposit { get; }

        public PnWClient(string apiKey, string? botKey = null)
        {
            Query = new QueryClient(apiKey, botKey);
            BankDeposit = new BankDepositClient(apiKey, botKey);
        }
    }
}
