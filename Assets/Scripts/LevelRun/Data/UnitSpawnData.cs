public class UnitSpawnData {

    public Constants.Platforms.PlatformType PlatformType { get; private set; }
    public float PositionOnPlatformInMeters { get; private set; }
    public UnitSettings UnitSettings { get; private set; }
    //TODO check if this is the right place for weapon settings
    public WeaponSettings WeaponSettings { get; private set; }
    public bool UnitHasAutoAttack { get; private set; }

    public UnitSpawnData(Constants.Platforms.PlatformType platformType, float positionOnPlatformInMeters, UnitSettings unitSettings, WeaponSettings weaponSettings, bool unitHasAutoAttack) {
        //TODO arg check
        this.PlatformType = platformType;
        this.PositionOnPlatformInMeters = positionOnPlatformInMeters;
        this.UnitSettings = unitSettings;
        this.WeaponSettings = weaponSettings;
        this.UnitHasAutoAttack = unitHasAutoAttack;
    }
}