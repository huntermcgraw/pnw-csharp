namespace PnW.Models
{
    public class Alliance
    {
        public string? id { get; set; }
        public string? name { get; set; }
        public string? acronym { get; set; }
        public float? score { get; set; }
        public string? color { get; set; }
        public DateTime? date { get; set; }
        public List<Nation>? nations { get; set; }
        public float? average_score { get; set; }
        //public List<Treaty>? treaties { get; set; }
        //public List<AlliancePosition>? alliance_positions { get; set; }
        public bool? accept_members { get; set; }
        public string? flag { get; set; }
        public string? forum_link { get; set; }
        public string? discord_link { get; set; }
        public string? wiki_link { get; set; }
        public List<Bankrec>? bankrecs { get; set; }
        public List<Bankrec>? taxrecs { get; set; }
        //public List<TaxBracket>? tax_brackets { get; set; }
        //public List<War>? wars { get; set; }
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
        //public List<Award>? awards { get; set; }
        public int? rank { get; set; }
        //public List<Bulletin>? bulletins { get; set; }

    }
}