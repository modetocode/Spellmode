﻿using System;
using System.Collections.Generic;
using UnityEngine;
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

        this.shopButton.onClick.AddListener(GoToShopScene);
        this.logoutButton.onClick.AddListener(Logout);
    }

    public void Start() {
        Action<LevelRunData> onLevelRunButtonClickedAction = (levelRunData => {
            LevelRunModel.Initialize(levelRunData, this.GetHeroSpawnData());
            SceneManager.LoadScene(Constants.Scenes.LevelRunSceneName);
        });

        IList<LevelRunData> runData = LevelsDataManager.GetLevelsData();
        int numberOfLevels = LevelsDataManager.GetNumberOfLevels();
        int highestUnlockedLevelNumber = Mathf.Clamp(this.PlayerModel.PlayerGameData.HighestCompletedLevelNumber + 1, 1, numberOfLevels);
        this.levelRunList.Initialize(listItemData: runData, highestUnlockedLevelNumber: highestUnlockedLevelNumber, onListItemClickedAction: onLevelRunButtonClickedAction);
        this.scrollableLevelRunTabsAddon.Initialize();
        this.scrollableLevelRunTabsAddon.SetTab((highestUnlockedLevelNumber - 1) / Constants.Scenes.LevelSelect.NumberOdDisplayedLevelsPerTab);
        this.goldAmountText.text = this.PlayerModel.PlayerGameData.GoldAmount.ToString();
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
}