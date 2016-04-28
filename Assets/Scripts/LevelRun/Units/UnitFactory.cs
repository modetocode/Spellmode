using System;
using UnityEngine;

public static class UnitFactory {
    public static Unit CreateUnit(UnitType unitType, UnitLevelData unitLevelData, Vector2 unitSpawnPosition, bool unitHasAutoAttack) {
        if (unitLevelData == null) {
            throw new ArgumentNullException("unitLevelData");
        }

        UnitProgressionData progressionData = GameMechanicsManager.GetUnitProgressionData(unitType);
        UnitSettings unitSettings = GetUnitSettings(unitLevelData, progressionData);
        WeaponSettings weaponSettings = GetWeaponSettings(unitLevelData, unitType, progressionData.WeaponProgressionData);
        return new Unit(unitSettings, unitLevelData, weaponSettings, unitSpawnPosition, unitHasAutoAttack);
    }

    private static UnitSettings GetUnitSettings(UnitLevelData unitLevelData, UnitProgressionData progressionData) {
        UnitSettings unitSettings = new UnitSettings(
            unitType: progressionData.UnitType,
            movementSpeed: progressionData.MovementSpeed,
            jumpSpeed: progressionData.JumpSpeed,
            maxHealth: UnitStatsCalculator.CalculateMaxHealthValue(progressionData.UnitType, unitLevelData.HealthUpgradeLevel),
            weaponMountYOffset: progressionData.WeaponMountYOffset
        );

        return unitSettings;
    }

    private static WeaponSettings GetWeaponSettings(UnitLevelData unitLevelData, UnitType unitType, WeaponProgressionData weaponProgressionData) {
        WeaponSettings weaponSettings = new WeaponSettings(
            isMeleeWeapon: weaponProgressionData.IsMeleeWeapon,
            damagePerHit: UnitStatsCalculator.CalculateDamagePerHitValue(unitType, unitLevelData.DamageUpgradeLevel),
            timeBetweenShots: weaponProgressionData.TimeBetweenShots,
            bulletSpeed: weaponProgressionData.BulletSpeed,
            rangeInMeters: weaponProgressionData.RangeInMeters,
            ammunitionType: weaponProgressionData.AmmunitionType,
            numberOfStartingBullets: UnitStatsCalculator.CalculateAmmunitionValue(unitType, unitLevelData.AmmunitionUpgradeLevel)
        );

        return weaponSettings;
    }
}