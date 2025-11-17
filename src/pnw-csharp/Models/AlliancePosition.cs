namespace PnW.Models
{
    public class AlliancePosition
    {
        public string? id { get; set; }
        public DateTime? date { get; set; }
        public string? alliance_id { get; set; }
        public string? name { get; set; }
        public string? creator_id { get; set; }
        public string? last_editor_id { get; set; }
        public DateTime? date_modified { get; set; }
        public int position_level { get; set; }
        public bool leader { get; set; }
        public bool heir { get; set; }
        public bool officer { get; set; }
        public bool member { get; set; }
        public int permissions { get; set; }
        public bool view_bank { get; set; }
        public bool withdraw_bank { get; set; }
        public bool change_permissions { get; set; }
        public bool see_spies { get; set; }
        public bool see_reset_timers { get; set; }
        public bool tax_brackets { get; set; }
        public bool post_announcements { get; set; }
        public bool manage_announcements { get; set; }
        public bool accept_applicants { get; set; }
        public bool remove_members { get; set; }
        public bool edit_alliance_info { get; set; }
        public bool manage_treaties { get; set; }
        public bool manage_market_share { get; set; }
        public bool manage_embargoes { get; set; }
        public bool promote_self_to_leader { get; set; }
    }
}
