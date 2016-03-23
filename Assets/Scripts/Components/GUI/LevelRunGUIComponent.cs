using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Responsible for all of the GUI elements on the screen for the level run
/// </summary>
public class LevelRunGUIComponent : MonoBehaviour, ITickable {

    [SerializeField]
    private Text ProgressInfoText;
    private LevelRunManager levelRunManager;

    public void Initialize(LevelRunManager levelRunManager) {
        if (levelRunManager == null) {
            throw new ArgumentNullException("levelRunManager");
        }

        this.levelRunManager = levelRunManager;
    }


    public void Tick(float deltaTime) {
        if (this.levelRunManager == null) {
            return;
        }

        //TODO extract the format in constants
        this.ProgressInfoText.text = string.Format("{0:0.} / {1:0.}", this.levelRunManager.CurrentProgressInMeters, this.levelRunManager.LevelLengthInMeters);
    }

    public void OnTickingPaused(float deltaTime) {
    }

}