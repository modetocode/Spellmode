public class UnitSettings {
    public float MovementSpeed { get; private set; }
    public float MaxHealth { get; private set; }

    public UnitSettings(float movementSpeed, float maxHealth) {
        //TODO arg check
        this.MovementSpeed = movementSpeed;
        this.MaxHealth = maxHealth;
    }
}