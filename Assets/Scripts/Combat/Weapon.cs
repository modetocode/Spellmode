using System;
using System.Collections.Generic;
using UnityEngine;

public class Weapon {

    /// <summary>
    /// Event that is thrown when the weapon is ready to fire
    /// </summary>
    public event Action<Weapon> ReadyToFire;

    /// <summary>
    /// Event that is thrown when a bullet is fired
    /// </summary>
    public event Action<Bullet> BulletFired;

    private WeaponSettings weaponSettings;
    private Unit owner;

    public Weapon(WeaponSettings weaponSettings, Unit owner) {
        //TODO arg check
        this.weaponSettings = weaponSettings;
        this.owner = owner;
    }

    /// <summary>
    /// Order to fire at some possible target. If there is not a target in range then the weapon skips firing.
    /// </summary>
    /// <param name="possibleTargetUnits"></param>
    public void Fire(IList<Unit> possibleTargetUnits) {
        IList<Unit> unitsInRange = this.GetUnitsInRange(possibleTargetUnits);
        if (unitsInRange.Count == 0) {
            return;
        }

        Unit target = this.ChooseTarget(unitsInRange);
        if (this.weaponSettings.IsMeleeWeapon) {
            target.TakeDamage(this.weaponSettings.DamagePerHit);
        }
        else {
            //TODO check for starting positon
            Vector2 bulletStartPosition = this.owner.PositionInMeters;
            Bullet bullet = new Bullet(target, this.weaponSettings.DamagePerHit, this.weaponSettings.BulletSpeed, bulletStartPosition);
            if (this.BulletFired != null) {
                this.BulletFired(bullet);
            }
        }
    }

    private IList<Unit> GetUnitsInRange(IList<Unit> possibleTargetUnits) {
        //TODO implement this
        throw new NotImplementedException();
    }

    private Unit ChooseTarget(IList<Unit> unitsInRange) {
        //TODO which unit should be picked for target
        return unitsInRange[0];
    }
}