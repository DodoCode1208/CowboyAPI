using System.ComponentModel;

namespace CowboyAPI.Models
{
    public enum GunEnumTypecs
    {
        [Description("PIS100")]
        Pistol100m,
        [Description("PIS500")]
        Pistol500m,
        [Description("RFL500")]
        Rifle,
        [Description("SNP001")]
        Sniper001,
        [Description("AK047")]
        AK47,
        [Description("AK056")]
        AK56,
        [Description("MTR001")]
        MotarGun,
        [Description("LRR002")]
        LongRangeRifle2000m
    }
}
