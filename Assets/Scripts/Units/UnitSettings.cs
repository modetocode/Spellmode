using System;

public class UnitSettings {
    public UnitType UnitType { get; private set; }
    public float MovementSpeed { get; private set; }
    public float JumpSpeed { get; private set; }
    public float MaxHealth { get; private set; }
    public float WeaponMountYOffset { get; private set; }

    public UnitSettings(UnitType unitType, float movementSpeed, float jumpSpeed, float maxHealth, float weaponMountYOffset) {
        if (movementSpeed < 0f) {
            throw new ArgumentOutOfRangeException("movementSpeed", "Cannot be less than zero.");
        }

        if (jumpSpeed < 0f) {
            throw new ArgumentOutOfRangeException("jumpSpeed", "Cannot be less than zero.");
        }

        if (maxHealth <= 0f) {
            throw new ArgumentOutOfRangeException("maxHealth", "Cannot be zero or less.");
        }

        this.UnitType = unitType;
        this.MovementSpeed = movementSpeed;
        this.JumpSpeed = jumpSpeed;
        this.MaxHealth = maxHealth;
        this.WeaponMountYOffset = weaponMountYOffset;
    }
}