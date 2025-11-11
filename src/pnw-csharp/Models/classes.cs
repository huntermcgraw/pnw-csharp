namespace PnW.Models
{

    public class City
    {
        public string? id { get; set; }
        public string? nation_id { get; set; }
        public Nation? nation { get; set; }
        public string? name { get; set; }
        public string? date { get; set; }
        public float? infrastructure { get; set; }
        public float? land { get; set; }
        public bool? powered { get; set; }
        public int? oil_power { get; set; }
        public int? wind_power { get; set; }
        public int? coal_power { get; set; }
        public int? nuclear_power { get; set; }
        public int? coal_mine { get; set; }
        public int? oil_well { get; set; }
        public int? uranium_mine { get; set; }
        public int? barracks { get; set; }
        public int? farm { get; set; }
        public int? police_station { get; set; }
        public int? hospital { get; set; }
        public int? recycling_center { get; set; }
        public int? subway { get; set; }
        public int? supermarket { get; set; }
        public int? bank { get; set; }
        public int? shopping_mall { get; set; }
        public int? stadium { get; set; }
        public int? lead_mine { get; set; }
        public int? iron_mine { get; set; }
        public int? bauxite_mine { get; set; }
        public int? oil_refinery { get; set; }
        public int? aluminum_refinery { get; set; }
        public int? steel_mill { get; set; }
        public int? munitions_factory { get; set; }
        public int? factory { get; set; }
        public int? hangar { get; set; }
        public int? drydock { get; set; }
        public string? nuke_date { get; set; }

        public Dictionary<string, int?> GetMilitary()
        {
            return new Dictionary<string, int?>
            {
                {"Barracks", this.barracks },
                {"Factory", this.factory },
                {"Hangar", this.hangar },
                {"Drydock", this.drydock}
            };
        }
    }
    public class Nation
    {
        public string? id { get; set; }
        public string? nation_name { get; set; }
        public string? leader_name { get; set; }
    }

    public class Trade
    {
        public string? id { get; set; }
        public string? type { get; set; }
        public string? date { get; set; }
        public string? sender_id { get; set; }
        public string? receiver_id { get; set; }

    }

    public class Bounty
    {
        public string? id { get; set; }
        public string? date { get; set; }
    }

    public class MilitaryResearch
    {
        public string? id { get; set; }
        public string? date { get; set; }
    }
    
        public class Alliance
    {
        public string? id { get; set; }
        public string? name { get; set; }
    }

}