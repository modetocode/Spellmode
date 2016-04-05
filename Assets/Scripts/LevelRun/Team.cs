using System;
using System.Collections.Generic;

public class Team : ITickable {

    public event Action<Unit> UnitAdded;
    public event Action AllUnitsDied;

    public IList<Unit> UnitsInTeam { get; private set; }
    public IList<Unit> AliveUnitsInTeam { get; private set; }

    public Team() {
        this.UnitsInTeam = new List<Unit>();
        this.AliveUnitsInTeam = new List<Unit>();
    }

    public void Tick(float deltaTime) {
        for (int i = 0; i < this.UnitsInTeam.Count; i++) {
            this.UnitsInTeam[i].Tick(deltaTime);
        }
    }

    public void AddUnit(Unit unit) {
        if (unit == null) {
            throw new ArgumentNullException("unit");
        }

        this.UnitsInTeam.Add(unit);
        this.AliveUnitsInTeam.Add(unit);
        if (this.UnitAdded != null) {
            this.UnitAdded(unit);
        }

        unit.Died += OnDiedRemoveFromAliveList;
    }

    private void OnDiedRemoveFromAliveList(Unit deadUnit) {
        this.AliveUnitsInTeam.Remove(deadUnit);
        if (this.AliveUnitsInTeam.Count == 0) {
            if (this.AllUnitsDied != null) {
                this.AllUnitsDied();
            }
        }

        deadUnit.Died -= OnDiedRemoveFromAliveList;
    }

    public void MoveAllAliveUnitsToUpperPlatformIfPossible() {
        for (int i = 0; i < AliveUnitsInTeam.Count; i++) {
            this.AliveUnitsInTeam[i].MoveToUpperPlatformIfPossible();
        }
    }

    public void MoveAllAliveUnitsToLowerPlatformIfPossible() {
        for (int i = 0; i < AliveUnitsInTeam.Count; i++) {
            this.AliveUnitsInTeam[i].MoveToLowerPlatformIfPossible();
        }
    }

    public bool IsUnitInTeam(Unit unit) {
        return this.UnitsInTeam.Contains(unit);
    }

    public void OnTickingFinished() {
        for (int i = 0; i < this.AliveUnitsInTeam.Count; i++) {
            this.AliveUnitsInTeam[i].Died -= OnDiedRemoveFromAliveList;
        }
    }
}