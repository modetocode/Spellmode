﻿using System;
using System.Collections.Generic;

public class LevelRunManager : ITickable {

    /// <summary>
    /// Event that is thrown when the game is paused
    /// </summary>
    public event Action GamePaused;

    /// <summary>
    /// Event that is thrown when the game is resumed from pause
    /// </summary>
    public event Action GameResumed;

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

    public CombatManager CombatManager { get; private set; }
    public BulletManager BulletManager { get; private set; }
    public LootItemManager LootItemManager { get; private set; }
    private Ticker Ticker { get; set; }
    private Spawner Spawner { get; set; }
    private LevelRunData LevelRunData { get; set; }
    private ProgressTracker ProgressTracker { get; set; }
    private LevelRunModel LevelRunModel { get { return LevelRunModel.Instance; } }

    public void InitializeRun() {
        //TODO get the appropriate attacking team data
        IList<UnitSpawnData> attackingTeamSpawnData = new UnitSpawnData[] {
            new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Bottom, positionOnPlatformInMeters: 0f, unitType: UnitType.HeroUnit, unitLevel: 1, unitHasAutoAttack: false),
        };

        this.LevelRunData = this.LevelRunModel.LevelRunData;
        if (this.LevelRunData == null) {
            throw new InvalidOperationException("Level Run data is not set.");
        }

        this.AttackingTeam = new Team();
        this.DefendingTeam = new Team();
        this.ProgressTracker = new ProgressTracker(this.AttackingTeam, this.LevelRunData.LengthInMeters);
        this.ProgressTracker.ProgressFinished += ProgressFinishedHandler;
        List<UnitSpawnData> spawnData = new List<UnitSpawnData>(attackingTeamSpawnData);
        spawnData.AddRange(this.LevelRunData.DefendingTeamUnitSpawnData);
        this.Spawner = new Spawner(this.ProgressTracker, spawnData, this.LevelRunData.LootSpawnData);
        this.Spawner.UnitSpawned += OnUnitSpawnedHandler;
        this.Spawner.LootItemSpawned += OnLootItemSpawnedHandler;
        this.CombatManager = new CombatManager(this.AttackingTeam, this.DefendingTeam);
        this.BulletManager = new BulletManager();
        this.LootItemManager = new LootItemManager();
        this.LootItemManager.LootItemCollected += OnLootItemCollectedHandler;
        this.AttackingTeam.AllUnitsDied += AllAttackingUnitsDiedHandler;
        this.AttackingTeam.UnitAdded += OnUnitAddedHandler;
        this.DefendingTeam.UnitAdded += OnUnitAddedHandler;
        this.IsGamePaused = false;
    }


    private void OnUnitAddedHandler(Unit unit) {
        unit.Weapon.BulletFired += BulletFiredHandler;
        unit.Died += OnUnitDiedHandler;
    }

    private void OnUnitDiedHandler(Unit unit) {
        unit.Died -= OnUnitDiedHandler;
        IList<LootItem> loot = BountyCalculator.GetLootForUnit(unit);
        if (loot.Count == 0) {
            return;
        }

        for (int i = 0; i < loot.Count; i++) {
            this.LootItemManager.AddLootItem(loot[i]);
        }
    }

    private void BulletFiredHandler(Bullet bullet) {
        this.BulletManager.AddBullet(bullet);
    }

    private void ProgressFinishedHandler() {
        this.ProgressTracker.ProgressFinished -= ProgressFinishedHandler;
        this.FinishRun();
    }

    private void AllAttackingUnitsDiedHandler() {
        this.AttackingTeam.AllUnitsDied -= AllAttackingUnitsDiedHandler;
        this.FinishRun();
    }

    private void OnLootItemCollectedHandler(LootItem lootItem) {
        if (lootItem.Type == LootItemType.Ammunition) {
            for (int i = 0; i < this.AttackingTeam.AliveUnitsInTeam.Count; i++) {
                this.AttackingTeam.AliveUnitsInTeam[i].Weapon.AddAmmunition(lootItem.Amount);
            }
        }
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

        this.Ticker = new Ticker(new ITickable[] { this.AttackingTeam, this.DefendingTeam, this.ProgressTracker, this.Spawner, this.CombatManager, this.BulletManager });
    }

    public void Tick(float deltaTime) {
        this.Ticker.Tick(deltaTime);
    }

    public void PauseGame() {
        if (this.IsGamePaused) {
            throw new InvalidOperationException("The game is already paused");
        }

        this.IsGamePaused = true;
        if (this.GamePaused != null) {
            this.GamePaused();
        }
    }

    public void ResumeGame() {
        if (!this.IsGamePaused) {
            throw new InvalidOperationException("The game was not paused");
        }

        this.IsGamePaused = false;
        if (this.GameResumed != null) {
            this.GameResumed();
        }
    }

    public void RestartGame() {
        //TODO do this properly when loading a level is done
        this.FinishRun();
    }

    public void OnTickingFinished() {
        for (int i = 0; i < this.AttackingTeam.UnitsInTeam.Count; i++) {
            this.AttackingTeam.UnitsInTeam[i].Weapon.BulletFired -= BulletFiredHandler;
            this.AttackingTeam.UnitsInTeam[i].Died -= OnUnitDiedHandler;
        }

        for (int i = 0; i < this.DefendingTeam.UnitsInTeam.Count; i++) {
            this.DefendingTeam.UnitsInTeam[i].Weapon.BulletFired -= BulletFiredHandler;
            this.DefendingTeam.UnitsInTeam[i].Died -= OnUnitDiedHandler;
        }

        this.Spawner.UnitSpawned -= OnUnitSpawnedHandler;
        this.Spawner.LootItemSpawned -= OnLootItemSpawnedHandler;
        this.LootItemManager.LootItemCollected -= OnLootItemCollectedHandler;
    }

    private void OnLootItemSpawnedHandler(LootItem lootItem) {
        this.LootItemManager.AddLootItem(lootItem);
    }

    private void OnUnitSpawnedHandler(Unit unit) {
        //TODO should we check with the hardcoded unit type?
        Team unitTeam = unit.UnitType == UnitType.HeroUnit ? this.AttackingTeam : this.DefendingTeam;
        unitTeam.AddUnit(unit);
    }
}