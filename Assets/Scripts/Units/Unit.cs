using UnityEngine;

public class Unit : ITickable {

    private UnitSettings unitSettings;

    /// <summary>
    /// The position of the unit in the world. The x-axis represents the horizontal distance (in meters) the starting point, and y-axis the vertical distance (in meters).
    /// </summary>
    public Vector2 PositionInMeters { get; private set; }
    public float CurrentMoveSpeed { get; private set; }

    public Unit(UnitSettings unitSettings, Vector2 unitSpawnPosition) {
        //TODO arg check
        this.unitSettings = unitSettings;
        this.PositionInMeters = unitSpawnPosition;
    }

    public void Tick(float deltaTime) {
        this.CurrentMoveSpeed = this.unitSettings.MovementSpeed * deltaTime;
        this.PositionInMeters += new Vector2(this.CurrentMoveSpeed, 0f);
        //TODO check for collisions, attack
    }
}