using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents one bullet. 
/// </summary>
public class Bullet : ITickable {

    public event Action<Bullet> Destroyed;

    public Vector2 CurrentPosition { get; private set; }
    public Vector2 Direction { get; private set; }

    private float damageOnHit;
    private float speed;
    private IList<Unit> possibleTargets;

    public Bullet(float damageOnHit, float speed, Vector2 startPosition, Vector2 direction, IList<Unit> possibleTargets) {
        //TODO arg check
        this.damageOnHit = damageOnHit;
        this.speed = speed;
        this.CurrentPosition = startPosition;
        this.Direction = direction;
        this.possibleTargets = possibleTargets;
    }

    public void Tick(float deltaTime) {
        this.Move(deltaTime);
    }

    // Marks that a given target unit is hit
    public void HitTargetUnit(Unit unit) {
        if (!this.possibleTargets.Contains(unit)) {
            throw new InvalidOperationException("The unit is not a target unit");
        }

        unit.TakeDamage(this.damageOnHit);
    }

    public void Destroy() {
        if (this.Destroyed != null) {
            this.Destroyed(this);
        }
    }

    private void Move(float deltaTime) {
        this.CurrentPosition += this.speed * this.Direction * deltaTime;
    }

    public void OnTickingFinished() {
    }
}