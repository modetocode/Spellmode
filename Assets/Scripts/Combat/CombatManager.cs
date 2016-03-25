using System;
using System.Collections.Generic;

/// <summary>
/// Responsible for controlling the combat - finding the targets for weapon fire and triggering weapon fire
/// </summary>
public class CombatManager : ITickable {
    private Team attackingTeam;
    private Team defendingTeam;

    public CombatManager(Team attackingTeam, Team defendingTeam) {
        if (attackingTeam == null) {
            throw new ArgumentNullException("attackingTeam");
        }

        if (defendingTeam == null) {
            throw new ArgumentNullException("defendingTeam");
        }

        this.attackingTeam = attackingTeam;
        this.defendingTeam = defendingTeam;
        this.attackingTeam.UnitAdded += SubscribeForWeaponFire;
        this.defendingTeam.UnitAdded += SubscribeForWeaponFire;
    }

    private void SubscribeForWeaponFire(Unit unit) {
        if (unit.Weapon == null) {
            throw new InvalidOperationException("Unit don't have a weapon");
        }

        Action<Weapon> readyToFireAction;
        if (attackingTeam.IsUnitInTeam(unit)) {
            readyToFireAction = this.FireAttackingUnitWeapon;
        }
        else {
            readyToFireAction = this.FireDefendingUnitWeapon;
        }

        unit.Weapon.ReadyToFire += readyToFireAction;
        unit.Died -= OnUnitDiedHandler;
    }

    private void OnUnitDiedHandler(Unit unit) {
        this.UnsubsribeFromUnitReadyToFire(unit);
    }

    private void FireAttackingUnitWeapon(Weapon weapon) {
        this.FireWeapon(weapon, this.defendingTeam.AliveUnitsInTeam);
    }

    private void FireDefendingUnitWeapon(Weapon weapon) {
        this.FireWeapon(weapon, this.attackingTeam.AliveUnitsInTeam);
    }

    private void FireWeapon(Weapon weapon, IList<Unit> targetUnits) {
        weapon.Fire(targetUnits);
    }

    private void UnsubsribeFromUnitReadyToFire(Unit unit) {
        unit.Weapon.ReadyToFire -= FireAttackingUnitWeapon;
        unit.Weapon.ReadyToFire -= FireDefendingUnitWeapon;
    }

    public void Tick(float deltaTime) {
    }

    public void OnTickingFinished() {
        for (int i = 0; i < this.attackingTeam.AliveUnitsInTeam.Count; i++) {
            this.UnsubsribeFromUnitReadyToFire(this.attackingTeam.AliveUnitsInTeam[i]);
        }

        for (int i = 0; i < this.defendingTeam.AliveUnitsInTeam.Count; i++) {
            this.UnsubsribeFromUnitReadyToFire(this.defendingTeam.AliveUnitsInTeam[i]);
        }
    }
}
