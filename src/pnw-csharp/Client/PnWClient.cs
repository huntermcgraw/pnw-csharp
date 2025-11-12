using System.Threading.Tasks.Dataflow;
using PnW.Client.Mutations;

namespace PnW.Client
{
    public class PnWClient
    {
        public QueryClient Query { get; }
        public BankDepositClient BankDeposit { get; }

        public PnWClient(string apiKey, string? botKey = null, string? botKeyApiKey = null)
        {
            Query = new QueryClient(apiKey, botKey, botKeyApiKey);
            BankDeposit = new BankDepositClient(apiKey, botKey, botKeyApiKey);
        }
    }
}
