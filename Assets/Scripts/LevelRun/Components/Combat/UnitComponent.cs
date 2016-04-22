using System;
using UnityEngine;

/// <summary>
/// Component that is attached to game objects that represent a unit.
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class UnitComponent : MonoBehaviour {

    public Unit Unit { get; private set; }
    private Animator unitAnimator;
    private Vector2 previousUnitPosition;

    public void Awake() {
        this.unitAnimator = this.GetComponent<Animator>();
        if (this.unitAnimator == null) {
            this.unitAnimator = this.GetComponentInChildren<Animator>();
        }

        if (this.unitAnimator == null) {
            throw new InvalidOperationException("No animator attached");
        }
    }

    public void Initialize(Unit unit) {
        if (unit == null) {
            throw new ArgumentNullException("unit");
        }

        this.Unit = unit;
        this.previousUnitPosition = Vector2.zero;
        this.Unit.Weapon.WeaponFired += ShowFireAnimation;
        this.Unit.Died += ShowDeathAnimation;
        this.Unit.HealthChanged += ShowHitAnimation;
    }

    public void Destroy() {
        Destroy(this.gameObject);
    }

    public void Update() {
        if (this.Unit == null) {
            return;
        }

        float currentMovementSpeed = 0f;
        if (this.previousUnitPosition != Vector2.zero) {
            currentMovementSpeed = Mathf.Abs(this.Unit.PositionInMeters.x - this.previousUnitPosition.x) / Time.deltaTime;
        }

        this.previousUnitPosition = this.Unit.PositionInMeters;
        this.transform.position = this.Unit.PositionInMeters;
        this.unitAnimator.SetFloat(Constants.Animations.MainCharacter.MoveSpeedParameterName, currentMovementSpeed);

    }

    private void ShowFireAnimation(Weapon weapon) {
        this.unitAnimator.SetTrigger(Constants.Animations.MainCharacter.FireTriggerParameterName);
    }

    private void ShowDeathAnimation(Unit unit) {
        this.unitAnimator.SetTrigger(Constants.Animations.MainCharacter.DeathTriggerParameterName);
    }

    private void ShowHitAnimation(float healthDeltaValue) {
        if (healthDeltaValue < 0 && this.Unit.IsAlive) {
            this.unitAnimator.SetTrigger(Constants.Animations.MainCharacter.HitTriggerParameterName);
        }
    }

    private void UnsubsribeFromEvents() {
        if (this.Unit != null) {
            this.Unit.Weapon.WeaponFired -= ShowFireAnimation;
            this.Unit.Died -= ShowDeathAnimation;
        }
    }

    public void OnDestroy() {
        this.UnsubsribeFromEvents();
    }
}