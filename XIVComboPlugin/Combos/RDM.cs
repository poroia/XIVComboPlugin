using Dalamud.Game.ClientState.JobGauge.Types;

namespace XIVCombo.Combos;

internal static class RDM
{
    public const byte JobID = 35;

    public const uint
        Jolt = 7503,
        Riposte = 7504,
        Verthunder = 7505,
        Veraero = 7507,
        Scatter = 7509,
        Verfire = 7510,
        Verstone = 7511,
        Zwerchhau = 7512,
        Moulinet = 7513,
        Vercure = 7514,
        Redoublement = 7516,
        Fleche = 7517,
        Acceleration = 7518,
        ContreSixte = 7519,
        Embolden = 7520,
        Manafication = 7521,
        Verraise = 7523,
        Jolt2 = 7524,
        Verflare = 7525,
        Verholy = 7526,
        EnchantedRiposte = 7527,
        EnchantedZwerchhau = 7528,
        EnchantedRedoublement = 7529,
        Verthunder2 = 16524,
        Veraero2 = 16525,
        Impact = 16526,
        Scorch = 16530,
        Verthunder3 = 25855,
        Veraero3 = 25856,
        Resolution = 25858,
        ViceOfThorns = 37005,
        GrandImpact = 37006,
        Prefulgence = 37007;

    public static class Buffs
    {
        public const ushort
            Swiftcast = 167,
            VerfireReady = 1234,
            VerstoneReady = 1235,
            Acceleration = 1238,
            Dualcast = 1249,
            LostChainspell = 2560,
            ThornedFlourish = 3876,
            GrandImpactReady = 3877,
            PrefulgenceReady = 3878;
    }

    public static class Debuffs
    {
        public const ushort
            Placeholder = 0;
    }

    public static class Levels
    {
        public const byte
            Jolt = 2,
            Verthunder = 4,
            Veraero = 10,
            Scatter = 15,
            Zwerchhau = 35,
            Fleche = 45,
            Redoublement = 50,
            Acceleration = 50,
            Vercure = 54,
            ContreSixte = 56,
            Embolden = 58,
            Manafication = 60,
            Jolt2 = 62,
            Verraise = 64,
            Impact = 66,
            Verflare = 68,
            Verholy = 70,
            Scorch = 80,
            Veraero3 = 82,
            Verthunder3 = 82,
            Resolution = 90,
            ViceOfThorns = 92,
            GrandImpact = 96,
            Prefulgence = 100;
    }
}


internal class RedMageVeraeroVerthunder2 : CustomCombo
{
    protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.RdmAny;

    protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
    {
        if (actionID == RDM.Veraero2 || actionID == RDM.Verthunder2)
        {
            if (IsEnabled(CustomComboPreset.RedMageAoEFeature))
            {
                if (level >= RDM.Levels.Scatter && (HasEffect(RDM.Buffs.Dualcast) || HasEffect(RDM.Buffs.Acceleration) || HasEffect(RDM.Buffs.Swiftcast) || HasEffect(RDM.Buffs.LostChainspell)))
                    return OriginalHook(RDM.Scatter);
            }
        }

        return actionID;
    }
}

internal class RedMageRedoublementMoulinet : CustomCombo
{
    protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.RdmAny;

    protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
    {
        if (actionID == RDM.Redoublement)
        {
            if (IsEnabled(CustomComboPreset.RedMageMeleeCombo))
            {
                if (lastComboMove == RDM.Zwerchhau && level >= RDM.Levels.Redoublement)
                    // Enchanted
                    return OriginalHook(RDM.Redoublement);

                if ((lastComboMove == RDM.Riposte || lastComboMove == RDM.EnchantedRiposte) && level >= RDM.Levels.Zwerchhau)
                    // Enchanted
                    return OriginalHook(RDM.Zwerchhau);

                // Enchanted
                return OriginalHook(RDM.Riposte);
            }
        }

        return actionID;
    }
}

internal class RedMageVerstoneVerfire : CustomCombo
{
    protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.RdmAny;

    protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
    {
        if (actionID == RDM.Verstone)
        {
            if (IsEnabled(CustomComboPreset.RedMageVerprocFeature))
            {
                if (HasEffect(RDM.Buffs.VerstoneReady))
                    return RDM.Verstone;

                // Jolt
                return OriginalHook(RDM.Jolt2);
            }
        }

        if (actionID == RDM.Verfire)
        {
            if (IsEnabled(CustomComboPreset.RedMageVerprocFeature))
            {
                if (HasEffect(RDM.Buffs.VerfireReady))
                    return RDM.Verfire;

                // Jolt
                return OriginalHook(RDM.Jolt2);
            }
        }

        return actionID;
    }
}
