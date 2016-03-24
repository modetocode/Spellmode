using System;
using UnityEngine;
using UnityEngine.SceneManagement;

class LevelRunComponent : MonoBehaviour {

    private LevelRunManager levelRunManager;
    private Ticker componentTicker;
    private Ticker lastToTickComponentTicker;

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
        this.componentTicker = new Ticker(new ITickable[] { this.inputComponent, this.cameraComponent, this.backgroundComponent });
        this.lastToTickComponentTicker = new Ticker(new ITickable[] { this.cameraComponent, this.levelRunGuiComponent });
        this.StartRun();
    }

    private void FinishRun() {
        this.levelRunManager.RunFinished -= FinishRun;
        this.UnsubscribeFromEvents();
        this.componentTicker.FinishTicking();
        this.lastToTickComponentTicker.FinishTicking();
        //TODO add the appropriate logic when level is finished
        SceneManager.LoadScene(Constants.Scenes.LevelRunSceneName);
    }

    private void OnUnitInstantiated(UnitComponent unitComponent) {
        this.instantiatorComponent.HeroUnitInstantiated -= OnUnitInstantiated;
        this.cameraComponent.TrackObject(unitComponent.gameObject);
        this.componentTicker.AddTickableObject(unitComponent);
        //TODO remove tickable object
    }

    private void StartRun() {
        this.levelRunManager.StartRun();
        this.inputComponent.UnblockInput();
        this.inputComponent.JumpUpInputed += JumpUpInputedHandler;
        this.inputComponent.JumpDownInputed += JumpDownInputedHandler;
        this.inputComponent.PauseInputed += PauseInputedHandler;
    }

    private void JumpDownInputedHandler() {
        this.levelRunManager.AttackingTeam.MoveAllAliveUnitsToLowerPlatformIfPossible();
    }

    private void JumpUpInputedHandler() {
        this.levelRunManager.AttackingTeam.MoveAllAliveUnitsToUpperPlatformIfPossible();
    }

    private void PauseInputedHandler() {
        if (this.componentTicker.IsTicking) {
            this.levelRunManager.PauseGame();
            this.componentTicker.PauseTicking();
            this.lastToTickComponentTicker.PauseTicking();
        }
        else {
            this.levelRunManager.ResumeGame();
            this.componentTicker.ResumeTicking();
            this.lastToTickComponentTicker.ResumeTicking();
        }
    }

    public void UnsubscribeFromEvents() {
        this.inputComponent.JumpUpInputed -= JumpUpInputedHandler;
        this.inputComponent.JumpDownInputed -= JumpDownInputedHandler;
        this.inputComponent.PauseInputed -= PauseInputedHandler;
    }

    public void Update() {
        if (this.componentTicker.TickingFinished) {
            return;
        }

        this.levelRunManager.Tick(Time.deltaTime);

        // Level run can be finished in this tick, so additional check for ticking is required
        if (this.componentTicker.TickingFinished) {
            return;
        }

        this.componentTicker.Tick(Time.deltaTime);
        // The camera and gui should be updated last after all of the moving units have updated the positions
        this.lastToTickComponentTicker.Tick(Time.deltaTime);
    }
}