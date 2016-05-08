using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Responsible for all of the GUI elements on the screen for the level run
/// </summary>
public class LevelRunGUIComponent : MonoBehaviour {

    [SerializeField]
    private LevelRunComponent levelRunComponent;
    [SerializeField]
    private Text progressInfoText;
    [SerializeField]
    private Slider progressBar;
    [SerializeField]
    private Text ammunitionLootInfoText;
    [SerializeField]
    private Text goldLootInfoText;
    [SerializeField]
    private Text healthInfoText;
    [SerializeField]
    private Slider healthBar;
    [SerializeField]
    private RectTransform pauseMenuGroup;
    [SerializeField]
    private RectTransform runInfoGroup;
    [SerializeField]
    private Text runInfoHeaderText;
    [SerializeField]
    private Button runInfoStartButton;
    [SerializeField]
    private Button runInfoExitButton;
    [SerializeField]
    private Button pauseGameButton;
    [SerializeField]
    private Button pauseMenuResumeGameButton;
    [SerializeField]
    private Button pauseMenuRestartGameButton;
    [SerializeField]
    private Button pauseMenuExitGameButton;
    [SerializeField]
    private RectTransform levelFailedInfoGroup;
    [SerializeField]
    private LevelCompletedGUIComponent levelCompletedComponent;
    [SerializeField]
    private GUIHighligherComponent guiHighligherComponent;
    [SerializeField]
    private MessagePopupComponent messagePopupComponent;
    [SerializeField]
    private RectTransform healthGroupRectTransform;
    [SerializeField]
    private RectTransform ammunitionGroupRectTransform;
    [SerializeField]
    private RectTransform progressGroupRectTransform;
    [SerializeField]
    private RectTransform goldAmountGroupRectTransform;

    private LevelRunModel LevelRunModel { get { return LevelRunModel.Instance; } }
    private Unit trackedUnit;
    private bool guiInitialized = false;
    private bool commandsTutorialShownOnce = false;

    public void Awake() {
        if (this.levelRunComponent == null) {
            throw new NullReferenceException("levelRunComponent is null");
        }

        if (this.progressInfoText == null) {
            throw new NullReferenceException("progressInfoText is null");
        }

        if (this.progressBar == null) {
            throw new NullReferenceException("progressBar is null");
        }

        if (this.ammunitionLootInfoText == null) {
            throw new NullReferenceException("ammunitionLootInfoText is null");
        }

        if (this.goldLootInfoText == null) {
            throw new NullReferenceException("goldLootInfoText is null");
        }

        if (this.healthInfoText == null) {
            throw new NullReferenceException("healthInfoText is null");
        }

        if (this.pauseMenuGroup == null) {
            throw new NullReferenceException("pauseMenuGroup is null");
        }

        if (this.runInfoGroup == null) {
            throw new NullReferenceException("runInfoGroup is null");
        }

        if (this.runInfoHeaderText == null) {
            throw new NullReferenceException("runInfoHeaderText is null");
        }

        if (this.runInfoStartButton == null) {
            throw new NullReferenceException("runInfoStartButton is null");
        }

        if (this.runInfoExitButton == null) {
            throw new NullReferenceException("runInfoExitButton is null");
        }

        if (this.pauseGameButton == null) {
            throw new NullReferenceException("pauseGameButton is null");
        }

        if (this.pauseMenuResumeGameButton == null) {
            throw new NullReferenceException("pauseMenuResumeGameButton is null");
        }

        if (this.pauseMenuRestartGameButton == null) {
            throw new NullReferenceException("pauseMenuRestartGameButton is null");
        }

        if (this.pauseMenuExitGameButton == null) {
            throw new NullReferenceException("pauseMenuExitGameButton is null");
        }

        if (this.levelFailedInfoGroup == null) {
            throw new NullReferenceException("levelFailedInfoGroup is null");
        }

        if (this.levelCompletedComponent == null) {
            throw new NullReferenceException("levelCompletedComponent is null");
        }

        if (this.guiHighligherComponent == null) {
            throw new NullReferenceException("guiHighligherComponent is null");
        }

        if (this.messagePopupComponent == null) {
            throw new NullReferenceException("messagePopupComponent is null");
        }

        if (this.healthGroupRectTransform == null) {
            throw new NullReferenceException("healthGroupRectTransform is null");
        }

        if (this.ammunitionGroupRectTransform == null) {
            throw new NullReferenceException("ammunitionGroupRectTransform is null");
        }

        if (this.progressGroupRectTransform == null) {
            throw new NullReferenceException("progressGroupRectTransform is null");
        }

        if (this.goldAmountGroupRectTransform == null) {
            throw new NullReferenceException("goldAmountGroupRectTransform is null");
        }


        this.runInfoStartButton.onClick.AddListener(StartRun);
        this.runInfoExitButton.onClick.AddListener(ExitGame);
        this.pauseMenuGroup.gameObject.SetActive(false);
        this.pauseMenuRestartGameButton.onClick.AddListener(RestartGameClickedHandler);
        this.pauseMenuResumeGameButton.onClick.AddListener(ResumeGameClickedHandler);
        this.pauseMenuExitGameButton.onClick.AddListener(ExitGame);
        this.levelFailedInfoGroup.gameObject.SetActive(false);
        this.levelCompletedComponent.ShowComponent(false);
        this.pauseGameButton.interactable = false;
        this.pauseGameButton.onClick.AddListener(PauseGameClickedHandler);
    }

