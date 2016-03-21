using System;
using System.Collections.Generic;

/// <summary>
/// Responsible for tracking the progression of the attacking team towards completion of a level. 
/// </summary>
public class ProgressTracker : ITickable {

    public float CurrentProgressInMeters { get; private set; }
    private Team attackingTeam;
    private float levelLengthInMeters;

    public ProgressTracker(Team attackingTeam, float levelLengthInMeters) {
        if (attackingTeam == null) {
            throw new ArgumentNullException("attackingTeam");
        }

        if (levelLengthInMeters <= 0f) {
            throw new ArgumentOutOfRangeException("levelLengthInMeters", "Cannot be less or equal to zero");
        }

        this.attackingTeam = attackingTeam;
        this.levelLengthInMeters = levelLengthInMeters;
        this.CurrentProgressInMeters = 0.0f;
    }

    public void Tick(float deltaTime) {
        IList<Unit> aliveUnits = this.attackingTeam.AliveUnitsInTeam;
        if (aliveUnits.Count == 0) {
            return;
        }

        this.UpdateProgressForAliveUnits(aliveUnits);
    }

    private void UpdateProgressForAliveUnits(IList<Unit> aliveUnits) {
        if (aliveUnits.Count == 0) {
            throw new System.InvalidOperationException("Cannot update position for 0 alive units.");
        }

        float averagePositionInMeters = 0f;
        for (int i = 0; i < aliveUnits.Count; i++) {
            averagePositionInMeters += aliveUnits[i].PositionInMeters.x;
        }

        averagePositionInMeters /= aliveUnits.Count;
        this.CurrentProgressInMeters = averagePositionInMeters;
    }
}