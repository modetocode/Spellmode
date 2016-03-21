public class UnitSpawnData {

    public Constants.Platforms.PlatformType PlatformType { get; private set; }
    public float PositionOnPlatformInMeters { get; private set; }
    public UnitSettings UnitSettings { get; private set; }

    public UnitSpawnData(Constants.Platforms.PlatformType platformType, float positionOnPlatformInMeters, UnitSettings unitSettings) {
        //TODO arg check
        this.PlatformType = platformType;
        this.PositionOnPlatformInMeters = positionOnPlatformInMeters;
        this.UnitSettings = unitSettings;
    }
}