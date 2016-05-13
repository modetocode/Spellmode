using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class BulletComponent : MonoBehaviour {

    public Bullet Bullet { get; private set; }
    public Action<BulletComponent> OnDestroyAction { get; private set; }

    public void Initialize(Bullet bullet, Action<BulletComponent> onDestroyAction) {
        if (bullet == null) {
            throw new ArgumentNullException("bullet");
        }

        if (onDestroyAction == null) {
            throw new ArgumentNullException("onDestroyAction");
        }

        this.Bullet = bullet;
        this.Bullet.Destroyed += Destroy;
        this.OnDestroyAction = onDestroyAction;
        this.SetPosition();

        //Calculate the rotation angle in order to rotate the sprite so it represents the direction of the bullet
        var angle = Mathf.Atan2(this.Bullet.Direction.y, this.Bullet.Direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void Destroy(Bullet obj) {
        this.Bullet.Destroyed -= Destroy;
        this.OnDestroyAction(this);
        this.Bullet = null;
        this.OnDestroyAction = null;
    }

    public void Update() {
        if (this.Bullet == null) {
            return;
        }

        this.SetPosition();
    }

    public void OnTriggerEnter2D(Collider2D other) {
        UnitComponent unitComponent = other.gameObject.GetComponent<UnitComponent>();
        if (unitComponent == null) {
            return;
        }

        if (this.Bullet.PossibleTargets.Contains(unitComponent.Unit)) {
            this.Bullet.HitTargetUnit(unitComponent.Unit);
        }
    }

    private void SetPosition() {
        this.gameObject.transform.position = this.Bullet.CurrentPosition;
    }
}