using System;
using UnityEngine;

/// <summary>
/// Represents one bullet. 
/// </summary>
public class Bullet : ITickable {

    public event Action<Bullet> Destroyed;

    private Unit target;
    private float damageOnHit;
    private Vector2 position;
    private float speed;
    private Vector2 direction;
    private Vector2 targetPosition;

    public Bullet(Unit target, float damageOnHit, float speed, Vector2 startPosition) {
        //TODO arg check
        this.target = target;
        this.damageOnHit = damageOnHit;
        this.speed = speed;
        this.targetPosition = target.PositionInMeters;
        this.direction = (this.targetPosition - startPosition).normalized;
    }

    public void Tick(float deltaTime) {
        Vector3 previousPosition = this.position;
        this.Move(deltaTime);
        if (this.WasTargetReached(previousPosition)) {
            if (this.IsTargetInRange()) {
                this.TakeDamage();
                this.Destroy();
            }
            //TODO what to do when target was reached but the target is not in range
        }
    }

    private void TakeDamage() {
        this.target.TakeDamage(this.damageOnHit);
    }

    private bool IsTargetInRange() {
        //TODO how to check if it is in range
        throw new NotImplementedException();
    }

    private bool WasTargetReached(Vector2 previousPosition) {
        float traveledDistance = Vector2.Distance(previousPosition, this.position);
        float previousPositionToTargetDistance = Vector2.Distance(previousPosition, this.targetPosition);
        return previousPositionToTargetDistance < traveledDistance;
    }

    public void Destroy() {
        if (this.Destroyed != null) {
            this.Destroyed(this);
        }
    }

    private void Move(float deltaTime) {
        this.position += speed * direction * deltaTime;
    }

    public void OnTickingFinished() {
    }
}