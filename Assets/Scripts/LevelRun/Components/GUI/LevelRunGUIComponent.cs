using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Responsible for all of the GUI elements on the screen for the level run
/// </summary>
public class LevelRunGUIComponent : MonoBehaviour {

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
    private Button pauseGameButton;
    [SerializeField]
    private RectTransform levelFailedInfoGroup;
    [SerializeField]
    private RectTransform levelCompletedInfoGroup;

    private LevelRunManager levelRunManager;
    private Team trackedTeam;
    private Unit trackedUnit;

    public void Awake() {
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

        if (this.pauseGameButton == null) {
            throw new NullReferenceException("pauseGameButton is null");
        }

        if (this.levelFailedInfoGroup == null) {
            throw new NullReferenceException("levelFailedInfoGroup is null");
        }

        if (this.levelCompletedInfoGroup == null) {
            throw new NullReferenceException("levelCompletedInfoGroup is null");
        }
        this.pauseMenuGroup.gameObject.SetActive(false);
        this.levelFailedInfoGroup.gameObject.SetActive(false);
        this.levelCompletedInfoGroup.gameObject.SetActive(false);
        this.pauseGameButton.interactable = false;
    }

    public void Initialize(LevelRunManager levelRunManager) {
        if (levelRunManager == null) {
            throw new ArgumentNullException("levelRunManager");
        }

        this.levelRunManager = levelRunManager;
        this.levelRunManager.RunFinished += OnRunFinishedHandler;
        this.trackedTeam = this.levelRunManager.AttackingTeam;
        if (this.trackedTeam.UnitsInTeam.Count == 0) {
            this.trackedTeam.UnitAdded += OnUnitToTrackAddedHandler;
        }
        else {
            if (trackedTeam.UnitsInTeam.Count > 1) {
                throw new InvalidOperationException("Cannot track more than one unit.");
            }

            this.trackedUnit = this.trackedTeam.UnitsInTeam[0];
        }

        this.runInfoHeaderText.text += " " + this.levelRunManager.LevelRunData.LevelNumber;
        this.runInfoGroup.gameObject.SetActive(true);
    }

    private void OnUnitToTrackAddedHandler(Unit unitToTrack) {
        if (this.trackedUnit != null) {
            throw new InvalidOperationException("Cannot track more than one unit.");
        }

        this.trackedUnit = unitToTrack;
    }

    private void OnRunFinishedHandler(LevelRunFinishType finishType) {
        this.levelRunManager.RunFinished -= OnRunFinishedHandler;
        this.trackedTeam.UnitAdded -= OnUnitToTrackAddedHandler;
        this.pauseGameButton.interactable = false;
        if (finishType == LevelRunFinishType.RunCompleted) {
            this.levelCompletedInfoGroup.gameObject.SetActive(true);
        }

        if (finishType == LevelRunFinishType.RunFailed) {
            this.levelFailedInfoGroup.gameObject.SetActive(true);
        }
    }

    public void Update() {
        if (this.levelRunManager == null) {
            return;
        }

        //TODO extract the format in constants
        this.progressInfoText.text = string.Format("{0:0.} / {1:0.}", this.levelRunManager.CurrentProgressInMeters, this.levelRunManager.LevelLengthInMeters);
        this.progressBar.value = this.levelRunManager.CurrentProgressInMeters / (float)this.levelRunManager.LevelLengthInMeters;
        if (this.trackedUnit != null) {
            this.ammunitionLootInfoText.text = this.trackedUnit.Weapon.NumberOfBullets.ToString();
            this.healthBar.value = this.trackedUnit.Health / (float)this.trackedUnit.MaxHealth;
            this.healthInfoText.text = this.trackedUnit.Health.ToString();
        }

        this.goldLootInfoText.text = this.levelRunManager.LootItemManager.GetCollectedLootAmountByType(LootItemType.Gold).ToString();
    }

    public void StartRun() {
        this.runInfoGroup.gameObject.SetActive(false);
        this.pauseGameButton.interactable = true;
        this.levelRunManager.StartRun();
    }

    public void PauseGame() {
        if (this.levelRunManager.IsGamePaused) {
            return;
        }

        this.levelRunManager.PauseGame();
        this.pauseMenuGroup.gameObject.SetActive(true);
    }

    public void ResumeGame() {
        if (!this.levelRunManager.IsGamePaused) {
            return;
        }

        this.levelRunManager.ResumeGame();
        this.pauseMenuGroup.gameObject.SetActive(false);
    }

    public void RestartGame() {
        if (!this.levelRunManager.IsGameFinished) {
            this.levelRunManager.FinishRun();
        }

        this.pauseMenuGroup.gameObject.SetActive(false);
        SceneManager.LoadScene(Constants.Scenes.LevelRunSceneName);
    }


    public void ExitGame() {
        if (!this.levelRunManager.IsGameFinished) {
            this.levelRunManager.FinishRun();
        }

        SceneManager.LoadScene(Constants.Scenes.LevelSelectSceneName);
    }
}