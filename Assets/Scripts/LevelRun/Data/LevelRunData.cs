using System;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// The data that is needed for level run
/// </summary>
[Serializable]
public class LevelRunData {

    [SerializeField]
    private float lengthInMeters;
    [SerializeField]
    private List<UnitSpawnData> defendingTeamUnitSpawnData;
    [SerializeField]
    private List<LootItemSpawnData> lootSpawnData;

    public float LengthInMeters { get { return this.lengthInMeters; } }
    public IList<UnitSpawnData> DefendingTeamUnitSpawnData { get { return this.defendingTeamUnitSpawnData; } }
    public IList<LootItemSpawnData> LootSpawnData { get { return this.lootSpawnData; } }

    public LevelRunData(float lengthInMeters, List<UnitSpawnData> defendingTeamUnitSpawnData, List<LootItemSpawnData> lootSpawnData) {
        if (lengthInMeters <= 0f) {
            throw new ArgumentOutOfRangeException("lengthInMeters", "Cannot be zero or less.");
        }

        if (defendingTeamUnitSpawnData == null) {
            throw new ArgumentNullException("defendingTeamUnitSpawnData");
        }

        if (lootSpawnData == null) {
            throw new ArgumentNullException("lootSpawnData");
        }

        this.lengthInMeters = lengthInMeters;
        this.defendingTeamUnitSpawnData = defendingTeamUnitSpawnData;
        this.lootSpawnData = lootSpawnData;
    }
}
