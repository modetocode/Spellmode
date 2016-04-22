using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InputComponent : MonoBehaviour {

    public event Action JumpUpInputed;
    public event Action JumpDownInputed;
    public event Action PauseInputed;
    public event Action ShootInputed;

    [SerializeField]
    public GraphicRaycaster uiRaycaster;

    IDictionary<int, Vector2> touchIdToStartPositionMap;

    public void Awake() {
        this.touchIdToStartPositionMap = new Dictionary<int, Vector2>();
    }

    public void Update() {
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
            Touch currentTouch = Input.touches[i];

            if (currentTouch.phase == TouchPhase.Began) {
                if (this.IsGUITouch(Input.touches[i])) {
                    continue;
                }

                this.touchIdToStartPositionMap[currentTouch.fingerId] = currentTouch.position;
            }

            if (currentTouch.phase == TouchPhase.Ended) {
                if (!this.touchIdToStartPositionMap.ContainsKey(currentTouch.fingerId)) {
                    continue;
                }

                Vector2 touchEndPosition = Input.touches[i].position;
                Vector2 touchDeltaPositon = touchEndPosition - this.touchIdToStartPositionMap[currentTouch.fingerId];
                this.touchIdToStartPositionMap.Remove(currentTouch.fingerId);
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

    private bool IsGUITouch(Touch touch) {
        if (this.uiRaycaster == null) {
            return false;
        }

        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = touch.position;
        List<RaycastResult> results = new List<RaycastResult>();
        uiRaycaster.Raycast(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}