    public void Start() {
        if (!this.levelRunComponent.IsInitialized) {
            this.levelRunComponent.Initialized += InitializeGUI;
            return;
        }

        this.InitializeGUI();
    }

    public void Destroy() {
        this.runInfoStartButton.onClick.RemoveListener(StartRun);
        this.runInfoExitButton.onClick.RemoveListener(ExitGame);
        this.pauseGameButton.onClick.RemoveListener(PauseGameClickedHandler);
        this.pauseMenuRestartGameButton.onClick.RemoveListener(RestartGameClickedHandler);
        this.pauseMenuResumeGameButton.onClick.RemoveListener(ResumeGameClickedHandler);
        this.pauseMenuExitGameButton.onClick.RemoveListener(ExitGame);
    }

    public void Update() {
        if (!this.guiInitialized) {
            return;
        }

        float currentProgressInMeters = this.LevelRunModel.ProgressTracker.CurrentProgressInMeters;
        float levelLengthInMeters = this.LevelRunModel.LevelRunData.LengthInMeters;
        this.progressInfoText.text = string.Format(Constants.Scenes.LevelRun.LevelProgressStringTemplate, currentProgressInMeters, levelLengthInMeters);
        this.progressBar.value = currentProgressInMeters / levelLengthInMeters;
        if (this.trackedUnit != null) {
            this.ammunitionLootInfoText.text = this.trackedUnit.Weapon.NumberOfBullets.ToString();
            this.healthBar.value = this.trackedUnit.Health / (float)this.trackedUnit.MaxHealth;
            this.healthInfoText.text = this.trackedUnit.Health.ToString();
        }

        this.goldLootInfoText.text = this.LevelRunModel.LootItemManager.GetCollectedLootAmountByType(LootItemType.Gold).ToString();
        if (this.ShouldShowTutorial() && currentProgressInMeters > 2f && !this.commandsTutorialShownOnce) {
            string message = string.Empty;
#if UNITY_STANDALONE
            message = Constants.Strings.WindowsCommandsTutorialMessage;
#endif

#if UNITY_ANDROID
            message = Constants.Strings.MobileCommandsTutorialMessage;
#endif
            this.levelRunComponent.PauseGame();
            UnityAction onMessageConfirmedAction = () => {
                this.levelRunComponent.ResumeGame();
            };

            this.messagePopupComponent.Show(message, onMessageConfirmedAction);
            this.commandsTutorialShownOnce = true;
        }
    }

    private void InitializeGUI() {
        this.guiInitialized = true;
        this.levelRunComponent.Initialized -= InitializeGUI;
        this.trackedUnit = this.LevelRunModel.HeroUnit;
        this.LevelRunModel.RunFinished += OnRunFinishedHandler;
        this.runInfoHeaderText.text += " " + this.LevelRunModel.LevelNumber;
        this.runInfoGroup.gameObject.SetActive(true);
        if (this.ShouldShowTutorial()) {
            this.ShowTutorialGUI();
        }
    }

