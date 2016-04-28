using System;

/// <summary>
/// Responsible for calculation of unit stats that will increase based on the level of the stat.
/// </summary>
public static class UnitStatsCalculator {

    public static float CalculateMaxHealthValue(UnitType unitType, int healthUpgradeLevel) {
        if (healthUpgradeLevel < 1) {
            throw new ArgumentOutOfRangeException("healthUpgradeLevel", "Cannot be less than one.");
        }

        UnitProgressionData progressionData = GetProgressionData(unitType);
        return progressionData.BaseMaxHealth + progressionData.HealthIncreasePerLevel * (healthUpgradeLevel - 1);
    }

    public static float CalculateNextLevelMaxHealthIncreaseValue(UnitType unitType, int healthUpgradeLevel) {
        if (healthUpgradeLevel < 1) {
            throw new ArgumentOutOfRangeException("healthUpgradeLevel", "Cannot be less than one.");
        }

        UnitProgressionData progressionData = GetProgressionData(unitType);
        return progressionData.HealthIncreasePerLevel;
    }

    public static float CalculateDamagePerHitValue(UnitType unitType, int damageUpgradeLevel) {
        if (damageUpgradeLevel < 1) {
            throw new ArgumentOutOfRangeException("damageUpgradeLevel", "Cannot be less than one.");
        }

        UnitProgressionData progressionData = GetProgressionData(unitType);
        return progressionData.WeaponProgressionData.BaseDamagePerHit + progressionData.WeaponProgressionData.DamageIncreasePerLevel * (damageUpgradeLevel - 1);
    }

    public static float CalculateNextLevelDamageIncreaseValue(UnitType unitType, int damageUpgradeLevel) {
        if (damageUpgradeLevel < 1) {
            throw new ArgumentOutOfRangeException("damageUpgradeLevel", "Cannot be less than one.");
        }

        UnitProgressionData progressionData = GetProgressionData(unitType);
        return progressionData.WeaponProgressionData.DamageIncreasePerLevel;
    }

    public static int CalculateAmmunitionValue(UnitType unitType, int ammunitionUpgradeLevel) {
        if (ammunitionUpgradeLevel < 1) {
            throw new ArgumentOutOfRangeException("ammunitionUpgradeLevel", "Cannot be less than one.");
        }

        UnitProgressionData progressionData = GetProgressionData(unitType);
        return progressionData.WeaponProgressionData.NumberOfStartingBullets + progressionData.WeaponProgressionData.NumberOfAdditionalBulletsPerLevel * (ammunitionUpgradeLevel - 1);
    }

    public static int CalculateNextLevelAmmunitionIncreaseValue(UnitType unitType, int ammunitionUpgradeLevel) {
        if (ammunitionUpgradeLevel < 1) {
            throw new ArgumentOutOfRangeException("ammunitionUpgradeLevel", "Cannot be less than one.");
        }

        UnitProgressionData progressionData = GetProgressionData(unitType);
        return progressionData.WeaponProgressionData.NumberOfAdditionalBulletsPerLevel;
    }

    private static UnitProgressionData GetProgressionData(UnitType unitType) {
        return GameMechanicsManager.GetUnitProgressionData(unitType);
    }

}
