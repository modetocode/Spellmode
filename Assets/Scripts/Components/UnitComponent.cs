using System;
using UnityEngine;

/// <summary>
/// Component that is attached to game objects that represent a unit.
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class UnitComponent : MonoBehaviour, ITickable {

    private Unit unit;
    private Animator unitAnimator;
    private Collider2D unitCollider2D;

    public void Awake() {
        this.unitAnimator = this.GetComponent<Animator>();
        if (this.unitAnimator == null) {
            this.unitAnimator = this.GetComponentInChildren<Animator>();
        }

        if (this.unitAnimator == null) {
            throw new InvalidOperationException("No animator attached");
        }

        this.unitCollider2D = this.GetComponent<Collider2D>();
    }

    public void Initialize(Unit unit) {
        this.unit = unit;
    }

    public void Tick(float deltaTime) {
        if (this.unit == null) {
            return;
        }

        this.transform.position = this.unit.PositionInMeters;
        this.unitAnimator.SetFloat(Constants.Animations.MainCharacter.MoveSpeedParameterName, this.unit.CurrentMoveSpeed);

    }

    public void OnTickingPaused(float deltaTime) {
        this.unitAnimator.SetFloat(Constants.Animations.MainCharacter.MoveSpeedParameterName, 0f);
    }
}