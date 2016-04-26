﻿using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Responsible for all of the GUI elements on the screen for the level run
/// </summary>
public class LevelRunGUIComponent : MonoBehaviour {

    [SerializeField]
    private Text ProgressInfoText;
    [SerializeField]
    private Slider ProgressBar;
    [SerializeField]
    private Text AmmunitionLootInfoText;
    [SerializeField]
    private Text GoldLootInfoText;
    [SerializeField]
    private Text HealthInfoText;
    [SerializeField]
    private Slider HealthBar;
    [SerializeField]
    private RectTransform PauseMenuGroup;
    [SerializeField]
    private RectTransform RunInfoGroup;
    [SerializeField]
    private Button PauseGameButton;

    private LevelRunManager levelRunManager;
    private Team trackedTeam;
    private Unit trackedUnit;

    public void Awake() {
        if (this.ProgressInfoText == null) {
            throw new NullReferenceException("ProgressInfoText is null");
        }

        if (this.ProgressBar == null) {
            throw new NullReferenceException("ProgressBar is null");
        }

        if (this.AmmunitionLootInfoText == null) {
            throw new NullReferenceException("AmmunitionLootInfoText is null");
        }

        if (this.GoldLootInfoText == null) {
            throw new NullReferenceException("GoldLootInfoText is null");
        }

        if (this.HealthInfoText == null) {
            throw new NullReferenceException("HealthInfoText is null");
        }

        if (this.PauseMenuGroup == null) {
            throw new NullReferenceException("PauseMenuGroup is null");
        }

        if (this.RunInfoGroup == null) {
            throw new NullReferenceException("RunInfoGroup is null");
        }

        if (this.PauseGameButton == null) {
            throw new NullReferenceException("PauseGameButton is null");
        }

        this.PauseMenuGroup.gameObject.SetActive(false);
        this.RunInfoGroup.gameObject.SetActive(true);
        this.PauseGameButton.interactable = false;
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
    }

    private void OnUnitToTrackAddedHandler(Unit unitToTrack) {
        if (this.trackedUnit != null) {
            throw new InvalidOperationException("Cannot track more than one unit.");
        }

        this.trackedUnit = unitToTrack;
    }

    private void OnRunFinishedHandler() {
        this.levelRunManager.RunFinished -= OnRunFinishedHandler;
        this.trackedTeam.UnitAdded -= OnUnitToTrackAddedHandler;
    }

    public void Update() {
        if (this.levelRunManager == null) {
            return;
        }

        //TODO extract the format in constants
        this.ProgressInfoText.text = string.Format("{0:0.} / {1:0.}", this.levelRunManager.CurrentProgressInMeters, this.levelRunManager.LevelLengthInMeters);
        this.ProgressBar.value = this.levelRunManager.CurrentProgressInMeters / (float)this.levelRunManager.LevelLengthInMeters;
        if (this.trackedUnit != null) {
            this.AmmunitionLootInfoText.text = this.trackedUnit.Weapon.NumberOfBullets.ToString();
            this.HealthBar.value = this.trackedUnit.Health / (float)this.trackedUnit.MaxHealth;
            this.HealthInfoText.text = this.trackedUnit.Health.ToString();
        }

        this.GoldLootInfoText.text = this.levelRunManager.LootItemManager.GetCollectedLootAmountByType(LootItemType.Gold).ToString();
    }

    public void StartRun() {
        this.RunInfoGroup.gameObject.SetActive(false);
        this.PauseGameButton.interactable = true;
        this.levelRunManager.StartRun();
    }

    public void PauseGame() {
        if (this.levelRunManager.IsGamePaused) {
            return;
        }

        this.levelRunManager.PauseGame();
        this.PauseMenuGroup.gameObject.SetActive(true);
    }

    public void ResumeGame() {
        if (!this.levelRunManager.IsGamePaused) {
            return;
        }

        this.levelRunManager.ResumeGame();
        this.PauseMenuGroup.gameObject.SetActive(false);
    }

    public void RestartGame() {
        this.levelRunManager.RestartGame();
        this.PauseMenuGroup.gameObject.SetActive(false);
    }


    public void ExitGame() {
        //TODO implement this properly
        SceneManager.LoadScene(Constants.Scenes.AllLevelsSceneName);
    }
}