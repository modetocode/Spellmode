using System;
using UnityEngine;

public class Unit : ITickable {

    private UnitSettings unitSettings;

    /// <summary>
    /// The position of the unit in the world. The x-axis represents the horizontal distance (in meters) the starting point, and y-axis the vertical distance (in meters).
    /// </summary>
    public Vector2 PositionInMeters { get; private set; }
    public float CurrentMoveSpeed { get; private set; }

    private bool isJumping;
    private float jumpDirection;
    private float jumpYDestination;


    public Unit(UnitSettings unitSettings, Vector2 unitSpawnPosition) {
        //TODO arg check
        this.unitSettings = unitSettings;
        this.PositionInMeters = unitSpawnPosition;
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

        //TODO check for collisions, attack
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
}