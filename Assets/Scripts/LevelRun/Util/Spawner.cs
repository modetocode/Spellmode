using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Responsible for spawning objects (units/items etc.). Decision is made based on the progression of the attacking team and when new objects need to spawn.
/// </summary>
public class Spawner : ITickable {

    public event Action<Unit> UnitSpawned;
    public event Action<LootItem> LootItemSpawned;

    private ProgressTracker progressTracker;
    private readonly float distanceToTriggerObjectSpawnInMetters;
    private IList<UnitSpawnData> unitSpawnData;
    private IList<LootItemSpawnData> lootItemSpawnData;

    public Spawner(ProgressTracker progressTracker, IList<UnitSpawnData> unitSpawnData, IList<LootItemSpawnData> lootItemSpawnData) {
        if (progressTracker == null) {
            throw new ArgumentOutOfRangeException("progressTracker");
        }

        if (unitSpawnData == null) {
            throw new ArgumentOutOfRangeException("unitSpawnData");
        }

        if (lootItemSpawnData == null) {
            throw new ArgumentOutOfRangeException("lootItemSpawnData");
        }

        this.progressTracker = progressTracker;
        this.unitSpawnData = unitSpawnData.OrderBy(x => x.PositionOnPlatformInMeters).ToList();
        this.lootItemSpawnData = lootItemSpawnData.OrderBy(x => x.PositionOnPlatformInMeters).ToList();
        this.distanceToTriggerObjectSpawnInMetters = Constants.LevelRun.DistanceToTriggerObjectSpawnInMetters;
    }

    public void Tick(float deltaTime) {
        this.SpawnAllVisibleObjects();
    }

    private void SpawnAllVisibleObjects() {
        this.SpawnVisibleObjects<UnitSpawnData>(this.unitSpawnData, this.SpawnUnit);
        this.SpawnVisibleObjects<LootItemSpawnData>(this.lootItemSpawnData, this.SpawnLootItem);
    }

    private void SpawnUnit(UnitSpawnData unitSpawnData) {
        float unitSpawnYPosition = PlatformManager.GetYCoordinateForPlatform(unitSpawnData.PlatformType);
        Vector2 unitSpawnPosition = new Vector2(unitSpawnData.PositionOnPlatformInMeters, unitSpawnYPosition);
        Unit newUnit = UnitFactory.CreateUnit(unitSpawnData.UnitType, unitSpawnData.UnitLevel, unitSpawnPosition, unitSpawnData.UnitHasAutoAttack);
        if (this.UnitSpawned != null) {
            this.UnitSpawned(newUnit);
        }
    }

    private void SpawnLootItem(LootItemSpawnData lootItemSpawnData) {
        float lootItemSpawnYPosition = PlatformManager.GetYCoordinateForPlatform(lootItemSpawnData.PlatformType);
        Vector2 lootItemSpawnPosition = new Vector2(lootItemSpawnData.PositionOnPlatformInMeters, lootItemSpawnYPosition);
        LootItem newLootItem = new LootItem(lootItemSpawnData.LootItemType, lootItemSpawnData.LootItemAmount, lootItemSpawnPosition);
        if (this.LootItemSpawned != null) {
            this.LootItemSpawned(newLootItem);
        }
    }

    public void SpawnVisibleObjects<T>(IList<T> spawnData, Action<T> whenSpawnableAction) where T : SpawnData {
        float currentProgressPositionInMeters = this.progressTracker.CurrentProgressInMeters;
        float spawnablePosition = currentProgressPositionInMeters + distanceToTriggerObjectSpawnInMetters;

        for (int i = 0; i < spawnData.Count; i++) {
            if (spawnData[i].PositionOnPlatformInMeters <= spawnablePosition) {
                whenSpawnableAction(spawnData[i]);
                spawnData.RemoveAt(i);
                i--;
            }
            else {
                break;
            }
        }
    }

    public void OnTickingFinished() {
    }
}