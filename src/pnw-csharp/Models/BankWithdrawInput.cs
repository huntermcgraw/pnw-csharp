namespace PnW.Models
{
    public class BankWithdrawInput
    {
        public required string receiver;
        public required int receiver_type;
        public float? money { get; set; }
        public float? coal { get; set; }
        public float? oil { get; set; }
        public float? uranium { get; set; }
        public float? iron { get; set; }
        public float? bauxite { get; set; }
        public float? lead { get; set; }
        public float? gasoline { get; set; }
        public float? munitions { get; set; }
        public float? steel { get; set; }
        public float? aluminum { get; set; }
        public float? food { get; set; }
        public string? note { get; set; } 
    }
}