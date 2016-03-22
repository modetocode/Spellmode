using System;
using UnityEngine;

class LevelRunComponent : MonoBehaviour {

    private LevelRunManager levelRunManager;
    private Ticker componentTicker;

    [SerializeField]
    private InputComponent inputComponent;

    [SerializeField]
    private BackgroundComponent backgroundComponent;

    [SerializeField]
    private InstantiatorComponent instantiatorComponent;

    [SerializeField]
    private LevelRunCameraComponent cameraComponent;


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

        //TODO script references check 
    }

    public void Start() {
        this.inputComponent.BlockInput();
        this.levelRunManager = new LevelRunManager();
        this.levelRunManager.InitializeRun();
        this.instantiatorComponent.InitializeComponent(this.levelRunManager.AttackingTeam, this.levelRunManager.DefendingTeam);
        this.instantiatorComponent.HeroUnitInstantiated += OnUnitInstantiated;
        componentTicker = new Ticker(new ITickable[] { this.inputComponent, this.cameraComponent, this.backgroundComponent });
        this.StartRun();
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
        this.inputComponent.JumpInputed += JumpInputedHandler;
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
            this.componentTicker.PauseTicking();
            this.levelRunManager.PauseGame();
        }
        else {
            this.componentTicker.ResumeTicking();
            this.levelRunManager.ResumeGame();
        }
    }

    public void OnDestroy() {
        //TODO check if this is the right place for unsubscribe
        this.inputComponent.JumpInputed -= JumpInputedHandler;
        this.inputComponent.JumpUpInputed -= JumpUpInputedHandler;
        this.inputComponent.JumpDownInputed -= JumpDownInputedHandler;
        this.inputComponent.PauseInputed -= PauseInputedHandler;
    }

    private void JumpInputedHandler() {
        //TODO implement this
    }

    public void Update() {
        this.componentTicker.Tick(Time.deltaTime);
        this.levelRunManager.Tick(Time.deltaTime);
    }
}