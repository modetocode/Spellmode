using System;
using System.Collections.Generic;

public class Team : ITickable {

    public event Action<Unit> UnitAdded;

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

    internal void AddUnit(Unit unit) {
        //TODO arg check
        this.UnitsInTeam.Add(unit);
        this.AliveUnitsInTeam.Add(unit);
        if (this.UnitAdded != null) {
            this.UnitAdded(unit);
        }
    }
}