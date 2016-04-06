using System;

/// <summary>
/// Base class that contains data for one spawn object
/// </summary>
public abstract class SpawnData {
    public Constants.Platforms.PlatformType PlatformType { get; private set; }
    public float PositionOnPlatformInMeters { get; private set; }

    protected SpawnData(Constants.Platforms.PlatformType platformType, float positionOnPlatformInMeters) {
        if (positionOnPlatformInMeters < 0) {
            throw new ArgumentOutOfRangeException("positionOnPlatformInMeters", "Cannot be less than zero.");
        }

        this.PlatformType = platformType;
        this.PositionOnPlatformInMeters = positionOnPlatformInMeters;
    }
}
