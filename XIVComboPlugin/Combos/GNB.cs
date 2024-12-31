using System.Data.Common;
using Dalamud.Game.ClientState.JobGauge.Types;
using System;

namespace XIVCombo.Combos;

internal static class GNB
{
    public const byte JobID = 37;

    public const uint
        KeenEdge = 16137,
        NoMercy = 16138,
        BrutalShell = 16139,
        DemonSlice = 16141,
        RoyalGuard = 16142,
        LightningShot = 16143,
        DangerZone = 16144,
        SolidBarrel = 16145,
        GnashingFang = 16146,
        SavageClaw = 16147,
        DemonSlaughter = 16149,
        WickedTalon = 16150,
        SonicBreak = 16153,
        Trajectory = 36934,
        Continuation = 16155,
        JugularRip = 16156,
        AbdomenTear = 16157,
        EyeGouge = 16158,
        BowShock = 16159,
        BurstStrike = 16162,
        FatedCircle = 16163,
        Bloodfest = 16164,
        Hypervelocity = 25759,
        DoubleDown = 25760,
        RoyalGuardRemoval = 32068,
        FatedBrand = 36936,
        ReignOfBeasts = 36937,
        NobleBlood = 36938,
        LionHeart = 36939;

    public static class Buffs
    {
        public const ushort
            NoMercy = 1831,
            RoyalGuard = 1833,
            ReadyToRip = 1842,
            ReadyToTear = 1843,
            ReadyToGouge = 1844,
            ReadyToBlast = 2686,
            ReadyToFated = 3839,
            ReadyToReign = 3840,
            ReadyToBreak = 3886;
    }

    public static class Debuffs
    {
        public const ushort
            BowShock = 1838;
    }

    public static class Levels
    {
        public const byte
            NoMercy = 2,
            BrutalShell = 4,
            RoyalGuard = 10,
            DangerZone = 18,
            SolidBarrel = 26,
            BurstStrike = 30,
            DemonSlaughter = 40,
            SonicBreak = 54,
            Trajectory = 56,
            GnashingFang = 60,
            BowShock = 62,
            Continuation = 70,
            FatedCircle = 72,
            Bloodfest = 76,
            EnhancedContinuation = 86,
            CartridgeCharge2 = 88,
            DoubleDown = 90,
            ReignOfBeasts = 100;
    }
}

internal class GunbreakerSolidBarrel : CustomCombo
{
    protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.GunbreakerSolidBarrelCombo;

    protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
    {
        if (actionID == GNB.SolidBarrel)
        {
            if (comboTime > 0)
            {
                if (lastComboMove == GNB.BrutalShell && level >= GNB.Levels.SolidBarrel)
                {
                    return GNB.SolidBarrel;
                }

                if (lastComboMove == GNB.KeenEdge && level >= GNB.Levels.BrutalShell)
                    return GNB.BrutalShell;
            }

            return GNB.KeenEdge;
        }

        return actionID;
    }
}

internal class GunbreakerGnashingFang : CustomCombo
{
    protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.GunbreakerGnashingFangCont;

    protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
    {
        if (actionID == GNB.GnashingFang)
        {
            if (level >= GNB.Levels.Continuation)
            {
                if (HasEffect(GNB.Buffs.ReadyToGouge))
                    return GNB.EyeGouge;

                if (HasEffect(GNB.Buffs.ReadyToTear))
                    return GNB.AbdomenTear;

                if (HasEffect(GNB.Buffs.ReadyToRip))
                    return GNB.JugularRip;
            }

            // Gnashing Fang > Savage Claw > Wicked Talon
            return OriginalHook(GNB.GnashingFang);
        }

        return actionID;
    }
}

internal class GunbreakerBurstStrikeFatedCircle : CustomCombo
{
    protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.GnbAny;

    protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
    {
        if (actionID == GNB.BurstStrike)
        {
            if (IsEnabled(CustomComboPreset.GunbreakerBurstStrikeCont))
            {
                if (level >= GNB.Levels.EnhancedContinuation && HasEffect(GNB.Buffs.ReadyToBlast) && CanUseAction(OriginalHook(GNB.Hypervelocity)))
                    return GNB.Hypervelocity;
            }
        }

        if (actionID == GNB.FatedCircle)
        {
            if (IsEnabled(CustomComboPreset.GunbreakerFatedCircleCont))
            {
                if (level >= GNB.Levels.EnhancedContinuation && HasEffect(GNB.Buffs.ReadyToFated))
                    return GNB.FatedBrand;
            }
        }

        return actionID;
    }
}


internal class GunbreakerDemonSlaughter : CustomCombo
{
    protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.GunbreakerDemonSlaughterCombo;

    protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
    {
        if (actionID == GNB.DemonSlaughter)
        {

            if (comboTime > 0 && lastComboMove == GNB.DemonSlice && level >= GNB.Levels.DemonSlaughter)
            {
                return GNB.DemonSlaughter;
            }

            return GNB.DemonSlice;
        }

        return actionID;
    }
}
