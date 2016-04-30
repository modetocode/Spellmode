using System;
using UnityEngine;

/// <summary>
/// Base class that contains data for one spawn object
/// </summary>
[Serializable]
public abstract class SpawnData {

    [SerializeField]
    private Constants.Platforms.PlatformType platformType;
    [SerializeField]
    private float positionOnPlatformInMeters;

    public Constants.Platforms.PlatformType PlatformType { get { return this.platformType; } }
    public float PositionOnPlatformInMeters { get { return this.positionOnPlatformInMeters; } }

    protected SpawnData(Constants.Platforms.PlatformType platformType, float positionOnPlatformInMeters) {
        if (positionOnPlatformInMeters < 0) {
            throw new ArgumentOutOfRangeException("positionOnPlatformInMeters", "Cannot be less than zero.");
        }

        this.platformType = platformType;
        this.positionOnPlatformInMeters = positionOnPlatformInMeters;
    }
}
