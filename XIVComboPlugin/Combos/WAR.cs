using System;

using Dalamud.Game.ClientState.JobGauge.Types;

namespace XIVCombo.Combos;

internal static class WAR
{
    public const byte ClassID = 3;
    public const byte JobID = 21;

    public const uint
        HeavySwing = 31,
        Maim = 37,
        Berserk = 38,
        ThrillOfBattle = 40,
        Overpower = 41,
        StormsPath = 42,
        StormsEye = 45,
        Defiance = 48,
        InnerBeast = 49,
        SteelCyclone = 51,
        Infuriate = 52,
        FellCleave = 3549,
        Decimate = 3550,
        RawIntuition = 3551,
        Equilibrium = 3552,
        InnerRelease = 7389,
        MythrilTempest = 16462,
        ChaoticCyclone = 16463,
        NascentFlash = 16464,
        InnerChaos = 16465,
        Bloodwhetting = 25751,
        PrimalRend = 25753,
        DefianceRemoval = 32066,
        PrimalWrath = 36924,
        PrimalRuination = 36925;

    public static class Buffs
    {
        public const ushort
            Berserk = 86,
            Defiance = 91,
            InnerRelease = 1177,
            NascentChaos = 1897,
            PrimalRendReady = 2624,
            SurgingTempest = 2677,
            Wrathful = 3901,
            PrimalRuinationReady = 3834;
    }

    public static class Debuffs
    {
        public const ushort
            Placeholder = 0;
    }

    public static class Levels
    {
        public const byte
            Maim = 4,
            Berserk = 6,
            Defiance = 10,
            StormsPath = 26,
            ThrillOfBattle = 30,
            InnerBeast = 35,
            MythrilTempest = 40,
            StormsEye = 50,
            Infuriate = 50,
            FellCleave = 54,
            RawIntuition = 56,
            Equilibrium = 58,
            Decimate = 60,
            InnerRelease = 70,
            MythrilTempestTrait = 74,
            NascentFlash = 76,
            InnerChaos = 80,
            Bloodwhetting = 82,
            PrimalRend = 90,
            PrimalWrath = 96,
            PrimalRuination = 100;
    }
}

internal class WarriorStormsPathCombo : CustomCombo
{
    protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.WarriorStormsPathCombo;

    protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
    {
        if (actionID == WAR.StormsPath)
        {
            if (lastComboMove == WAR.Maim && level >= WAR.Levels.StormsPath)
            {
                return WAR.StormsPath;
            }

            if (lastComboMove == WAR.HeavySwing && level >= WAR.Levels.Maim)
            {
                return WAR.Maim;
            }

            return WAR.HeavySwing;
        }

        return actionID;
    }
}

internal class WarriorStormsEyeCombo : CustomCombo
{
    protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.WarriorStormsEyeCombo;

    protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
	{

		if (actionID == WAR.StormsEye)
		{
			if (lastComboMove == WAR.Maim && level >= WAR.Levels.StormsEye)
			{
				return WAR.StormsEye;
			}

			if (lastComboMove == WAR.HeavySwing && level >= WAR.Levels.Maim)
                {
                    return WAR.Maim;
                }

                return WAR.HeavySwing;
        }

        return actionID;
    }
}

internal class WarriorMythrilTempestCombo : CustomCombo
{
    protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.WarriorMythrilTempestCombo;

    protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
    {
        if (actionID == WAR.MythrilTempest)
        {
            if (lastComboMove == WAR.Overpower && level >= WAR.Levels.MythrilTempest)
            {
                return WAR.MythrilTempest;
            }

            return WAR.Overpower;
        }

        return actionID;
    }
}
