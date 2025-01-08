using Dalamud.Game.ClientState.JobGauge.Types;

namespace XIVCombo.Combos;

internal static class RPR
{
    public const byte JobID = 39;

    public const uint
        // Single Target
        Slice = 24373,
        WaxingSlice = 24374,
        InfernalSlice = 24375,
        // AoE
        SpinningScythe = 24376,
        NightmareScythe = 24377,
        // Soul Reaver
        Gibbet = 24382,
        Gallows = 24383,
        Guillotine = 24384,
        BloodStalk = 24389,
        UnveiledGibbet = 24390,
        UnveiledGallows = 24391,
        GrimSwathe = 24392,
        Gluttony = 24393,
        // Generators
        SoulSlice = 24380,
        SoulScythe = 24381,
        // Sacrifice
        ArcaneCircle = 24405,
        PlentifulHarvest = 24385,
        // Shroud
        Enshroud = 24394,
        Communio = 24398,
        VoidReaping = 24395,
        CrossReaping = 24396,
        GrimReaping = 24397,
        LemuresSlice = 24399,
        LemuresScythe = 24400,
        Sacrificium = 36969,
        Perfectio = 36973,
        // Misc
        ShadowOfDeath = 24378,
        Harpe = 24386,
        Soulsow = 24387,
        HarvestMoon = 24388,
        HellsIngress = 24401,
        HellsEgress = 24402,
        Regress = 24403;

    public static class Buffs
    {
        public const ushort
            EnhancedHarpe = 2845,
            SoulReaver = 2587,
            Executioner = 3858,
            EnhancedGibbet = 2588,
            EnhancedGallows = 2589,
            EnhancedVoidReaping = 2590,
            EnhancedCrossReaping = 2591,
            ImmortalSacrifice = 2592,
            Enshrouded = 2593,
            Soulsow = 2594,
            Threshold = 2595,
            Oblatio = 3857,          // Sacrificium ready to use
            PerfectioOcculta = 3859, // Turns into Perfectio Parata when Communio is used
            PerfectioParata = 3860;  // Perfectio ready to use
    }

    public static class Debuffs
    {
        public const ushort
            Placeholder = 0;
    }

    public static class Levels
    {
        public const byte
            WaxingSlice = 5,
            HellsIngress = 20,
            HellsEgress = 20,
            SpinningScythe = 25,
            InfernalSlice = 30,
            NightmareScythe = 45,
            BloodStalk = 50,
            GrimSwathe = 55,
            SoulSlice = 60,
            SoulScythe = 65,
            SoulReaver = 70,
            Regress = 74,
            Gluttony = 76,
            Enshroud = 80,
            Soulsow = 82,
            HarvestMoon = 82,
            EnhancedShroud = 86,
            LemuresScythe = 86,
            PlentifulHarvest = 88,
            Communio = 90,
            Sacrificium = 92,
            Executioner = 96,
            Perfectio = 100;
    }
}

internal class ReaperSlice : CustomCombo
{
    protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.ReaperSliceCombo;

    protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
    {
        if (actionID == RPR.InfernalSlice)
        {
            if (lastComboMove == RPR.WaxingSlice && level >= RPR.Levels.InfernalSlice)
                return RPR.InfernalSlice;

            if (lastComboMove == RPR.Slice && level >= RPR.Levels.WaxingSlice)
                return RPR.WaxingSlice;

            return RPR.Slice;
        }

        return actionID;
    }
}

internal class ReaperScythe : CustomCombo
{
    protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.ReaperScytheCombo;

    protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
    {
        if (actionID == RPR.NightmareScythe)
        {
            if (lastComboMove == RPR.SpinningScythe && level >= RPR.Levels.NightmareScythe)
                return RPR.NightmareScythe;

            return RPR.SpinningScythe;
        }

        return actionID;
    }
}


internal class ReaperEnshroud : CustomCombo
{
    protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.ReaperEnshroudCommunioFeature;

    protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
    {
        if (actionID == RPR.Enshroud)
        {
            var gauge = GetJobGauge<RPRGauge>();

                if (level >= RPR.Levels.Communio && gauge.EnshroudedTimeRemaining > 0 || HasEffect(RPR.Buffs.PerfectioParata))
                    return OriginalHook(RPR.Communio);
        }

        return actionID;
    }
}

internal class ReaperArcaneCircle : CustomCombo
{
    protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.ReaperArcaneHarvestFeature;

    protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
    {
        if (actionID == RPR.ArcaneCircle)
        {
                if (level >= RPR.Levels.PlentifulHarvest && HasEffect(RPR.Buffs.ImmortalSacrifice))
                    return RPR.PlentifulHarvest;
        }

        return actionID;
    }
}


internal class ReaperHellsIngressEgress : CustomCombo
{
    protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.ReaperRegressFeature;

    protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
    {
        if (actionID == RPR.HellsEgress || actionID == RPR.HellsIngress)
        {
            if (level >= RPR.Levels.Regress)
			{
				if (FindEffect(RPR.Buffs.Threshold) != null)
					return RPR.Regress;
			}
        }

        return actionID;
    }
}
