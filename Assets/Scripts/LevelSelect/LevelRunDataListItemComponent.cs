using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Component for displaying info for one level run data.
/// </summary>
public class LevelRunDataListItemComponent : MonoBehaviour {
    [SerializeField]
    Text levelInfoText;
    [SerializeField]
    Image lockIconImage;
    [SerializeField]
    Button levelInfoButton;
    [SerializeField]
    Image backgroundImage;
    [SerializeField]
    Sprite lockedBackgroundSprite;
    [SerializeField]
    Sprite notLockedBackgroundSprite;

    int levelNumber;
    bool isLocked;
    bool isInitialized;
    private Action<int> onListItemClickedAction;

    public void Awake() {
        if (this.levelInfoText == null) {
            throw new NullReferenceException("levelInfoText is null");
        }

        if (this.lockIconImage == null) {
            throw new NullReferenceException("lockIconImage is null");
        }

        if (this.levelInfoButton == null) {
            throw new NullReferenceException("levelInfoButton is null");
        }

        if (this.backgroundImage == null) {
            throw new NullReferenceException("backgroundImage is null");
        }

        if (this.lockedBackgroundSprite == null) {
            throw new NullReferenceException("lockedBackgroundSprite is null");
        }

        if (this.notLockedBackgroundSprite == null) {
            throw new NullReferenceException("notLockedBackgroundSprite is null");
        }
    }

    public void Initialize(int levelNumber, bool isLocked, Action<int> onListItemClickedAction) {
        if (levelNumber < 1) {
            throw new ArgumentOutOfRangeException("levelNumber", "Cannot be less than one.");
        }

        if (onListItemClickedAction == null) {
            throw new ArgumentNullException("onListItemClickedAction");
        }

        this.isLocked = isLocked;
        this.onListItemClickedAction = onListItemClickedAction;
        this.levelNumber = levelNumber;
        this.levelInfoText.text = this.isLocked ? string.Empty : this.levelNumber.ToString();
        this.levelInfoText.gameObject.SetActive(!this.isLocked);
        this.lockIconImage.gameObject.SetActive(this.isLocked);
        this.backgroundImage.sprite = this.isLocked ? this.lockedBackgroundSprite : this.notLockedBackgroundSprite;
        if (!this.isLocked) {
            this.levelInfoButton.onClick.AddListener(onClickAction);
        }

        this.isInitialized = true;
    }

    public void Destroy() {
        if (!this.isInitialized) {
            return;
        }

        if (!this.isLocked) {
            this.levelInfoButton.onClick.RemoveListener(onClickAction);
        }
    }

    private void onClickAction() {
        this.onListItemClickedAction(this.levelNumber);
    }
}
