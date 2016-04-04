using UnityEngine;

/// <summary>
/// Represents the data that is specified by game design for specific unit type. 
/// </summary>
public class UnitProgressionData : ScriptableObject {

    [SerializeField]
    private UnitType unitType;
    [SerializeField]
    [Range(0f, 100f)]
    private float movementSpeed;
    [SerializeField]
    [Range(0f, 100f)]
    private float jumpSpeed;
    [SerializeField]
    private float baseMaxHealth;
    [SerializeField]
    private float healthIncreasePerLevel;
    [SerializeField]
    private float weaponMountYOffset;
    [SerializeField]
    private WeaponProgressionData weaponProgressionData;

    public UnitType UnitType { get { return this.unitType; } }
    public float MovementSpeed { get { return this.movementSpeed; } }
    public float JumpSpeed { get { return this.jumpSpeed; } }
    public float BaseMaxHealth { get { return this.baseMaxHealth; } }
    public float HealthIncreasePerLevel { get { return this.healthIncreasePerLevel; } }
    public float WeaponMountYOffset { get { return this.weaponMountYOffset; } }
    public WeaponProgressionData WeaponProgressionData { get { return this.weaponProgressionData; } }
}