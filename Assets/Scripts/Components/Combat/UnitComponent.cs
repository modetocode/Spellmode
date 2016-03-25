using System;
using UnityEngine;

/// <summary>
/// Component that is attached to game objects that represent a unit.
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class UnitComponent : MonoBehaviour {

    private Unit unit;
    private Animator unitAnimator;
    private Collider2D unitCollider2D;
    private Vector2 previousUnitPosition;

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
        this.previousUnitPosition = Vector2.zero;
    }

    public void Update() {
        if (this.unit == null) {
            return;
        }

        float currentMovementSpeed = 0f;
        if (this.previousUnitPosition != Vector2.zero) {
            currentMovementSpeed = Mathf.Abs(this.unit.PositionInMeters.x - this.previousUnitPosition.x) / Time.deltaTime;
        }

        this.previousUnitPosition = this.unit.PositionInMeters;
        this.transform.position = this.unit.PositionInMeters;
        this.unitAnimator.SetFloat(Constants.Animations.MainCharacter.MoveSpeedParameterName, currentMovementSpeed);

    }
}