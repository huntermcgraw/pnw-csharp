// File: src/pnw-csharp/Models/BankDepositRecord.cs

namespace PnW.Models
{
    public class Bankrec
    {
        public int id { get; set; }
        public DateTime date { get; set; }
        public int sender_id { get; set; } 
        public string? note { get; set; }
        public float money { get; set; }
        public float coal { get; set; }

        // add more
    }
}