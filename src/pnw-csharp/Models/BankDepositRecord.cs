// File: src/pnw-csharp/Models/BankDepositRecord.cs

namespace PnW.Models
{
    public class BankDepositRecord
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int SenderId { get; set; } 
        public string? Note { get; set; }
        public float Money { get; set; }
        public float Coal { get; set; }
    }
}