using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Controller responsible for the level select scene.
/// </summary>
public class LevelSelectComponent : MonoBehaviour {

    [SerializeField]
    private Text goldAmountText;
    [SerializeField]
    private LevelRunDataListComponent levelRunList;
    [SerializeField]
    private ScrollableTabsAddon scrollableLevelRunTabsAddon;
    [SerializeField]
    private Button shopButton;
    [SerializeField]
    private Button logoutButton;
    [SerializeField]
    private GUIHighligherComponent guiHighligherComponent;
    [SerializeField]
    private MessagePopupComponent messagePopupComponent;
    [SerializeField]
    private RectTransform goldAmountRectTransform;
    [SerializeField]
    private RectTransform shopButtonRectTransform;
    [SerializeField]
    private RectTransform loadingGroupRect;

    private PlayerModel PlayerModel { get { return PlayerModel.Instance; } }
    private LevelRunModel LevelRunModel { get { return LevelRunModel.Instance; } }

    public void Awake() {
        if (this.goldAmountText == null) {
            throw new NullReferenceException("goldAmountText is null");
        }

        if (this.levelRunList == null) {
            throw new NullReferenceException("levelRunList is null");
        }

        if (this.scrollableLevelRunTabsAddon == null) {
            throw new NullReferenceException("scrollableLevelRunTabsAddon is null");
        }

        if (this.shopButton == null) {
            throw new NullReferenceException("shopButton is null");
        }

        if (this.logoutButton == null) {
            throw new NullReferenceException("logoutButton is null");
        }

        if (this.guiHighligherComponent == null) {
            throw new NullReferenceException("guiHighligherComponent is null");
        }

        if (this.messagePopupComponent == null) {
            throw new NullReferenceException("messagePopupComponent is null");
        }

        if (this.goldAmountRectTransform == null) {
            throw new NullReferenceException("goldAmountRectTransform is null");
        }

        if (this.shopButtonRectTransform == null) {
            throw new NullReferenceException("shopButtonRectTransform is null");
        }

        if (this.loadingGroupRect == null) {
            throw new NullReferenceException("loadingGroupRect is null");
        }

        this.shopButton.onClick.AddListener(GoToShopScene);
        this.logoutButton.onClick.AddListener(Logout);
        this.loadingGroupRect.gameObject.SetActive(false);
    }

    public void Start() {
        bool showLevelRunTutorial = !this.PlayerModel.PlayerGameData.FirstLevelRunTutorialCompleted;
        Action<int> onLevelRunButtonClickedAction = (levelNumber => {
            this.HideMessageAndHighlight();
            this.loadingGroupRect.gameObject.SetActive(true);
            LevelRunData levelRunData = LevelsDataManager.GetLevelData(levelNumber);
            LevelRunModel.Initialize(levelRunData, this.GetHeroSpawnData(), levelNumber, showLevelRunTutorial);
            SceneManager.LoadScene(Constants.Scenes.LevelRunSceneName);
        });

        int numberOfLevels = LevelsDataManager.GetNumberOfLevels();
        int highestUnlockedLevelNumber = Mathf.Clamp(this.PlayerModel.PlayerGameData.HighestCompletedLevelNumber + 1, 1, numberOfLevels);
        this.levelRunList.Initialize(numberOfLevels: numberOfLevels, highestUnlockedLevelNumber: highestUnlockedLevelNumber, onListItemClickedAction: onLevelRunButtonClickedAction);
        this.scrollableLevelRunTabsAddon.Initialize();
        this.scrollableLevelRunTabsAddon.SetTab((highestUnlockedLevelNumber - 1) / Constants.Scenes.LevelSelect.NumberOdDisplayedLevelsPerTab);
        this.goldAmountText.text = this.PlayerModel.PlayerGameData.GoldAmount.ToString();
        if (!this.PlayerModel.PlayerGameData.FirstLevelRunTutorialCompleted) {
            this.ShowGameIntroTutorial();
        }

        if (this.ShouldShowShopIntroTutorial()) {
            this.ShowShopIntroTutorial();
        }
    }

    public void Destroy() {
        this.shopButton.onClick.RemoveListener(GoToShopScene);
        this.logoutButton.onClick.RemoveListener(Logout);
    }

    private UnitSpawnData GetHeroSpawnData() {
        PlayerHeroData heroData = this.PlayerModel.PlayerGameData.GetHeroData(UnitType.HeroUnit);
        return new UnitSpawnData(
            platformType: Constants.Platforms.PlatformType.Bottom,
            positionOnPlatformInMeters: 0f,
            unitType: heroData.HeroType,
            unitLevelData: heroData.HeroLevelData,
            unitHasAutoAttack: false);
    }

    private void GoToShopScene() {
        SceneManager.LoadScene(Constants.Scenes.ShopSceneName);
    }

    private void Logout() {
        this.PlayerModel.Clear();
        this.LevelRunModel.Clear();
        SceneManager.LoadScene(Constants.Scenes.LoginSceneName);
    }

    private void ShowGameIntroTutorial() {
        LevelRunDataListItemComponent firstListItem = this.levelRunList.GetListItemComponent(0);
        RectTransform firstItemRect = firstListItem.GetComponent<RectTransform>();
        string message = string.Format(Constants.Strings.WelcomeInfoTutorialMessageTemplate, this.PlayerModel.PlayerGameData.Username);
        this.ShowMessageAndHighlight(firstItemRect, message, null, false);
    }

    private bool ShouldShowShopIntroTutorial() {
        return this.PlayerModel.PlayerGameData.HighestCompletedLevelNumber > 0 && !this.PlayerModel.PlayerGameData.ShopIntroTutorialCompleted;
    }

    private void ShowShopIntroTutorial() {
        UnityAction OnTotalGoldInfoMessageConfirmed = () => {
            ShowShopIntroMessage();
            this.PlayerModel.PlayerGameData.ShopIntroTutorialCompleted = true;
        };

        this.ShowMessageAndHighlight(this.goldAmountRectTransform, Constants.Strings.TotalGoldTutorialMessage, OnTotalGoldInfoMessageConfirmed);
    }

    private void ShowShopIntroMessage() {
        this.ShowMessageAndHighlight(this.shopButtonRectTransform, Constants.Strings.ShopIntroTutorialMessage, null, false);
    }

    private void ShowMessageAndHighlight(RectTransform highlightedRectTranform, string messsage, UnityAction onConfirmedAction = null, bool showConfirmationButton = true) {
        this.guiHighligherComponent.HighlightUIElement(highlightedRectTranform);
        this.messagePopupComponent.Show(messsage, onConfirmedAction, showConfirmationButton);
    }

    private void HideMessageAndHighlight() {
        this.guiHighligherComponent.HideHighlight();
        this.messagePopupComponent.Hide();
    }
}