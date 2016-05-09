using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// Displays a popup with a message and two buttons: yes and no
/// </summary>
public class YesNoPopupComponent : MonoBehaviour {
    [SerializeField]
    private GameObject popupGroup;
    [SerializeField]
    private Text textComponent;
    [SerializeField]
    private Button yesButton;
    [SerializeField]
    private Button noButton;

    private UnityAction onYesClickedAction;
    private UnityAction onNoClickedAction;

    public void Awake() {
        if (this.popupGroup == null) {
            throw new NullReferenceException("popupGroup is null");
        }

        if (this.textComponent == null) {
            throw new NullReferenceException("textComponent is null");
        }

        if (this.yesButton == null) {
            throw new NullReferenceException("yesButton is null");
        }

        if (this.noButton == null) {
            throw new NullReferenceException("noButton is null");
        }

        this.yesButton.onClick.AddListener(OnYesButtonClickedHandler);
        this.noButton.onClick.AddListener(OnNoButtonClickedHandler);
    }

    public void Destroy() {
        this.yesButton.onClick.RemoveListener(OnYesButtonClickedHandler);
        this.noButton.onClick.RemoveListener(OnNoButtonClickedHandler);
    }

    public void Show(string message, UnityAction onYesClickedAction = null, UnityAction onNoClickedAction = null) {
        this.textComponent.text = message;
        this.onYesClickedAction = onYesClickedAction;
        this.onNoClickedAction = onNoClickedAction;
        this.ShowPopup(true);
    }

    public void Hide() {
        this.ShowPopup(false);
    }

    private void ShowPopup(bool toBeShown) {
        this.popupGroup.SetActive(toBeShown);
    }

    private void OnYesButtonClickedHandler() {
        this.popupGroup.SetActive(false);
        if (this.onYesClickedAction != null) {
            this.onYesClickedAction();
        }
    }

    private void OnNoButtonClickedHandler() {
        this.popupGroup.SetActive(false);
        if (this.onNoClickedAction != null) {
            this.onNoClickedAction();
        }
    }
}
