using Dalamud.Game.ClientState.JobGauge.Types;

namespace XIVCombo.Combos;

internal static class NIN
{
    public const byte ClassID = 29;
    public const byte JobID = 30;

    public const uint
        SpinningEdge = 2240,
        GustSlash = 2242,
        Hide = 2245,
        Assassinate = 8814,
        Mug = 2248,
        DeathBlossom = 2254,
        AeolianEdge = 2255,
        TrickAttack = 2258,
        Ninjutsu = 2260,
        Kassatsu = 2264,
        Suiton = 2271,
        ArmorCrush = 3563,
        DreamWithinADream = 3566,
        Hellfrog = 7401,
        Bhavacakra = 7402,
        TenChiJin = 7403,
        HakkeMujinsatsu = 16488,
        Meisui = 16489,
        Bunshin = 16493,
        Huraijin = 25876,
        PhantomKamaitachi = 25774,
        ForkedRaiju = 25777,
        FleetingRaiju = 25778,
        Dokumori = 36957,

        // Ninjutsu
        Ten = 2259, // Normal version on your bar, with charges
        Chi = 2261,
        Jin = 2263,
        TenMudra = 18805, // No-cooldown version that only appears during a Mudra cast, after the first symbol
        ChiMudra = 18806,
        JinMudra = 18807;

    public static class Buffs
    {
        public const ushort
            Mudra = 496,
            Kassatsu = 497,
            Suiton = 507,
            Hidden = 614,
            Bunshin = 1954,
            RaijuReady = 2690,
            ShadowWalker = 3848,
            Higi = 3850;
    }

    public static class Debuffs
    {
        public const ushort
            Placeholder = 0;
    }

    public static class Levels
    {
        public const byte
            GustSlash = 4,
            Hide = 10,
            Mug = 15,
            TrickAttack = 18,
            AeolianEdge = 26,
            Ninjutsu = 30,
            Suiton = 45,
            Kassatsu = 50,
            HakkeMujinsatsu = 52,
            ArmorCrush = 54,
            Huraijin = 60,
            Hellfrog = 62,
            Dokumori = 66,
            Bhavacakra = 68,
            TenChiJin = 70,
            Meisui = 72,
            EnhancedKassatsu = 76,
            Bunshin = 80,
            PhantomKamaitachi = 82,
            Raiju = 90,
            KunaisBane = 92,
            TenriJindo = 100;
    }
}

internal class NinjaAeolianEdge : CustomCombo
{
    protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.NinAny;

    protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
    {
        if (actionID == NIN.AeolianEdge)
        {
            var gauge = GetJobGauge<NINGauge>();

            if (IsEnabled(CustomComboPreset.NinjaAeolianEdgeCombo))
            {
                if (lastComboMove == NIN.GustSlash && level >= NIN.Levels.AeolianEdge)
                    return NIN.AeolianEdge;

                if (lastComboMove == NIN.SpinningEdge && level >= NIN.Levels.GustSlash)
                    return NIN.GustSlash;

                return NIN.SpinningEdge;
            }
        }

        return actionID;
    }
}

internal class NinjaArmorCrush : CustomCombo
{
    protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.NinAny;

    protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
    {
        if (actionID == NIN.ArmorCrush)
        {
            if (IsEnabled(CustomComboPreset.NinjaArmorCrushCombo))
            {
                if (lastComboMove == NIN.GustSlash && level >= NIN.Levels.ArmorCrush)
                    return NIN.ArmorCrush;

                if (lastComboMove == NIN.SpinningEdge && level >= NIN.Levels.GustSlash)
                    return NIN.GustSlash;

                return NIN.SpinningEdge;
            }
        }

        return actionID;
    }
}

internal class NinjaHakkeMujinsatsu : CustomCombo
{
    protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.NinAny;

    protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
    {
        if (actionID == NIN.HakkeMujinsatsu)
        {

            if (IsEnabled(CustomComboPreset.NinjaHakkeMujinsatsuCombo))
            {
                if (lastComboMove == NIN.DeathBlossom && level >= NIN.Levels.HakkeMujinsatsu)
                    return NIN.HakkeMujinsatsu;

                return NIN.DeathBlossom;
            }
        }

        return actionID;
    }
}