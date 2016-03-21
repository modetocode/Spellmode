﻿using System;
using UnityEngine;

class LevelRunComponent : MonoBehaviour {

    private LevelRunManager levelRunManager;

    [SerializeField]
    private InputComponent inputComponent;

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
        this.instantiatorComponent.HeroUnitInstantiated += TrackObject;
        this.StartRun();
    }

    private void TrackObject(UnitComponent objectToTrack) {
        this.instantiatorComponent.HeroUnitInstantiated -= TrackObject;
        this.cameraComponent.TrackObject(objectToTrack.gameObject);
    }

    private void StartRun() {
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