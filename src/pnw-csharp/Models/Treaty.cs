namespace PnW.Models
{
    public class Treaty
    {
        public string? id { get; set; }
        public DateTime? date { get; set; }
        public string? treaty_type { get; set; }
        public string? treaty_url { get; set; }
        public int? turns_left { get; set; }
        public string? alliance1_id { get; set; }
        public Alliance? alliance1 { get; set; }
        public string? alliance2_id { get; set; }
        public Alliance? alliance2 { get; set; }
        public bool? approved { get; set; }
    }
}