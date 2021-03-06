﻿using System;
using System.Collections.Generic;

/// <summary>
/// Responsible for controlling the combat - finding the targets for weapon fire and triggering weapon fire
/// </summary>
public class CombatManager : ITickable {

    private Team attackingTeam;
    private Team defendingTeam;
    private IList<Unit> unitsReadyToFire;

    public CombatManager(Team attackingTeam, Team defendingTeam) {
        if (attackingTeam == null) {
            throw new ArgumentNullException("attackingTeam");
        }

        if (defendingTeam == null) {
            throw new ArgumentNullException("defendingTeam");
        }

        this.attackingTeam = attackingTeam;
        this.defendingTeam = defendingTeam;
        this.unitsReadyToFire = new List<Unit>();

        for (int i = 0; i < this.attackingTeam.UnitsInTeam.Count; i++) {
            this.SubscribeForWeaponFire(this.attackingTeam.UnitsInTeam[i]);
        }

        for (int i = 0; i < this.defendingTeam.UnitsInTeam.Count; i++) {
            this.SubscribeForWeaponFire(this.defendingTeam.UnitsInTeam[i]);
        }

        this.attackingTeam.UnitAdded += SubscribeForWeaponFire;
        this.defendingTeam.UnitAdded += SubscribeForWeaponFire;
    }

    private void SubscribeForWeaponFire(Unit unit) {
        if (unit.Weapon == null) {
            throw new InvalidOperationException("Unit don't have a weapon");
        }

        unit.Weapon.ReadyToFire += AddReadyToFireWeapon;
        unit.Died += OnUnitDiedHandler;
    }

    private void AddReadyToFireWeapon(Weapon weapon) {
        this.unitsReadyToFire.Add(weapon.Owner);
    }

    private void OnUnitDiedHandler(Unit unit) {
        unit.Died -= OnUnitDiedHandler;
        if (this.unitsReadyToFire.Contains(unit)) {
            this.unitsReadyToFire.Remove(unit);
        }

        this.UnsubsribeFromUnitReadyToFire(unit);
    }

    private void UnsubsribeFromUnitReadyToFire(Unit unit) {
        unit.Weapon.ReadyToFire -= AddReadyToFireWeapon;
    }

    public void Tick(float deltaTime) {
        this.FireAllReadyUnits(onlyAutoAttackingUnits: true, fireOnlyWhenThereIsATargetInRange: true);
    }

    public void OnTickingFinished() {
        for (int i = 0; i < this.attackingTeam.AliveUnitsInTeam.Count; i++) {
            this.UnsubsribeFromUnitReadyToFire(this.attackingTeam.AliveUnitsInTeam[i]);
        }

        for (int i = 0; i < this.defendingTeam.AliveUnitsInTeam.Count; i++) {
            this.UnsubsribeFromUnitReadyToFire(this.defendingTeam.AliveUnitsInTeam[i]);
        }
    }

    /// <summary>
    /// All of the units that have manual attack and are ready to fire will fire their weapon
    /// </summary>
    public void TriggerManualAttack() {
        this.FireAllReadyUnits(onlyAutoAttackingUnits: false, fireOnlyWhenThereIsATargetInRange: false);
    }

    private void FireAllReadyUnits(bool onlyAutoAttackingUnits, bool fireOnlyWhenThereIsATargetInRange) {
        IList<Unit> unitsNotReadyToFire = new List<Unit>();
        while (this.unitsReadyToFire.Count > 0) {
            Unit unitToFire = this.unitsReadyToFire[0];
            this.unitsReadyToFire.RemoveAt(0);
            if (unitToFire.HasAutoAttack != onlyAutoAttackingUnits) {
                unitsNotReadyToFire.Add(unitToFire);
                continue;
            }

            bool hasFired = this.Fire(unitToFire: unitToFire, fireOnlyWhenThereIsATargetInRange: fireOnlyWhenThereIsATargetInRange);
            if (!hasFired) {
                unitsNotReadyToFire.Add(unitToFire);
            }
        }

        // Check if some unit has died prior to firing and was passed before
        for (int i = 0; i < unitsNotReadyToFire.Count; i++) {
            if (!unitsNotReadyToFire[i].IsAlive) {
                unitsNotReadyToFire.RemoveAt(i);
                i--;
            }
        }

        this.unitsReadyToFire = unitsNotReadyToFire;
    }

    private bool Fire(Unit unitToFire, bool fireOnlyWhenThereIsATargetInRange) {
        Weapon unitWeapon = unitToFire.Weapon;
        IList<Unit> possibleTargets;
        if (this.attackingTeam.IsUnitInTeam(unitToFire)) {
            possibleTargets = this.defendingTeam.AliveUnitsInTeam;
        }
        else {
            possibleTargets = this.attackingTeam.AliveUnitsInTeam;
        }

        if (fireOnlyWhenThereIsATargetInRange) {
            for (int i = 0; i < possibleTargets.Count; i++) {
                if (unitWeapon.IsPositionInRange(possibleTargets[i].PositionInMeters)) {
                    unitWeapon.Fire(possibleTargets);
                    return true;
                }
            }

            return false;
        }

        unitWeapon.Fire(possibleTargets);
        return true;
    }
}
