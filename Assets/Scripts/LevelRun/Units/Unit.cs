using System;
using UnityEngine;

public class Unit : ITickable {


    public event Action<Unit> Died;

    /// <summary>
    /// Thrown when the health is changed. The difference from the previous health is specified (delta).
    /// </summary>
    public event Action<float> HealthChanged;

    /// <summary>
    /// The position of the unit in the world. The x-axis represents the horizontal distance (in meters) the starting point, and y-axis the vertical distance (in meters).
    /// </summary>
    public Vector2 PositionInMeters { get; private set; }
    public float CurrentMoveSpeed { get; private set; }
    public Weapon Weapon { get; private set; }
    public UnitType UnitType { get { return this.unitSettings.UnitType; } }
    public float MovementSpeed { get { return this.unitSettings.MovementSpeed; } }
    public int Level { get; private set; }

    /// <summary>
    /// True if the units auto-attacks when the weapon is ready to fire, or false it the firing is triggered manually.
    /// </summary>
    public bool HasAutoAttack { get; private set; }

    public float Health {
        get { return this.healthElement.Health; }
    }

    public float MaxHealth {
        get { return this.healthElement.MaxHealth; }
    }

    public bool IsAlive {
        get { return this.Health > 0; }
    }

    public float WeaponMountYOffset { get { return this.unitSettings.WeaponMountYOffset; } }

    private HealthElement healthElement;
    private bool isJumping;
    private float jumpDirection;
    private float jumpYDestination;
    private UnitSettings unitSettings;

    public Unit(UnitSettings unitSettings, int level, WeaponSettings weaponSettings, Vector2 unitSpawnPosition, bool hasAutoAttack) {
        if (unitSettings == null) {
            throw new ArgumentNullException("unitSettings");
        }

        if (level < 1) {
            throw new ArgumentOutOfRangeException("level", "Cannot be less than one.");
        }

        if (weaponSettings == null) {
            throw new ArgumentNullException("weaponSettings");
        }

        this.unitSettings = unitSettings;
        this.Level = level;
        this.PositionInMeters = unitSpawnPosition;
        this.Weapon = new Weapon(weaponSettings, this);
        this.healthElement = new HealthElement(unitSettings.MaxHealth);
        this.healthElement.HealthChanged += HealthChangedHandler;
        this.HasAutoAttack = hasAutoAttack;
    }

    public void Tick(float deltaTime) {
        this.CurrentMoveSpeed = this.unitSettings.MovementSpeed * deltaTime;
        this.PositionInMeters += new Vector2(this.CurrentMoveSpeed, 0f);
        if (this.isJumping) {
            float currentJumpSpeed = this.unitSettings.JumpSpeed * deltaTime * jumpDirection;
            float newYPosition = this.PositionInMeters.y + currentJumpSpeed;
            if (this.isJumpDestinationReached(newYPosition)) {
                this.isJumping = false;
                this.PositionInMeters = new Vector2(this.PositionInMeters.x, this.jumpYDestination);
            }
            else {
                this.PositionInMeters += new Vector2(0f, currentJumpSpeed);
            }
        }

        this.Weapon.Tick(deltaTime);
    }

    public void TakeDamage(float amount) {
        if (!this.IsAlive) {
            Debug.LogError("taking damage to already dead unit");
            return;
        }

        this.healthElement.DecreaseHealth(amount);
    }

    private void HealthChangedHandler(float amount) {
        if (this.HealthChanged != null) {
            this.HealthChanged(amount);
        }

        if (!this.IsAlive) {
            this.healthElement.HealthChanged -= this.HealthChangedHandler;
            if (this.Died != null) {
                this.Died(this);
            }
        }
    }

    private bool isJumpDestinationReached(float positionY) {
        if (this.jumpDirection == this.GetJumpDirectionValue(jumpingUp: true)) {
            return positionY >= jumpYDestination;
        }
        else {
            return positionY <= jumpYDestination;
        }

    }

    private float GetJumpDirectionValue(bool jumpingUp) {
        return jumpingUp ? 1f : -1f;
    }

    public void MoveToUpperPlatformIfPossible() {
        Func<Constants.Platforms.PlatformType, PlatformManager.PlatformResult> hasNeighbourPlatformFunction = (platformType) => PlatformManager.HasUpperPlatformThan(platformType);
        MoveToPlatformIfPossible(hasNeighbourPlatformFunction, jumpDirection: this.GetJumpDirectionValue(jumpingUp: true));
    }

    public void MoveToLowerPlatformIfPossible() {
        Func<Constants.Platforms.PlatformType, PlatformManager.PlatformResult> hasNeighbourPlatformFunction = (platformType) => PlatformManager.HasLowerPlatformThan(platformType);
        MoveToPlatformIfPossible(hasNeighbourPlatformFunction, jumpDirection: this.GetJumpDirectionValue(jumpingUp: false));
    }

    private void MoveToPlatformIfPossible(Func<Constants.Platforms.PlatformType, PlatformManager.PlatformResult> hasNeighbourPlatformFunction, float jumpDirection) {
        if (this.isJumping) {
            return;
        }

        PlatformManager.PlatformResult isOnPlatformResult = PlatformManager.IsPositionOnPlatform(this.PositionInMeters);
        if (!isOnPlatformResult.Successful) {
            return;
        }

        PlatformManager.PlatformResult hasNeighbourPlatformResult = hasNeighbourPlatformFunction(isOnPlatformResult.PlatformType);
        if (!hasNeighbourPlatformResult.Successful) {
            return;
        }

        this.jumpDirection = jumpDirection;
        this.isJumping = true;
        this.jumpYDestination = PlatformManager.GetYCoordinateForPlatform(hasNeighbourPlatformResult.PlatformType);
    }

    public void OnTickingFinished() {
    }
}