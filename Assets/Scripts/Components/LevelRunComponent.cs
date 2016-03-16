using UnityEngine;

class LevelRunComponent : MonoBehaviour {

    private LevelRunManager levelRunManager;

    [SerializeField]
    private InputComponent inputComponent;

    //TODO remove this
    [SerializeField]
    private MainCharacterComponent characterComponent;

    public void Awake() {
        if (inputComponent == null) {
            throw new System.NullReferenceException("inputComponent is null");
        }
    }

    public void Start() {
        this.inputComponent.BlockInput();
        this.levelRunManager = new LevelRunManager();
        this.levelRunManager.InitializeRun();
        this.inputComponent.UnblockInput();
        this.inputComponent.JumpInputed += JumpInputedHandler;
    }

    public void OnDestroy() {
        this.inputComponent.JumpInputed -= JumpInputedHandler;
    }

    private void JumpInputedHandler() {
        //TODO change this
        this.characterComponent.Jump();
    }

    public void Update() {
        this.levelRunManager.Tick(Time.deltaTime);
    }
}