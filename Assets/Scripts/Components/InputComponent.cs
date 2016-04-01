using System;
using UnityEngine;

public class InputComponent : MonoBehaviour {

    public event Action JumpUpInputed;
    public event Action JumpDownInputed;
    public event Action PauseInputed;
    //TODO remove this event when removing endless runner prototype
    public event Action JumpInputed;
    public event Action ShootInputed;

    private Vector2 touchStartPosition;

    public void Update() {
        if (Input.GetButtonDown(Constants.Input.JumpSpeedInputName)) {
            if (this.JumpInputed != null) {
                this.JumpInputed();
            }
        }

        if (Input.GetButtonDown(Constants.Input.JumpUpInputName)) {
            this.OnJumpUpInputed();
        }

        if (Input.GetButtonDown(Constants.Input.JumpDownInputName)) {
            this.OnJumpDownInputed();
        }

        if (Input.GetButtonDown(Constants.Input.PauseInputName)) {
            this.OnPauseInputed();
        }

        if (Input.GetButtonDown(Constants.Input.ShootInputName)) {
            this.OnShootInputed();
        }

        Action swipeUpAction = this.OnJumpUpInputed;
        Action swipeDownAction = this.OnJumpDownInputed;
        this.HandleTouchInput(swipeUpAction: swipeUpAction, swipeDownAction: swipeDownAction, swipeLeftAction: null, swipeRightAction: null, touchAction: this.OnShootInputed);
    }

    private void OnPauseInputed() {
        if (this.PauseInputed != null) {
            this.PauseInputed();
        }
    }

    private void OnJumpDownInputed() {
        if (this.JumpDownInputed != null) {
            this.JumpDownInputed();
        }
    }

    private void OnJumpUpInputed() {
        if (this.JumpUpInputed != null) {
            this.JumpUpInputed();
        }
    }

    private void OnShootInputed() {
        if (this.ShootInputed != null) {
            this.ShootInputed();
        }
    }

    private void HandleTouchInput(Action swipeUpAction, Action swipeDownAction, Action swipeLeftAction, Action swipeRightAction, Action touchAction) {
        for (int i = 0; i < Input.touches.Length; i++) {
            TouchPhase currentPhase = Input.touches[i].phase;
            if (currentPhase == TouchPhase.Began) {
                this.touchStartPosition = Input.touches[i].position;
            }

            if (currentPhase == TouchPhase.Ended) {
                Vector2 touchEndPosition = Input.touches[i].position;
                Vector2 touchDeltaPositon = touchEndPosition - this.touchStartPosition;
                Vector2 toucheDeltaPositionScreenPercentage = new Vector2(touchDeltaPositon.x / Screen.width, touchDeltaPositon.y / Screen.height);
                Vector2 touchScreenMovementPercentage = new Vector2(Mathf.Abs(toucheDeltaPositionScreenPercentage.x), Mathf.Abs(toucheDeltaPositionScreenPercentage.y));
                if (touchScreenMovementPercentage.x > Constants.Input.MinimalScreenPercentageConsideredToBeSwipe || touchScreenMovementPercentage.y > Constants.Input.MinimalScreenPercentageConsideredToBeSwipe) {
                    // we have detected a swipe
                    bool isHorizontalSwipe = touchScreenMovementPercentage.x > touchScreenMovementPercentage.y;
                    if (isHorizontalSwipe) {
                        if (toucheDeltaPositionScreenPercentage.x > 0) {
                            if (swipeRightAction != null) {
                                swipeRightAction();
                            }
                        }
                        else {
                            if (swipeLeftAction != null) {
                                swipeLeftAction();
                            }
                        }

                        return;
                    }

                    //vertical swipe
                    if (toucheDeltaPositionScreenPercentage.y > 0) {
                        if (swipeUpAction != null) {
                            swipeUpAction();
                        }
                    }
                    else {
                        if (swipeDownAction != null) {
                            swipeDownAction();
                        }
                    }
                }
                else {
                    //it is only a small swipe/touch
                    if (touchAction != null) {
                        touchAction();
                    }
                }
            }


        }
    }
}