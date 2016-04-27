using System;
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
    }

    public void Start() {
        this.PlayerModel.Initialize();
        Action<LevelRunData> onLevelRunButtonClickedAction = (levelRunData => {
            LevelRunModel.Initialize(levelRunData);
            SceneManager.LoadScene(Constants.Scenes.LevelRunSceneName);
        });

        IList<LevelRunData> runData = this.GetLevelRunData();
        int highestUnlockedLevelNumber = Mathf.Clamp(this.PlayerModel.PlayerGameData.HighestCompletedLevelNumber + 1, 1, runData.Count);
        this.levelRunList.Initialize(listItemData: runData, highestUnlockedLevelNumber: highestUnlockedLevelNumber, onListItemClickedAction: onLevelRunButtonClickedAction);
        this.scrollableLevelRunTabsAddon.Initialize();
        this.goldAmountText.text = PlayerModel.Instance.PlayerGameData.GoldAmount.ToString();
    }

    private IList<LevelRunData> GetLevelRunData() {
        //TODO move this to appropriate place when the level editor is done.
        IList<UnitSpawnData> defendingTeamSpawnData = new UnitSpawnData[] {
            new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Bottom, positionOnPlatformInMeters: 10f, unitType: UnitType.DefendingMeleeUnit, unitLevel: 1, unitHasAutoAttack: true),
            new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Top, positionOnPlatformInMeters: 13f, unitType: UnitType.DefendingMeleeUnit, unitLevel: 1, unitHasAutoAttack: true),
            new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Bottom, positionOnPlatformInMeters: 18f, unitType: UnitType.DefendingArcherUnit, unitLevel: 1, unitHasAutoAttack: true),
            new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Top, positionOnPlatformInMeters: 25f, unitType: UnitType.DefendingMeleeUnit, unitLevel: 1, unitHasAutoAttack: true),
            new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Top, positionOnPlatformInMeters: 26f, unitType: UnitType.DefendingArcherUnit, unitLevel: 1, unitHasAutoAttack: true),
            new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Top, positionOnPlatformInMeters: 32f, unitType: UnitType.DefendingMeleeUnit, unitLevel: 1, unitHasAutoAttack: true),
            new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Top, positionOnPlatformInMeters: 33f, unitType: UnitType.DefendingMeleeUnit, unitLevel: 1, unitHasAutoAttack: true),
            new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Bottom, positionOnPlatformInMeters: 33.5f, unitType: UnitType.DefendingMeleeUnit, unitLevel: 1, unitHasAutoAttack: true),
        };

        IList<LootItemSpawnData> lootSpawnData = new LootItemSpawnData[] {
            new LootItemSpawnData(platformType: Constants.Platforms.PlatformType.Top, positionOnPlatformInMeters: 14f, lootItemType: LootItemType.Ammunition, lootItemAmount: 5),
            new LootItemSpawnData(platformType: Constants.Platforms.PlatformType.Top, positionOnPlatformInMeters: 26.5f, lootItemType: LootItemType.Ammunition, lootItemAmount: 5),
            new LootItemSpawnData(platformType: Constants.Platforms.PlatformType.Top, positionOnPlatformInMeters: 33.5f, lootItemType: LootItemType.Gold, lootItemAmount: 100),
        };

        IList<LevelRunData> listItemData = new List<LevelRunData>() {
            new LevelRunData(levelNumber: 1, lengthInMeters: 10, defendingTeamUnitSpawnData: new UnitSpawnData[0], lootSpawnData: new LootItemSpawnData[0]),
            new LevelRunData(levelNumber: 2, lengthInMeters: 35, defendingTeamUnitSpawnData: defendingTeamSpawnData, lootSpawnData: lootSpawnData),
            new LevelRunData(levelNumber: 3, lengthInMeters: 35, defendingTeamUnitSpawnData: defendingTeamSpawnData, lootSpawnData: lootSpawnData),
            new LevelRunData(levelNumber: 4, lengthInMeters: 35, defendingTeamUnitSpawnData: defendingTeamSpawnData, lootSpawnData: lootSpawnData),
            new LevelRunData(levelNumber: 5, lengthInMeters: 35, defendingTeamUnitSpawnData: defendingTeamSpawnData, lootSpawnData: lootSpawnData),
            new LevelRunData(levelNumber: 6, lengthInMeters: 35, defendingTeamUnitSpawnData: defendingTeamSpawnData, lootSpawnData: lootSpawnData),
            new LevelRunData(levelNumber: 7, lengthInMeters: 35, defendingTeamUnitSpawnData: defendingTeamSpawnData, lootSpawnData: lootSpawnData),
            new LevelRunData(levelNumber: 8, lengthInMeters: 35, defendingTeamUnitSpawnData: defendingTeamSpawnData, lootSpawnData: lootSpawnData),
            new LevelRunData(levelNumber: 9, lengthInMeters: 35, defendingTeamUnitSpawnData: defendingTeamSpawnData, lootSpawnData: lootSpawnData),
            new LevelRunData(levelNumber: 10, lengthInMeters: 35, defendingTeamUnitSpawnData: defendingTeamSpawnData, lootSpawnData: lootSpawnData),
            new LevelRunData(levelNumber: 11, lengthInMeters: 35, defendingTeamUnitSpawnData: defendingTeamSpawnData, lootSpawnData: lootSpawnData),
            new LevelRunData(levelNumber: 12, lengthInMeters: 35, defendingTeamUnitSpawnData: defendingTeamSpawnData, lootSpawnData: lootSpawnData),
            new LevelRunData(levelNumber: 13, lengthInMeters: 35, defendingTeamUnitSpawnData: defendingTeamSpawnData, lootSpawnData: lootSpawnData),
            new LevelRunData(levelNumber: 14, lengthInMeters: 35, defendingTeamUnitSpawnData: defendingTeamSpawnData, lootSpawnData: lootSpawnData),
            new LevelRunData(levelNumber: 15, lengthInMeters: 35, defendingTeamUnitSpawnData: defendingTeamSpawnData, lootSpawnData: lootSpawnData),
            new LevelRunData(levelNumber: 16, lengthInMeters: 35, defendingTeamUnitSpawnData: defendingTeamSpawnData, lootSpawnData: lootSpawnData),
            new LevelRunData(levelNumber: 17, lengthInMeters: 35, defendingTeamUnitSpawnData: defendingTeamSpawnData, lootSpawnData: lootSpawnData),
            new LevelRunData(levelNumber: 18, lengthInMeters: 35, defendingTeamUnitSpawnData: defendingTeamSpawnData, lootSpawnData: lootSpawnData),
            new LevelRunData(levelNumber: 19, lengthInMeters: 35, defendingTeamUnitSpawnData: defendingTeamSpawnData, lootSpawnData: lootSpawnData),
            new LevelRunData(levelNumber: 20, lengthInMeters: 35, defendingTeamUnitSpawnData: defendingTeamSpawnData, lootSpawnData: lootSpawnData),
        };

        return listItemData;
    }
}