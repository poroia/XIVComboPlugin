using Dalamud.Game.ClientState.JobGauge.Types;

namespace XIVCombo.Combos;

internal static class SCH
{
    public const byte ClassID = 15;
    public const byte JobID = 28;

    public const uint
        Aetherflow = 166,
        EnergyDrain = 167,
        Resurrection = 173,
        Ruin = 17869,
        Broil = 3584,
        Broil2 = 7435,
        Broil3 = 16541,
        Broil4 = 25865,
        Bio = 17864,
        Adloquium = 185,
        SacredSoil = 188,
        Lustrate = 189,
        Physick = 190,
        Indomitability = 3583,
        DeploymentTactics = 3585,
        EmergencyTactics = 3586,
        Dissipation = 3587,
        Excogitation = 7434,
        ChainStratagem = 7436,
        Aetherpact = 7437,
        WhisperingDawn = 16537,
        FeyIllumination = 16538,
        Recitation = 16542,
        FeyBless = 16543,
        SummonSeraph = 16545,
        Consolation = 16546,
        SummonEos = 17215,
        SummonSelene = 17216,
        Ruin2 = 17870,
        Seraphism = 37014;

    public static class Buffs
    {
        public const ushort
            Dissipation = 791,
            Recitation = 1896,
            Seraphism = 3884,
            SeraphismAura = 3885;
    }

    public static class Debuffs
    {
        public const ushort
            Bio = 179,
            Bio2 = 189,
            Biolysis = 1895;
    }

    public static class Levels
    {
        public const byte
            Resurrection = 12,
            Adloquium = 30,
            Aetherflow = 45,
            Lustrate = 45,
            Excogitation = 62,
            ChainStratagem = 66,
            Recitation = 74,
            Consolation = 80,
            SummonSeraph = 80;
    }
}


internal class ScholarEnergyDrain : CustomCombo
{
    protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.ScholarEnergyDrainAetherflowFeature;

    protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
    {
        if (actionID == SCH.EnergyDrain)
        {
            var gauge = GetJobGauge<SCHGauge>();

            if (level >= SCH.Levels.Aetherflow && gauge.Aetherflow == 0)
                return SCH.Aetherflow;
        }

        return actionID;
    }
}