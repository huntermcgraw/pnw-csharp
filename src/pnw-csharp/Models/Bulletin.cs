namespace PnW.Models
{
    public class Bulletin
    {
        public string id { get; set; }
        public string nation_id { get; set; }
        public Nation? nation { get; set; }
        public string? alliance_id { get; set; }
        public Alliance? alliance { get; set; }
        public int type { get; set; }
        public string headline { get; set; }
        public string? excerpt { get; set; }
        public string? image { get; set; }
        public string? body { get; set; }
        public string? author { get; set; }
        public bool pinned { get; set; }
        public int? like_count { get; set; }
        public bool replies_enabled { get; set; }
        public bool locked { get; set; }
        public DateTime date { get; set; }
        public DateTime edit_date { get; set; }
        public bool archived { get; set; }
        public BulletinReplyPaginator? replies { get; set; }
    }
}
