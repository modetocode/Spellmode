public class UnitSettings {
    public float MovementSpeed { get; private set; }
    public float JumpSpeed { get; private set; }
    public float MaxHealth { get; private set; }

    public UnitSettings(float movementSpeed, float jumpSpeed, float maxHealth) {
        //TODO arg check
        this.MovementSpeed = movementSpeed;
        this.JumpSpeed = jumpSpeed;
        this.MaxHealth = maxHealth;
    }
}