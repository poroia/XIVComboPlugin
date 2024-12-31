using Lumina.Excel.Sheets;

namespace XIVCombo.Combos;

internal static class ADV
{
    public const byte ClassID = 0;
    public const byte JobID = 0;

    public const uint
        Provoke = 7533,
        Shirk = 7537,
        LowBlow = 7540,
        HeadGraze = 7551,
        Peloton = 7557,
        Swiftcast = 7561,
        LucidDreaming = 7562,
        AngelWhisper = 18317,
        VariantRaise2 = 29734;

    public static class Buffs
    {
        public const ushort
            Swiftcast = 167,
            Medicated = 49,
            Peloton = 1199;
    }

    public static class Debuffs
    {
        public const ushort
            Placeholder = 0;
    }

    public static class Levels
    {
        public const byte
            // Note that unlike class/job abilities, role actions are available even when level-synced below their
            // the level they are learned at.
            LowBlow = 12,
            Swiftcast = 18,
            VariantRaise2 = 90;
    }
}

