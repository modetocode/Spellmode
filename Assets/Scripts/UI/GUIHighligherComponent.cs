using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Responsible for highlighting one GUI component on the screen.
/// </summary>
public class GUIHighligherComponent : MonoBehaviour {

    /// <summary>
    /// Event that is thrown when an GUI element is highlighted;
    /// </summary>
    public event Action ElementHighlighted;

    [SerializeField]
    private GameObject highligherGroup;
    [SerializeField]
    private RectTransform highligherRect;
    [SerializeField]
    private LayoutElement leftLayoutElement;
    [SerializeField]
    private LayoutElement topLayoutElement;
    [SerializeField]
    private LayoutElement widthLayoutElement;
    [SerializeField]
    private LayoutElement heightLayoutElement;

    private int numberOfUpdateExecutions = 0;
    private bool isReadyToHighlight = false;
    private RectTransform rectToBeHighlighted;

    public void Awake() {
        if (this.highligherGroup == null) {
            throw new NullReferenceException("highligherGroup is null");
        }

        if (this.highligherRect == null) {
            throw new NullReferenceException("highligherRect is null");
        }

        if (this.leftLayoutElement == null) {
            throw new NullReferenceException("leftLayoutElement is null");
        }

        if (this.topLayoutElement == null) {
            throw new NullReferenceException("topLayoutElement is null");
        }

        if (this.widthLayoutElement == null) {
            throw new NullReferenceException("widthLayoutElement is null");
        }

        if (this.heightLayoutElement == null) {
            throw new NullReferenceException("heightLayoutElement is null");
        }
    }

    public void Update() {
        if (this.numberOfUpdateExecutions < 2) {
            //For some unknown reasons the unity methods for UI coordinate calculations won't work before the second Update when the scene is opened.
            //That is the reason why the event ElementHighlighted was defined and if there is something to be highlighed in the start of the scene it must wait until the second update.
            this.numberOfUpdateExecutions++;
            this.isReadyToHighlight = true;
            if (this.rectToBeHighlighted != null) {
                this.HighlightUIElement(this.rectToBeHighlighted);
            }
        }
    }

    public void HideHighlight() {
        this.highligherGroup.SetActive(false);
    }

    /// <summary>
    /// Highlights UI element. When the highlighting is finished the event ElementHighlighted is thrown. Sometimes the highlighting is not done istantly so always subscribe to this event.
    /// </summary>
    /// <param name="highlightUIRect"></param>
    public void HighlightUIElement(RectTransform highlightUIRect) {
        //TODO arg check
        if (!this.isReadyToHighlight) {
            this.rectToBeHighlighted = highlightUIRect;
            this.highligherGroup.SetActive(true);
            return;
        }

        Vector3[] cornersWorldSpace = new Vector3[4];
        highlightUIRect.GetWorldCorners(cornersWorldSpace);
        float factorRatio = (cornersWorldSpace[1].y - cornersWorldSpace[0].y) / highlightUIRect.rect.height;
        this.leftLayoutElement.preferredWidth = cornersWorldSpace[0].x / factorRatio;
        this.topLayoutElement.preferredHeight = (Screen.height - cornersWorldSpace[1].y) / factorRatio;
        this.widthLayoutElement.preferredWidth = highlightUIRect.rect.width;
        this.heightLayoutElement.preferredHeight = highlightUIRect.rect.height;
        this.highligherGroup.SetActive(true);
        if (this.ElementHighlighted != null) {
            this.ElementHighlighted();
        }
    }
}
