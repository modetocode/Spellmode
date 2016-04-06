using System;

/// <summary>
/// Represents the data that is needed for spawning of one unit.
/// </summary>
public class UnitSpawnData : SpawnData {

    public UnitType UnitType { get; private set; }
    public int UnitLevel { get; private set; }
    public bool UnitHasAutoAttack { get; private set; }

    public UnitSpawnData(Constants.Platforms.PlatformType platformType, float positionOnPlatformInMeters, UnitType unitType, int unitLevel, bool unitHasAutoAttack)
    : base(platformType, positionOnPlatformInMeters) {

        if (unitLevel < 1) {
            throw new ArgumentOutOfRangeException("unitLevel", "Cannot be less than one.");
        }

        this.UnitType = unitType;
        this.UnitLevel = unitLevel;
        this.UnitHasAutoAttack = unitHasAutoAttack;
    }
}