namespace PnW.Models
{
    public class TaxBracket
    {
        public string? id { get; set; }
        public string? alliance_id { get; set; }
        public Alliance? alliance { get; set; }
        public DateTime? date { get; set; }
        public DateTime? date_modified { get; set; }
        public string? last_modifier_id { get; set; }
        public Nation? last_modifier { get; set; }
        public int tax_rate { get; set; }
        public int resource_tax_rate { get; set; }
        public string? bracket_name { get; set; }
    }
}