    private void OnUnitToTrackAddedHandler(Unit unitToTrack) {
        if (this.trackedUnit != null) {
            throw new InvalidOperationException("Cannot track more than one unit.");
        }

        this.trackedUnit = unitToTrack;
    }

    private void OnRunFinishedHandler(LevelRunFinishType finishType) {
        this.LevelRunModel.RunFinished -= OnRunFinishedHandler;
        this.pauseGameButton.interactable = false;
        if (finishType == LevelRunFinishType.RunCompleted) {
            this.levelCompletedComponent.Initialize(this.LevelRunModel.LevelCompletedRewardData);
            this.levelCompletedComponent.ShowComponent(true);
        }

        if (finishType == LevelRunFinishType.RunFailed) {
            this.levelFailedInfoGroup.gameObject.SetActive(true);
        }
    }

    private void StartRun() {
        this.runInfoGroup.gameObject.SetActive(false);
        this.pauseGameButton.interactable = true;
        this.levelRunComponent.StartRun();
    }

    private void PauseGameClickedHandler() {
        if (this.levelRunComponent.IsGamePaused) {
            return;
        }

        this.levelRunComponent.PauseGame();
        this.pauseMenuGroup.gameObject.SetActive(true);
    }

    private void ResumeGameClickedHandler() {
        if (!this.levelRunComponent.IsGamePaused) {
            return;
        }

        this.levelRunComponent.ResumeGame();
        this.pauseMenuGroup.gameObject.SetActive(false);
    }

    private void RestartGameClickedHandler() {
        if (!this.levelRunComponent.IsGameFinished) {
            this.levelRunComponent.FinishRun();
        }

        this.pauseMenuGroup.gameObject.SetActive(false);
        SceneManager.LoadScene(Constants.Scenes.LevelRunSceneName);
    }

    private void ExitGame() {
        if (!this.levelRunComponent.IsGameFinished) {
            this.levelRunComponent.FinishRun();
        }

        SceneManager.LoadScene(Constants.Scenes.LevelSelectSceneName);
    }

    private bool ShouldShowTutorial() {
        return this.LevelRunModel.ShowRunTutorial;
    }

    private void ShowTutorialGUI() {
        UnityAction OnGoldInfoMessageConfirmed = HideMessageAndHighlight;
        UnityAction OnProgressInfoMessageConfirmed = () => ShowGoldInfoMessage(OnGoldInfoMessageConfirmed);
        UnityAction OnAmmunitionInfoMessageConfirmed = () => ShowProgressInfoMessage(OnProgressInfoMessageConfirmed);
        UnityAction OnHealthInfoMessageConfirmed = () => ShowAmmunitionMessage(OnAmmunitionInfoMessageConfirmed);
        ShowHealthInfoMessage(OnHealthInfoMessageConfirmed);
    }

    private void ShowGoldInfoMessage(UnityAction onMessageConfirmedAction) {
        this.ShowMessageAndHighlight(this.goldAmountGroupRectTransform, Constants.Strings.GoldAmountInfoTutorialMessage, onMessageConfirmedAction);
    }

    private void ShowProgressInfoMessage(UnityAction onMessageConfirmedAction) {
        this.ShowMessageAndHighlight(this.progressGroupRectTransform, Constants.Strings.ProgressInfoTutorialMessage, onMessageConfirmedAction);
    }

    private void ShowHealthInfoMessage(UnityAction onMessageConfirmedAction) {
        this.ShowMessageAndHighlight(this.healthGroupRectTransform, Constants.Strings.HealthInfoTutorialMessage, onMessageConfirmedAction);
    }

    private void ShowAmmunitionMessage(UnityAction onMessageConfirmedAction) {
        this.ShowMessageAndHighlight(this.ammunitionGroupRectTransform, Constants.Strings.AmmunitionInfoTutorialMessage, onMessageConfirmedAction);
    }

    private void ShowMessageAndHighlight(RectTransform highlightedRectTranform, string messsage, UnityAction onConfirmedAction) {
        this.guiHighligherComponent.HighlightUIElement(highlightedRectTranform);
        this.messagePopupComponent.Show(messsage, onConfirmedAction);
    }

    private void HideMessageAndHighlight() {
        this.guiHighligherComponent.HideHighlight();
        this.messagePopupComponent.Hide();
    }
}