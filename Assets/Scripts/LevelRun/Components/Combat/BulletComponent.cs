using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class BulletComponent : MonoBehaviour {

    public Bullet Bullet { get; private set; }

    public void Initialize(Bullet bullet) {
        this.Bullet = bullet;
        this.Bullet.Destroyed += Destroy;
        //Calculate the rotation angle in order to rotate the sprite so it represents the direction of the bullet
        var angle = Mathf.Atan2(this.Bullet.Direction.y, this.Bullet.Direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void Destroy(Bullet obj) {
        this.Bullet.Destroyed -= Destroy;
        Destroy(this.gameObject);
    }

    void Update() {
        if (this.Bullet == null) {
            return;
        }

        this.gameObject.transform.position = this.Bullet.CurrentPosition;
    }

    void OnTriggerEnter2D(Collider2D other) {
        UnitComponent unitComponent = other.gameObject.GetComponent<UnitComponent>();
        if (unitComponent == null) {
            return;
        }

        if (this.Bullet.PossibleTargets.Contains(unitComponent.Unit)) {
            this.Bullet.HitTargetUnit(unitComponent.Unit);
        }
    }
}