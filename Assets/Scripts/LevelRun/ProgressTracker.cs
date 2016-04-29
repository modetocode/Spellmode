using System;
using System.Collections.Generic;

/// <summary>
/// Responsible for tracking the progression of a team towards completion of a level. 
/// </summary>
public class ProgressTracker : ITickable {

    /// <summary>
    /// Thrown when the tracking team has finished the progress.
    /// </summary>
    public event Action ProgressFinished;

    public float CurrentProgressInMeters { get; private set; }
    public float LevelLengthInMeters { get; private set; }
    private Team trackingTeam;

    public ProgressTracker(Team trackingTeam, float levelLengthInMeters) {
        if (trackingTeam == null) {
            throw new ArgumentNullException("attackingTeam");
        }

        if (levelLengthInMeters <= 0f) {
            throw new ArgumentOutOfRangeException("levelLengthInMeters", "Cannot be less or equal to zero");
        }

        this.trackingTeam = trackingTeam;
        this.LevelLengthInMeters = levelLengthInMeters;
        this.CurrentProgressInMeters = 0.0f;
    }

    public void Tick(float deltaTime) {
        IList<Unit> aliveUnits = this.trackingTeam.AliveUnitsInTeam;
        if (aliveUnits.Count == 0) {
            return;
        }

        this.UpdateProgressForAliveUnits(aliveUnits);
        if (this.CurrentProgressInMeters >= this.LevelLengthInMeters) {
            if (this.ProgressFinished != null) {
                this.ProgressFinished();
            }
        }
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

    public void OnTickingFinished() {
    }
}