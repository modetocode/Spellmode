﻿using UnityEngine;

class LevelRunComponent : MonoBehaviour {

    private LevelRunManager levelRunManager;

    [SerializeField]
    private InputComponent inputComponent;

    [SerializeField]
    private InstantiatorComponent instantiatorComponent;

    public void Awake() {
        if (inputComponent == null) {
            throw new System.NullReferenceException("inputComponent is null");
        }
    }

    public void Start() {
        this.inputComponent.BlockInput();
        this.levelRunManager = new LevelRunManager();
        this.levelRunManager.InitializeRun();
        this.instantiatorComponent.InitializeComponent(this.levelRunManager.AttackingTeam, this.levelRunManager.DefendingTeam);
        this.levelRunManager.StartRun();
        this.inputComponent.UnblockInput();
        this.inputComponent.JumpInputed += JumpInputedHandler;
    }

    public void OnDestroy() {
        this.inputComponent.JumpInputed -= JumpInputedHandler;
    }

    private void JumpInputedHandler() {
        //TODO implement this
    }

    public void Update() {
        this.levelRunManager.Tick(Time.deltaTime);
    }
}