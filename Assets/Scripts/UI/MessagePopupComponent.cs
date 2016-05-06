using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// Displays message popup with the defined message.
/// </summary>
public class MessagePopupComponent : MonoBehaviour {
    [SerializeField]
    private GameObject popupGroup;
    [SerializeField]
    private Text textComponent;
    [SerializeField]
    private Button confirmButton;

    UnityAction onConfirmClickedAction;

    public void Awake() {
        if (this.popupGroup == null) {
            throw new NullReferenceException("popupGroup is null");
        }

        if (this.textComponent == null) {
            throw new NullReferenceException("textComponent is null");
        }

        if (this.confirmButton == null) {
            throw new NullReferenceException("confirmButton is null");
        }

        this.confirmButton.onClick.AddListener(OnConfirmButtonClickedHandler);
    }

    public void Destroy() {
        this.confirmButton.onClick.RemoveListener(OnConfirmButtonClickedHandler);
    }

    public void Show(string message, UnityAction onConfirmClickedAction = null) {
        this.textComponent.text = message;
        this.onConfirmClickedAction = onConfirmClickedAction;
        this.ShowPopup(true);
    }

    public void Hide() {
        this.ShowPopup(false);
    }

    private void ShowPopup(bool toBeShown) {
        this.popupGroup.SetActive(toBeShown);
    }

    private void OnConfirmButtonClickedHandler() {
        this.popupGroup.SetActive(false);
        if (this.onConfirmClickedAction != null) {
            this.onConfirmClickedAction();
        }
    }
}