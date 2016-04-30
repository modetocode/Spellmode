using System;
using UnityEngine;

/// <summary>
/// Represents the data that is needed for spawning of one unit.
/// </summary>
[Serializable]
public class UnitSpawnData : SpawnData {

    [SerializeField]
    private UnitType unitType;
    [SerializeField]
    private UnitLevelData unitLevelData;
    [SerializeField]
    private bool unitHasAutoAttack;

    public UnitType UnitType { get { return this.unitType; } }
    public UnitLevelData UnitLevelData { get { return this.unitLevelData; } }
    public bool UnitHasAutoAttack { get { return this.unitHasAutoAttack; } }

    public UnitSpawnData(Constants.Platforms.PlatformType platformType, float positionOnPlatformInMeters, UnitType unitType, UnitLevelData unitLevelData, bool unitHasAutoAttack = true)
    : base(platformType, positionOnPlatformInMeters) {
        if (unitLevelData == null) {
            throw new ArgumentNullException("unitLevelData");
        }

        this.unitType = unitType;
        this.unitLevelData = unitLevelData;
        this.unitHasAutoAttack = unitHasAutoAttack;
    }
}