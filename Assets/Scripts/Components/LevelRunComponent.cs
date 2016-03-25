using System;
using UnityEngine;
using UnityEngine.SceneManagement;

class LevelRunComponent : MonoBehaviour {

    private LevelRunManager levelRunManager;

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
        //TODO script references check 
    }

    public void Start() {
        this.inputComponent.BlockInput();
        this.levelRunManager = new LevelRunManager();
        this.levelRunManager.InitializeRun();
        this.levelRunManager.RunFinished += FinishRun;
        this.instantiatorComponent.InitializeComponent(this.levelRunManager.AttackingTeam, this.levelRunManager.DefendingTeam);
        this.instantiatorComponent.HeroUnitInstantiated += OnUnitInstantiated;
        this.levelRunGuiComponent.Initialize(this.levelRunManager);
        this.StartRun();
    }

    private void FinishRun() {
        this.levelRunManager.RunFinished -= FinishRun;
        this.UnsubscribeFromEvents();
        //TODO add the appropriate logic when level is finished
        SceneManager.LoadScene(Constants.Scenes.LevelRunSceneName);
    }

    private void OnUnitInstantiated(UnitComponent unitComponent) {
        this.instantiatorComponent.HeroUnitInstantiated -= OnUnitInstantiated;
        this.cameraComponent.TrackObject(unitComponent.gameObject);
    }

    private void StartRun() {
        this.levelRunManager.StartRun();
        this.inputComponent.UnblockInput();
        this.inputComponent.JumpUpInputed += JumpUpInputedHandler;
        this.inputComponent.JumpDownInputed += JumpDownInputedHandler;
        this.inputComponent.PauseInputed += PauseInputedHandler;
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

    public void UnsubscribeFromEvents() {
        this.inputComponent.JumpUpInputed -= JumpUpInputedHandler;
        this.inputComponent.JumpDownInputed -= JumpDownInputedHandler;
        this.inputComponent.PauseInputed -= PauseInputedHandler;
    }

    public void Update() {
        if (this.levelRunManager.IsGamePaused) {
            return;
        }

        this.levelRunManager.Tick(Time.deltaTime);
    }
}