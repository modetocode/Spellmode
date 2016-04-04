using System;

/// <summary>
/// Represents the data that is needed for spawning of one unit.
/// </summary>
public class UnitSpawnData {

    public Constants.Platforms.PlatformType PlatformType { get; private set; }
    public float PositionOnPlatformInMeters { get; private set; }
    public UnitType UnitType { get; private set; }
    public int UnitLevel { get; private set; }
    public bool UnitHasAutoAttack { get; private set; }

    public UnitSpawnData(Constants.Platforms.PlatformType platformType, float positionOnPlatformInMeters, UnitType unitType, int unitLevel, bool unitHasAutoAttack) {
        if (positionOnPlatformInMeters < 0) {
            throw new ArgumentOutOfRangeException("positionOnPlatformInMeters", "Cannot be less than zero.");
        }

        if (unitLevel < 1) {
            throw new ArgumentOutOfRangeException("unitLevel", "Cannot be less than one.");
        }

        this.PlatformType = platformType;
        this.PositionOnPlatformInMeters = positionOnPlatformInMeters;
        this.UnitType = unitType;
        this.UnitLevel = unitLevel;
        this.UnitHasAutoAttack = unitHasAutoAttack;
    }
}