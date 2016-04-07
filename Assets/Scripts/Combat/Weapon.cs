using System;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : ITickable {

    /// <summary>
    /// Event that is thrown when the weapon is ready to fire
    /// </summary>
    public event Action<Weapon> ReadyToFire;

    /// <summary>
    /// Event that is thrown when the weapon has fired
    /// </summary>
    public event Action<Weapon> WeaponFired;

    /// <summary>
    /// Event that is thrown when a bullet is fired
    /// </summary>
    public event Action<Bullet> BulletFired;

    public bool IsMeleeWeapon { get { return this.weaponSettings.IsMeleeWeapon; } }
    public Unit Owner { get; private set; }
    public int NumberOfBullets { get; private set; }

    private WeaponSettings weaponSettings;
    private float elapsedTimeFromLastFiring;
    private bool isReadyToFire;

    public Weapon(WeaponSettings weaponSettings, Unit owner) {
        if (weaponSettings == null) {
            throw new ArgumentNullException("weaponSettings");
        }

        if (owner == null) {
            throw new ArgumentNullException("owner");
        }

        this.weaponSettings = weaponSettings;
        this.Owner = owner;
        this.elapsedTimeFromLastFiring = 0f;
        this.NumberOfBullets = this.weaponSettings.NumberOfStartingBullets;
    }

    /// <summary>
    /// Order the weapon to fire
    /// </summary>
    /// <param name="possibleTargetUnits"></param>
    public void Fire(IList<Unit> possibleTargetUnits) {
        if (!this.isReadyToFire) {
            throw new InvalidOperationException("The weapon wasn't ready to fire.");
        }

        this.elapsedTimeFromLastFiring = 0f;
        this.isReadyToFire = false;
        if (this.weaponSettings.AmmunitionType == AmmunitionType.LimitedAmmunition) {
            this.NumberOfBullets--;
            if (this.NumberOfBullets < 0) {
                throw new InvalidOperationException("The weapon doesn't have ammunition to fire");
            }
        }

        if (this.WeaponFired != null) {
            this.WeaponFired(this);
        }

        //TODO should weapon be responsible for taking damage?
        //TODO should we introduce direction of melee weapon
        if (this.weaponSettings.IsMeleeWeapon) {
            for (int i = 0; i < possibleTargetUnits.Count; i++) {
                if (IsPositionInRange(possibleTargetUnits[i].PositionInMeters)) {
                    possibleTargetUnits[i].TakeDamage(this.weaponSettings.DamagePerHit);
                }
            }
        }

        if (!this.weaponSettings.IsMeleeWeapon) {
            Vector2 bulletStartPosition = this.Owner.PositionInMeters + new Vector2(0f, this.Owner.WeaponMountYOffset);
            //TODO calculate the bullet direction properly - decide direction for touch position or automatic
            Vector2 bulletDirection = (this.Owner.UnitType == UnitType.HeroUnit) ? Vector2.right : Vector2.left;
            Bullet bullet = new Bullet(this.weaponSettings.DamagePerHit, this.weaponSettings.BulletSpeed, bulletStartPosition, bulletDirection, possibleTargetUnits);
            if (this.BulletFired != null) {
                this.BulletFired(bullet);
            }
        }


    }

    public void Tick(float deltaTime) {
        this.elapsedTimeFromLastFiring += deltaTime;
        if (!this.isReadyToFire && this.elapsedTimeFromLastFiring >= this.weaponSettings.TimeBetweenShots && this.HasAmmunitionToFire()) {
            this.isReadyToFire = true;
            if (this.ReadyToFire != null) {
                this.ReadyToFire(this);
            }
        }
    }

    public void OnTickingFinished() {
    }

    public bool IsPositionInRange(Vector2 positionInMeters) {
        float distanceToPosition = Vector2.Distance(this.Owner.PositionInMeters, positionInMeters);
        if (distanceToPosition > this.weaponSettings.RangeInMeters) {
            return false;
        }

        PlatformManager.PlatformResult positionOnPlatformResult = PlatformManager.IsPositionOnPlatform(positionInMeters);
        if (!positionOnPlatformResult.Successful) {
            return false;
        }

        PlatformManager.PlatformResult ownerPlatformResult = PlatformManager.IsPositionOnPlatform(this.Owner.PositionInMeters);
        if (!ownerPlatformResult.Successful) {
            return false;
        }

        return positionOnPlatformResult.PlatformType == ownerPlatformResult.PlatformType;
    }

    public void AddAmmunition(int amount) {
        if (amount <= 0) {
            throw new ArgumentOutOfRangeException("amount", "Cannot be zero or less.");
        }

        if (this.weaponSettings.AmmunitionType == AmmunitionType.UnlimitedAmmunition) {
            return;
        }

        this.NumberOfBullets += amount;
    }

    private bool HasAmmunitionToFire() {
        return this.weaponSettings.AmmunitionType == AmmunitionType.UnlimitedAmmunition || (this.weaponSettings.AmmunitionType == AmmunitionType.LimitedAmmunition && this.NumberOfBullets > 0);
    }
}