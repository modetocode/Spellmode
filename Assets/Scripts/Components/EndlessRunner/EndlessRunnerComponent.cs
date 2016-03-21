using UnityEngine;

class EndlessRunnerComponent : MonoBehaviour {

    private LevelRunManager levelRunManager;

    [SerializeField]
    private InputComponent inputComponent;

    [SerializeField]
    private MainCharacterComponent characterComponent;

    public void Awake() {
        if (inputComponent == null) {
            throw new System.NullReferenceException("inputComponent is null");
        }
    }

    public void Start() {
        this.inputComponent.BlockInput();
        this.inputComponent.JumpInputed += JumpInputedHandler;
        this.characterComponent.Move(200f);
        this.inputComponent.UnblockInput();
    }

    public void OnDestroy() {
        this.inputComponent.JumpInputed -= JumpInputedHandler;
    }

    private void JumpInputedHandler() {
        //TODO change this
        this.characterComponent.Jump();
    }

    public void Update() {
        //this.levelRunManager.Tick(Time.deltaTime);
    }
}