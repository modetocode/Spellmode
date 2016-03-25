using System;
using System.Collections.Generic;

public class LevelRunManager : ITickable {

    /// <summary>
    /// Event that is thrown when the run is finished
    /// </summary>
    public event Action RunFinished;

    public Team AttackingTeam { get; private set; }
    public Team DefendingTeam { get; private set; }
    public bool IsGamePaused { get; private set; }

    public float CurrentProgressInMeters {
        get {
            if (this.ProgressTracker == null) {
                return 0f;
            }

            return this.ProgressTracker.CurrentProgressInMeters;
        }
    }
    public float LevelLengthInMeters {
        get {
            if (this.ProgressTracker == null) {
                return 0f;
            }

            return this.ProgressTracker.LevelLengthInMeters;
        }
    }

    private Ticker Ticker { get; set; }
    private UnitSpawner UnitSpawner { get; set; }
    private LevelRunData LevelRunData { get; set; }
    private ProgressTracker ProgressTracker { get; set; }
    private CombatManager CombatManager { get; set; }
    private BulletManager BulletManager { get; set; }

    public void InitializeRun() {
        //TODO get the appropriate data and set it
        this.LevelRunData = new LevelRunData(1, 1000f, new UnitSpawnData[0]);
        IList<UnitSpawnData> attackingTeamSpawnData = new UnitSpawnData[1];
        attackingTeamSpawnData[0] = new UnitSpawnData(Constants.Platforms.PlatformType.Bottom, 0f, new UnitSettings(movementSpeed: 10f, jumpSpeed: 10f, maxHealth: 100f), new WeaponSettings(isMeleeWeapon: false, damagePerHit: 10f, timeBetweenShots: 0.2f, bulletSpeed: 30f));
        this.AttackingTeam = new Team();
        this.DefendingTeam = new Team();
        this.ProgressTracker = new ProgressTracker(this.AttackingTeam, this.LevelRunData.LengthInMeters);
        this.ProgressTracker.ProgressFinished += ProgressFinishedHandler;
        this.UnitSpawner = new UnitSpawner(this.ProgressTracker, this.AttackingTeam, this.DefendingTeam, attackingTeamSpawnData, this.LevelRunData.DefendingTeamUnitSpawnData);
        this.CombatManager = new CombatManager(this.AttackingTeam, this.DefendingTeam);
        this.BulletManager = new BulletManager();
        this.AttackingTeam.UnitAdded += SubscribeForBulletsSpawn;
        this.DefendingTeam.UnitAdded += SubscribeForBulletsSpawn;
        this.IsGamePaused = false;
    }

    private void SubscribeForBulletsSpawn(Unit unit) {
        unit.Weapon.BulletFired += BulletFiredHandler;
    }

    private void BulletFiredHandler(Bullet bullet) {
        this.BulletManager.AddBullet(bullet);
    }

    private void ProgressFinishedHandler() {
        this.ProgressTracker.ProgressFinished -= ProgressFinishedHandler;
        this.FinishRun();
    }

    private void FinishRun() {
        this.Ticker.FinishTicking();
        if (this.RunFinished != null) {
            this.RunFinished();
        }
    }

    public void StartRun() {
        if (this.Ticker != null) {
            throw new InvalidOperationException("The run is already started");
        }

        this.Ticker = new Ticker(new ITickable[] { this.AttackingTeam, this.DefendingTeam, this.ProgressTracker, this.UnitSpawner, this.CombatManager, this.BulletManager });
    }

    public void Tick(float deltaTime) {
        this.Ticker.Tick(deltaTime);
    }

    public void PauseGame() {
        if (this.IsGamePaused) {
            throw new InvalidOperationException("The game is already paused");
        }
        this.IsGamePaused = true;
    }

    public void ResumeGame() {
        if (!this.IsGamePaused) {
            throw new InvalidOperationException("The game was not paused");
        }

        this.IsGamePaused = false;
    }

    public void OnTickingFinished() {
        for (int i = 0; i < this.AttackingTeam.UnitsInTeam.Count; i++) {
            this.AttackingTeam.UnitsInTeam[i].Weapon.BulletFired -= BulletFiredHandler;
        }

        for (int i = 0; i < this.DefendingTeam.UnitsInTeam.Count; i++) {
            this.DefendingTeam.UnitsInTeam[i].Weapon.BulletFired -= BulletFiredHandler;
        }
    }
}