namespace PnW.Models
{
    public class War
    {
        public string? id { get; set; }
        public DateTime? date { get; set; }
        public DateTime? end_date { get; set; }
        public string? reason { get; set; }
        //public WarType? war_type { get; set; }
        public string? ground_control { get; set; }
        public string? air_superiority { get; set; }
        public string? naval_blockade { get; set; }
        public string? winner_id { get; set; }
        //public List<WarAttack> attacks { get; set; } = new List<WarAttack>();
        public int? turns_left { get; set; }
        public string? att_id { get; set; }
        public string? att_alliance_id { get; set; }
        //public AlliancePositionEnum? att_alliance_position { get; set; }
        public Nation? attacker { get; set; }
        public string? def_id { get; set; }
        public string? def_alliance_id { get; set; }
        //public AlliancePositionEnum? def_alliance_position { get; set; }
        public Nation? defender { get; set; }
        public int? att_points { get; set; }
        public int? def_points { get; set; }
        public bool? att_peace { get; set; }
        public bool? def_peace { get; set; }
        public int? att_resistance { get; set; }
        public int? def_resistance { get; set; }
        public bool? att_fortify { get; set; }
        public bool? def_fortify { get; set; }
        public float? att_gas_used { get; set; }
        public float? def_gas_used { get; set; }
        public float? att_mun_used { get; set; }
        public float? def_mun_used { get; set; }
        public float? att_alum_used { get; set; }
        public float? def_alum_used { get; set; }
        public float? att_steel_used { get; set; }
        public float? def_steel_used { get; set; }
        public float? att_infra_destroyed { get; set; }
        public float? def_infra_destroyed { get; set; }
        public float? att_money_looted { get; set; }
        public float? def_money_looted { get; set; }
        public int? def_soldiers_lost { get; set; }
        public int? att_soldiers_lost { get; set; }
        public int? def_tanks_lost { get; set; }
        public int? att_tanks_lost { get; set; }
        public int? def_aircraft_lost { get; set; }
        public int? att_aircraft_lost { get; set; }
        public int? def_ships_lost { get; set; }
        public int? att_ships_lost { get; set; }
        public float? att_infra_destroyed_value { get; set; }
        public float? def_infra_destroyed_value { get; set; }
    }
}