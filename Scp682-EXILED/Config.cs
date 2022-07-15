using System.ComponentModel;
using Exiled.API.Interfaces;

namespace Scp682_EXILED
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        [Description("Role Configs")]
        public int MaxHealth { get; set; } = 3000;
        public uint Id { get; set; } = 682;
        public string Description { get; set; } = string.Empty;
        public string CustomInfo { get; set; } = string.Empty;
        public float DamageAmount { get; set; } = 80f;
        public string CassieDeathMessage { get; set; } = "S C P 6 8 2 Has been contained";
        public int SpawnChance { get; set; } = 30;
        public int RegenAmount { get; set; } = 30;
        public float RegenDelay { get; set; } = 20f;
    }
}