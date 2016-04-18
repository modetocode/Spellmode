using System;
using UnityEngine;
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

    private LevelRunManager levelRunManager;
    private Team trackedTeam;
    private Unit trackedUnit;

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

    public void PauseGame() {
        this.levelRunManager.PauseGame();
    }
}