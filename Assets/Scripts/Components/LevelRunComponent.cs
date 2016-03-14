using UnityEngine;

class LevelRunComponent : MonoBehaviour {

    private LevelRunManager levelRunManager;
    private InputComponent inputComponent;

    public void Start() {
        this.levelRunManager = new LevelRunManager();
        this.levelRunManager.InitializeRun();
    }

    public void Update() {
        this.levelRunManager.Tick(Time.deltaTime);
    }
}