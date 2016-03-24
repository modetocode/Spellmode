/// <summary>
/// The settings data of one weapon.
/// </summary>
public class WeaponSettings {
    public bool IsMeleeWeapon { get; private set; }
    public float DamagePerHit { get; private set; }
    public float TimeBetweenShots { get; private set; }
    public float BulletSpeed { get; private set; }

    public WeaponSettings(bool isMeleeWeapon, float damagePerHit, float timeBetweenShots, float bulletSpeed) {
        //TODO arg check
        this.IsMeleeWeapon = isMeleeWeapon;
        this.DamagePerHit = damagePerHit;
        this.TimeBetweenShots = timeBetweenShots;
        this.BulletSpeed = bulletSpeed;
    }
}