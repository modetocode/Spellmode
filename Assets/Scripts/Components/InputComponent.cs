using System;
using UnityEngine;

public class InputComponent : MonoBehaviour, ITickable {

    public event Action JumpInputed;
    public event Action JumpUpInputed;
    public event Action JumpDownInputed;
    public event Action PauseInputed;

    private bool isInputBlocked;

    public void BlockInput() {
        this.isInputBlocked = true;
    }

    public void UnblockInput() {
        this.isInputBlocked = false;
    }

    public void Tick(float deltaTime) {
        if (this.isInputBlocked) {
            return;
        }

        if (Input.GetButtonDown(Constants.Input.JumpSpeedInputName)) {
            if (this.JumpInputed != null) {
                this.JumpInputed();
            }
        }

        if (Input.GetButtonDown(Constants.Input.JumpUpInputName)) {
            if (this.JumpUpInputed != null) {
                this.JumpUpInputed();
            }
        }

        if (Input.GetButtonDown(Constants.Input.JumpDownInputName)) {
            if (this.JumpDownInputed != null) {
                this.JumpDownInputed();
            }
        }

        this.CheckForPauseInput();

        for (int i = 0; i < Input.touches.Length; i++) {
            if (Input.touches[i].phase != TouchPhase.Began) {
                continue;
            }

            if (this.JumpInputed != null) {
                this.JumpInputed();
            }
        }
    }

    public void OnTickingPaused(float deltaTime) {
        this.CheckForPauseInput();
    }

    private void CheckForPauseInput() {
        if (Input.GetButtonDown(Constants.Input.PauseInputName)) {
            if (this.PauseInputed != null) {
                this.PauseInputed();
            }
        }
    }
}