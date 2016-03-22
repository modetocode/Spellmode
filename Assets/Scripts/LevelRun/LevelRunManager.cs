using System;
using System.Collections.Generic;

public class LevelRunManager : ITickable {

    /// <summary>
    /// Event that is thrown when the run is finished
    /// </summary>
    public event Action RunFinished;

    public Team AttackingTeam { get; private set; }
    public Team DefendingTeam { get; private set; }

    private Ticker Ticker { get; set; }
    private ProgressTracker ProgressTracker { get; set; }
    private UnitSpawner UnitSpawner { get; set; }
    private LevelRunData LevelRunData { get; set; }

    public void InitializeRun() {
        //TODO get the appropriate data and set it
        this.LevelRunData = new LevelRunData(1, 150f, new UnitSpawnData[0]);
        IList<UnitSpawnData> attackingTeamSpawnData = new UnitSpawnData[1];
        attackingTeamSpawnData[0] = new UnitSpawnData(Constants.Platforms.PlatformType.Bottom, 0f, new UnitSettings(movementSpeed: 10f, jumpSpeed: 10f, maxHealth: 100f));
        this.AttackingTeam = new Team();
        this.DefendingTeam = new Team();
        this.ProgressTracker = new ProgressTracker(this.AttackingTeam, this.LevelRunData.LengthInMeters);
        this.ProgressTracker.ProgressFinished += ProgressFinishedHandler;
        this.UnitSpawner = new UnitSpawner(this.ProgressTracker, this.AttackingTeam, this.DefendingTeam, attackingTeamSpawnData, this.LevelRunData.DefendingTeamUnitSpawnData);
    }

    private void ProgressFinishedHandler() {
        this.ProgressTracker.ProgressFinished -= ProgressFinishedHandler;
        this.FinishRun();
    }

    private void FinishRun() {
        if (this.RunFinished != null) {
            this.RunFinished();
        }
    }

    public void StartRun() {
        if (this.Ticker != null) {
            throw new System.InvalidOperationException("The run is already started");
        }

        this.Ticker = new Ticker(new ITickable[] { this.AttackingTeam, this.DefendingTeam, this.ProgressTracker, this.UnitSpawner });
    }

    public void Tick(float deltaTime) {
        this.Ticker.Tick(deltaTime);
    }

    public void PauseGame() {
        this.Ticker.PauseTicking();
    }

    public void ResumeGame() {
        this.Ticker.ResumeTicking();
    }

    public void OnTickingPaused(float deltaTime) {
    }
}