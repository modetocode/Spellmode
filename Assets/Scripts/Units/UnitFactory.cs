using System;
using UnityEngine;

public static class UnitFactory {
    public static Unit CreateUnit(UnitType unitType, int unitLevel, Vector2 unitSpawnPosition, bool unitHasAutoAttack) {
        if (unitLevel < 1) {
            throw new ArgumentOutOfRangeException("unitLevel", "Cannot be less than one.");
        }

        UnitProgressionData progressionData = GameMechanicsManager.GetUnitProgressionData(unitType);
        UnitSettings unitSettings = GetUnitSettings(unitLevel, progressionData);
        WeaponSettings weaponSettings = GetWeaponSettings(unitLevel, progressionData.WeaponProgressionData);
        return new Unit(unitSettings, unitLevel, weaponSettings, unitSpawnPosition, unitHasAutoAttack);
    }

    private static UnitSettings GetUnitSettings(int unitLevel, UnitProgressionData progressionData) {
        UnitSettings unitSettings = new UnitSettings(
            unitType: progressionData.UnitType,
            movementSpeed: progressionData.MovementSpeed,
            jumpSpeed: progressionData.JumpSpeed,
            maxHealth: progressionData.BaseMaxHealth + progressionData.HealthIncreasePerLevel * (unitLevel - 1),
            weaponMountYOffset: progressionData.WeaponMountYOffset
            );

        return unitSettings;
    }

    private static WeaponSettings GetWeaponSettings(int unitLevel, WeaponProgressionData weaponProgressionData) {
        WeaponSettings weaponSettings = new WeaponSettings(
            isMeleeWeapon: weaponProgressionData.IsMeleeWeapon,
            damagePerHit: weaponProgressionData.BaseDamagePerHit + weaponProgressionData.DamageIncreasePerLevel * (unitLevel - 1),
            timeBetweenShots: weaponProgressionData.TimeBetweenShots,
            bulletSpeed: weaponProgressionData.BulletSpeed,
            rangeInMeters: weaponProgressionData.RangeInMeters
            );

        return weaponSettings;
    }
}