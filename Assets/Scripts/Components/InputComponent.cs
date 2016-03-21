using System;
using UnityEngine;

public class InputComponent : MonoBehaviour {

    public event Action JumpInputed;
    public event Action JumpUpInputed;
    public event Action JumpDownInputed;

    private bool isInputBlocked;

    public void BlockInput() {
        this.isInputBlocked = true;
    }

    public void UnblockInput() {
        this.isInputBlocked = false;
    }

    public void Update() {
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

        for (int i = 0; i < Input.touches.Length; i++) {
            if (Input.touches[i].phase != TouchPhase.Began) {
                continue;
            }

            if (this.JumpInputed != null) {
                this.JumpInputed();
            }
        }
    }
}