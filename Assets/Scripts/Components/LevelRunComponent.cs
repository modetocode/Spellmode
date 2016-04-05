using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

class LevelRunComponent : MonoBehaviour {

    [SerializeField]
    private InputComponent inputComponent;

    [SerializeField]
    private BackgroundComponent backgroundComponent;

    [SerializeField]
    private InstantiatorComponent instantiatorComponent;

    [SerializeField]
    private LevelRunCameraComponent cameraComponent;

    [SerializeField]
    private LevelRunGUIComponent levelRunGuiComponent;

    private LevelRunManager levelRunManager;
    private bool runFinished;

    public void Awake() {
        if (this.inputComponent == null) {
            throw new NullReferenceException("inputComponent is null");
        }

        if (this.instantiatorComponent == null) {
            throw new NullReferenceException("instantiatorComponent is null");
        }

        if (this.cameraComponent == null) {
            throw new NullReferenceException("cameraComponent is null");
        }

        if (this.levelRunGuiComponent == null) {
            throw new NullReferenceException("levelRunGuiComponent is null");
        }
    }

    public void Start() {
        this.runFinished = false;
        this.levelRunManager = new LevelRunManager();
        this.levelRunManager.InitializeRun();
        this.levelRunManager.RunFinished += FinishRun;
        this.levelRunManager.AttackingTeam.UnitAdded += OnUnitInTeamAdded;
        this.levelRunManager.DefendingTeam.UnitAdded += OnUnitInTeamAdded;
        this.levelRunManager.BulletManager.BulletAdded += OnNewBulletAdded;
        this.instantiatorComponent.HeroUnitInstantiated += OnNewUnitInstantiated;
        this.levelRunGuiComponent.Initialize(this.levelRunManager);
        this.inputComponent.JumpDownInputed += JumpDownInputedHandler;
        this.inputComponent.JumpUpInputed += JumpUpInputedHandler;
        this.inputComponent.PauseInputed += PauseInputedHandler;
        this.inputComponent.ShootInputed += ShootInputedHandler;
        this.StartRun();
    }

    public void Update() {
        if (this.levelRunManager.IsGamePaused) {
            return;
        }

        if (this.runFinished) {
            return;
        }

        this.levelRunManager.Tick(Time.deltaTime);
    }

    private void StartRun() {
        this.levelRunManager.StartRun();
    }

    private void FinishRun() {
        this.levelRunManager.RunFinished -= FinishRun;
        this.UnsubscribeFromEvents();
        this.runFinished = true;
        //TODO add the appropriate logic when level is finished
        this.StartCoroutine(FinishRunCoroutine(Constants.LevelRun.WaitTimeAfterRunFinishedInSeconds));
    }

    private void OnUnitInTeamAdded(Unit newUnit) {
        this.instantiatorComponent.InstantiateUnit(newUnit);
    }

    private void OnNewBulletAdded(Bullet newBullet) {
        this.instantiatorComponent.InstantiateBullet(newBullet);
    }

    private void OnNewUnitInstantiated(UnitComponent unitComponent) {
        if (this.levelRunManager.AttackingTeam.IsUnitInTeam(unitComponent.Unit)) {
            this.cameraComponent.TrackObject(unitComponent.gameObject);
            this.backgroundComponent.SetMoveSpeed(unitComponent.Unit.MovementSpeed * Constants.LevelRun.BackgroundSpeedFactor);
        }
    }

    private void JumpDownInputedHandler() {
        if (this.levelRunManager.IsGamePaused) {
            return;
        }

        this.levelRunManager.AttackingTeam.MoveAllAliveUnitsToLowerPlatformIfPossible();
    }

    private void JumpUpInputedHandler() {
        if (this.levelRunManager.IsGamePaused) {
            return;
        }

        this.levelRunManager.AttackingTeam.MoveAllAliveUnitsToUpperPlatformIfPossible();
    }

    private void PauseInputedHandler() {
        if (this.levelRunManager.IsGamePaused) {
            this.levelRunManager.ResumeGame();
            this.backgroundComponent.ResumeMovement();
        }
        else {
            this.levelRunManager.PauseGame();
            this.backgroundComponent.PauseMovement();
        }
    }

    private void ShootInputedHandler() {
        if (this.levelRunManager.IsGamePaused) {
            return;
        }

        this.levelRunManager.CombatManager.TriggerManualAttack();
    }

    public void UnsubscribeFromEvents() {
        this.levelRunManager.AttackingTeam.UnitAdded -= OnUnitInTeamAdded;
        this.levelRunManager.DefendingTeam.UnitAdded -= OnUnitInTeamAdded;
        this.instantiatorComponent.HeroUnitInstantiated -= OnNewUnitInstantiated;
        this.levelRunManager.BulletManager.BulletAdded -= OnNewBulletAdded;
        this.inputComponent.JumpDownInputed -= JumpDownInputedHandler;
        this.inputComponent.JumpUpInputed -= JumpUpInputedHandler;
        this.inputComponent.PauseInputed -= PauseInputedHandler;
        this.inputComponent.ShootInputed -= ShootInputedHandler;
    }

    IEnumerator FinishRunCoroutine(float waitTime) {
        this.backgroundComponent.PauseMovement();
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(Constants.Scenes.LevelRunSceneName);
    }
}