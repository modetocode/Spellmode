using System;
using UnityEngine;

public class InputComponent : MonoBehaviour {

    public event Action JumpInputed;
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
            if (JumpInputed != null) {
                JumpInputed();
            }
        }

        for (int i = 0; i < Input.touches.Length; i++) {
            if (Input.touches[i].phase != TouchPhase.Began) {
                continue;
            }

            if (JumpInputed != null) {
                JumpInputed();
            }
        }
    }
}