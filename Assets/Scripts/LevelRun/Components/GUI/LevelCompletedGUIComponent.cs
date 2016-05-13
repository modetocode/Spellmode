using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Responsible for displaying the rewards when the player completes a level. 
/// </summary>
public class LevelCompletedGUIComponent : MonoBehaviour {

    [SerializeField]
    private Text levelCompletedGoldRewardAmountText;
    [SerializeField]
    private Text goldLootedAmountText;
    [SerializeField]
    private Text totalGoldAmountText;
    [SerializeField]
    private Text firstTimeCompletedGoldAmountText;
    [SerializeField]
    private RectTransform firstTimeCompletedGroupRect;
    [SerializeField]
    private Button confirmButton;

    private LevelRunModel LevelRunModel { get { return LevelRunModel.Instance; } }

    public void Awake() {
        if (this.levelCompletedGoldRewardAmountText == null) {
            throw new NullReferenceException("levelCompletedGoldRewardAmountText");
        }

        if (this.goldLootedAmountText == null) {
            throw new NullReferenceException("goldLootedAmountText");
        }

        if (this.totalGoldAmountText == null) {
            throw new NullReferenceException("totalGoldAmountText");
        }

        if (this.firstTimeCompletedGoldAmountText == null) {
            throw new NullReferenceException("firstTimeCompletedGoldAmountText");
        }

        if (this.firstTimeCompletedGroupRect == null) {
            throw new NullReferenceException("firstTimeCompletedGroupRect");
        }

        if (this.confirmButton == null) {
            throw new NullReferenceException("confirmButton");
        }

        this.confirmButton.onClick.AddListener(ReturnToLevelSelect);
    }

    public void Destroy() {
        this.confirmButton.onClick.RemoveListener(ReturnToLevelSelect);
    }

    public void Initialize(LevelCompetedRewardData levelCompletedRewardData) {
        if (levelCompletedRewardData == null) {
            throw new ArgumentNullException("levelCompletedRewardData");
        }

        string rewardsPrefix = "+";
        this.levelCompletedGoldRewardAmountText.text = rewardsPrefix + levelCompletedRewardData.LevelCompletedGoldRewardAmount.ToString();
        this.firstTimeCompletedGoldAmountText.text = rewardsPrefix + levelCompletedRewardData.FirstTimeCompletedGoldRewardAmount.ToString();
        this.firstTimeCompletedGroupRect.gameObject.SetActive(levelCompletedRewardData.FirstTimeCompletedGoldRewardAmount > 0);
        this.goldLootedAmountText.text = rewardsPrefix + levelCompletedRewardData.GoldLootedAmount.ToString();
        this.totalGoldAmountText.text = levelCompletedRewardData.PlayerTotalGold.ToString();
    }

    public void ShowComponent(bool isVisible) {
        this.gameObject.SetActive(isVisible);
    }

    private void ReturnToLevelSelect() {
        SceneManager.LoadScene(Constants.Scenes.LevelSelectSceneName);
    }
}
