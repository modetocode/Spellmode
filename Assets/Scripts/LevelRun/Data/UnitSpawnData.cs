using System;

/// <summary>
/// Represents the data that is needed for spawning of one unit.
/// </summary>
public class UnitSpawnData : SpawnData {

    public UnitType UnitType { get; private set; }
    public UnitLevelData UnitLevelData { get; private set; }
    public bool UnitHasAutoAttack { get; private set; }

    public UnitSpawnData(Constants.Platforms.PlatformType platformType, float positionOnPlatformInMeters, UnitType unitType, UnitLevelData unitLevelData, bool unitHasAutoAttack)
    : base(platformType, positionOnPlatformInMeters) {
        if (unitLevelData == null) {
            throw new ArgumentNullException("unitLevelData");
        }

        this.UnitType = unitType;
        this.UnitLevelData = unitLevelData;
        this.UnitHasAutoAttack = unitHasAutoAttack;
    }
}