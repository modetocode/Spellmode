using System;
/// <summary>
/// The settings data of one weapon.
/// </summary>
public class WeaponSettings {
    public bool IsMeleeWeapon { get; private set; }
    public float DamagePerHit { get; private set; }
    public float TimeBetweenShots { get; private set; }
    public float BulletSpeed { get; private set; }
    public float RangeInMeters { get; private set; }

    public WeaponSettings(bool isMeleeWeapon, float damagePerHit, float timeBetweenShots, float bulletSpeed, float rangeInMeters) {
        if (timeBetweenShots <= 0) {
            throw new ArgumentOutOfRangeException("timeBetweenShots", "Cannot be zero or less.");
        }

        if (bulletSpeed <= 0) {
            throw new ArgumentOutOfRangeException("bulletSpeed", "Cannot be zero or less.");
        }

        if (rangeInMeters <= 0) {
            throw new ArgumentOutOfRangeException("rangeInMeters", "Cannot be zero or less.");
        }

        this.IsMeleeWeapon = isMeleeWeapon;
        this.DamagePerHit = damagePerHit;
        this.TimeBetweenShots = timeBetweenShots;
        this.BulletSpeed = bulletSpeed;
        this.RangeInMeters = rangeInMeters;
    }
}