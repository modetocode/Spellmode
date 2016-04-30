using System.Collections.Generic;

/// <summary>
/// Responsible for storing the data that are needed for all the levels in the game.
/// </summary>
public static class LevelsDataManager {


    public static IList<LevelRunData> GetLevelsData() {
        //TODO check if we should only return data for only one level
        return levelsData;
    }

    public static int GetNumberOfLevels() {
        return levelsData.Count;
    }

    private static IList<LevelRunData> levelsData = new List<LevelRunData> {
        new LevelRunData (
            levelNumber: 1,
            lengthInMeters: 20,
            defendingTeamUnitSpawnData: new List<UnitSpawnData> {
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Bottom, positionOnPlatformInMeters: 12, unitType:  UnitType.DefendingMeleeUnit, unitLevelData: new UnitLevelData(1)),
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Top, positionOnPlatformInMeters: 12, unitType:  UnitType.DefendingMeleeUnit, unitLevelData: new UnitLevelData(1))
            },
            lootSpawnData: new List<LootItemSpawnData> {
                new LootItemSpawnData(platformType: Constants.Platforms.PlatformType.Top, positionOnPlatformInMeters: 14, lootItemType: LootItemType.Gold, lootItemAmount: 20),
            }
        ),
        new LevelRunData (
            levelNumber: 2,
            lengthInMeters: 20,
            defendingTeamUnitSpawnData: new List<UnitSpawnData> {
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Bottom, positionOnPlatformInMeters: 10, unitType:  UnitType.DefendingMeleeUnit, unitLevelData: new UnitLevelData(1)),
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Bottom, positionOnPlatformInMeters: 12, unitType:  UnitType.DefendingMeleeUnit, unitLevelData: new UnitLevelData(1)),
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Top, positionOnPlatformInMeters: 10, unitType:  UnitType.DefendingMeleeUnit, unitLevelData: new UnitLevelData(1)),
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Top, positionOnPlatformInMeters: 12, unitType:  UnitType.DefendingMeleeUnit, unitLevelData: new UnitLevelData(1))
            },
            lootSpawnData: new List<LootItemSpawnData> {
                new LootItemSpawnData(platformType: Constants.Platforms.PlatformType.Bottom, positionOnPlatformInMeters: 11, lootItemType: LootItemType.Gold, lootItemAmount: 20),
                new LootItemSpawnData(platformType: Constants.Platforms.PlatformType.Top, positionOnPlatformInMeters: 13, lootItemType: LootItemType.Gold, lootItemAmount: 20),
            }
        ),
        new LevelRunData (
            levelNumber: 3,
            lengthInMeters: 30,
            defendingTeamUnitSpawnData: new List<UnitSpawnData> {
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Bottom, positionOnPlatformInMeters: 10, unitType:  UnitType.DefendingMeleeUnit, unitLevelData: new UnitLevelData(1)),
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Bottom, positionOnPlatformInMeters: 12, unitType:  UnitType.DefendingMeleeUnit, unitLevelData: new UnitLevelData(1)),
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Bottom, positionOnPlatformInMeters: 14, unitType:  UnitType.DefendingMeleeUnit, unitLevelData: new UnitLevelData(1)),
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Bottom, positionOnPlatformInMeters: 16, unitType:  UnitType.DefendingMeleeUnit, unitLevelData: new UnitLevelData(1)),
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Bottom, positionOnPlatformInMeters: 18, unitType:  UnitType.DefendingMeleeUnit, unitLevelData: new UnitLevelData(1)),
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Top, positionOnPlatformInMeters: 20, unitType:  UnitType.DefendingMeleeUnit, unitLevelData: new UnitLevelData(1)),
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Top, positionOnPlatformInMeters: 22, unitType:  UnitType.DefendingMeleeUnit, unitLevelData: new UnitLevelData(1)),
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Top, positionOnPlatformInMeters: 24, unitType:  UnitType.DefendingMeleeUnit, unitLevelData: new UnitLevelData(1)),
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Top, positionOnPlatformInMeters: 26, unitType:  UnitType.DefendingMeleeUnit, unitLevelData: new UnitLevelData(1)),
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Top, positionOnPlatformInMeters: 28, unitType:  UnitType.DefendingMeleeUnit, unitLevelData: new UnitLevelData(1)),
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Top, positionOnPlatformInMeters: 15, unitType:  UnitType.DefendingMeleeUnit, unitLevelData: new UnitLevelData(2)),
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Bottom, positionOnPlatformInMeters: 25, unitType:  UnitType.DefendingMeleeUnit, unitLevelData: new UnitLevelData(2)),
            },
            lootSpawnData: new List<LootItemSpawnData> {
                new LootItemSpawnData(platformType: Constants.Platforms.PlatformType.Bottom, positionOnPlatformInMeters: 15, lootItemType: LootItemType.Gold, lootItemAmount: 25),
                new LootItemSpawnData(platformType: Constants.Platforms.PlatformType.Top, positionOnPlatformInMeters: 25, lootItemType: LootItemType.Gold, lootItemAmount: 25),
            }
        ),
        new LevelRunData (
            levelNumber: 4,
            lengthInMeters: 35,
            defendingTeamUnitSpawnData: new List<UnitSpawnData> {
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Bottom, positionOnPlatformInMeters: 8, unitType:  UnitType.DefendingMeleeUnit, unitLevelData: new UnitLevelData(1)),
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Top, positionOnPlatformInMeters: 10, unitType:  UnitType.DefendingMeleeUnit, unitLevelData: new UnitLevelData(1)),
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Bottom, positionOnPlatformInMeters: 12, unitType:  UnitType.DefendingMeleeUnit, unitLevelData: new UnitLevelData(1)),
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Top, positionOnPlatformInMeters: 14, unitType:  UnitType.DefendingMeleeUnit, unitLevelData: new UnitLevelData(1)),
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Bottom, positionOnPlatformInMeters: 16, unitType:  UnitType.DefendingMeleeUnit, unitLevelData: new UnitLevelData(1)),
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Top, positionOnPlatformInMeters: 18, unitType:  UnitType.DefendingMeleeUnit, unitLevelData: new UnitLevelData(1)),
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Bottom, positionOnPlatformInMeters: 20, unitType:  UnitType.DefendingMeleeUnit, unitLevelData: new UnitLevelData(1)),
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Top, positionOnPlatformInMeters: 22, unitType:  UnitType.DefendingMeleeUnit, unitLevelData: new UnitLevelData(1)),
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Bottom, positionOnPlatformInMeters: 24, unitType:  UnitType.DefendingMeleeUnit, unitLevelData: new UnitLevelData(1)),
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Top, positionOnPlatformInMeters: 26, unitType:  UnitType.DefendingMeleeUnit, unitLevelData: new UnitLevelData(1)),
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Bottom, positionOnPlatformInMeters: 28, unitType:  UnitType.DefendingMeleeUnit, unitLevelData: new UnitLevelData(1)),
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Top, positionOnPlatformInMeters: 30, unitType:  UnitType.DefendingMeleeUnit, unitLevelData: new UnitLevelData(1)),
            },
            lootSpawnData: new List<LootItemSpawnData> {
                new LootItemSpawnData(platformType: Constants.Platforms.PlatformType.Top, positionOnPlatformInMeters: 20, lootItemType: LootItemType.Gold, lootItemAmount: 35),
                new LootItemSpawnData(platformType: Constants.Platforms.PlatformType.Bottom, positionOnPlatformInMeters: 26, lootItemType: LootItemType.Gold, lootItemAmount: 25),
                new LootItemSpawnData(platformType: Constants.Platforms.PlatformType.Top, positionOnPlatformInMeters: 16, lootItemType: LootItemType.Ammunition, lootItemAmount: 2),
            }
        ),
        new LevelRunData (
            levelNumber: 5,
            lengthInMeters: 40,
            defendingTeamUnitSpawnData: new List<UnitSpawnData> {
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Bottom, positionOnPlatformInMeters: 12, unitType:  UnitType.DefendingMeleeUnit, unitLevelData: new UnitLevelData(1)),
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Bottom, positionOnPlatformInMeters: 13, unitType:  UnitType.DefendingMeleeUnit, unitLevelData: new UnitLevelData(1)),
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Bottom, positionOnPlatformInMeters: 14, unitType:  UnitType.DefendingMeleeUnit, unitLevelData: new UnitLevelData(1)),
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Bottom, positionOnPlatformInMeters: 15, unitType:  UnitType.DefendingMeleeUnit, unitLevelData: new UnitLevelData(2)),
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Bottom, positionOnPlatformInMeters: 16, unitType:  UnitType.DefendingMeleeUnit, unitLevelData: new UnitLevelData(2)),
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Top, positionOnPlatformInMeters: 12, unitType:  UnitType.DefendingMeleeUnit, unitLevelData: new UnitLevelData(1)),
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Top, positionOnPlatformInMeters: 13, unitType:  UnitType.DefendingMeleeUnit, unitLevelData: new UnitLevelData(1)),
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Top, positionOnPlatformInMeters: 14, unitType:  UnitType.DefendingMeleeUnit, unitLevelData: new UnitLevelData(1)),
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Top, positionOnPlatformInMeters: 15, unitType:  UnitType.DefendingMeleeUnit, unitLevelData: new UnitLevelData(2)),
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Top, positionOnPlatformInMeters: 16, unitType:  UnitType.DefendingMeleeUnit, unitLevelData: new UnitLevelData(2)),
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Bottom, positionOnPlatformInMeters: 30, unitType:  UnitType.DefendingMeleeUnit, unitLevelData: new UnitLevelData(1)),
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Bottom, positionOnPlatformInMeters: 31, unitType:  UnitType.DefendingMeleeUnit, unitLevelData: new UnitLevelData(1)),
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Bottom, positionOnPlatformInMeters: 32, unitType:  UnitType.DefendingMeleeUnit, unitLevelData: new UnitLevelData(1)),
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Bottom, positionOnPlatformInMeters: 33, unitType:  UnitType.DefendingMeleeUnit, unitLevelData: new UnitLevelData(2)),
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Bottom, positionOnPlatformInMeters: 34, unitType:  UnitType.DefendingMeleeUnit, unitLevelData: new UnitLevelData(2)),
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Top, positionOnPlatformInMeters: 30, unitType:  UnitType.DefendingMeleeUnit, unitLevelData: new UnitLevelData(1)),
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Top, positionOnPlatformInMeters: 31, unitType:  UnitType.DefendingMeleeUnit, unitLevelData: new UnitLevelData(1)),
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Top, positionOnPlatformInMeters: 32, unitType:  UnitType.DefendingMeleeUnit, unitLevelData: new UnitLevelData(1)),
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Top, positionOnPlatformInMeters: 33, unitType:  UnitType.DefendingMeleeUnit, unitLevelData: new UnitLevelData(2)),
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Top, positionOnPlatformInMeters: 34, unitType:  UnitType.DefendingMeleeUnit, unitLevelData: new UnitLevelData(2)),
            },
            lootSpawnData: new List<LootItemSpawnData> {
                new LootItemSpawnData(platformType: Constants.Platforms.PlatformType.Top, positionOnPlatformInMeters: 2, lootItemType: LootItemType.Ammunition, lootItemAmount: 3),
                new LootItemSpawnData(platformType: Constants.Platforms.PlatformType.Bottom, positionOnPlatformInMeters: 3, lootItemType: LootItemType.Ammunition, lootItemAmount: 3),
                new LootItemSpawnData(platformType: Constants.Platforms.PlatformType.Top, positionOnPlatformInMeters: 18, lootItemType: LootItemType.Ammunition, lootItemAmount: 3),
                new LootItemSpawnData(platformType: Constants.Platforms.PlatformType.Bottom, positionOnPlatformInMeters: 19, lootItemType: LootItemType.Ammunition, lootItemAmount: 3),
                new LootItemSpawnData(platformType: Constants.Platforms.PlatformType.Top, positionOnPlatformInMeters: 20, lootItemType: LootItemType.Ammunition, lootItemAmount: 3),
                new LootItemSpawnData(platformType: Constants.Platforms.PlatformType.Bottom, positionOnPlatformInMeters: 21, lootItemType: LootItemType.Ammunition, lootItemAmount: 3),
                new LootItemSpawnData(platformType: Constants.Platforms.PlatformType.Top, positionOnPlatformInMeters: 17, lootItemType: LootItemType.Gold, lootItemAmount: 40),
                new LootItemSpawnData(platformType: Constants.Platforms.PlatformType.Bottom, positionOnPlatformInMeters: 17, lootItemType: LootItemType.Gold, lootItemAmount: 40),
                new LootItemSpawnData(platformType: Constants.Platforms.PlatformType.Top, positionOnPlatformInMeters: 35, lootItemType: LootItemType.Gold, lootItemAmount: 40),
                new LootItemSpawnData(platformType: Constants.Platforms.PlatformType.Bottom, positionOnPlatformInMeters: 35, lootItemType: LootItemType.Gold, lootItemAmount: 40),
            }
        ),
        new LevelRunData (
            levelNumber: 6,
            lengthInMeters: 50,
            defendingTeamUnitSpawnData: new List<UnitSpawnData> {
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Bottom, positionOnPlatformInMeters: 10, unitType:  UnitType.DefendingArcherUnit, unitLevelData: new UnitLevelData(1)),
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Top, positionOnPlatformInMeters: 10, unitType:  UnitType.DefendingArcherUnit, unitLevelData: new UnitLevelData(1)),
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Bottom, positionOnPlatformInMeters: 20, unitType:  UnitType.DefendingArcherUnit, unitLevelData: new UnitLevelData(1)),
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Top, positionOnPlatformInMeters: 20, unitType:  UnitType.DefendingArcherUnit, unitLevelData: new UnitLevelData(1)),
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Bottom, positionOnPlatformInMeters: 25, unitType:  UnitType.DefendingArcherUnit, unitLevelData: new UnitLevelData(1)),
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Top, positionOnPlatformInMeters: 30, unitType:  UnitType.DefendingArcherUnit, unitLevelData: new UnitLevelData(1)),
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Top, positionOnPlatformInMeters: 35, unitType:  UnitType.DefendingArcherUnit, unitLevelData: new UnitLevelData(1)),
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Bottom, positionOnPlatformInMeters: 35, unitType:  UnitType.DefendingArcherUnit, unitLevelData: new UnitLevelData(1)),
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Top, positionOnPlatformInMeters: 40, unitType:  UnitType.DefendingArcherUnit, unitLevelData: new UnitLevelData(1)),
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Bottom, positionOnPlatformInMeters: 45, unitType:  UnitType.DefendingArcherUnit, unitLevelData: new UnitLevelData(1)),
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Top, positionOnPlatformInMeters: 50, unitType:  UnitType.DefendingArcherUnit, unitLevelData: new UnitLevelData(1)),
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Bottom, positionOnPlatformInMeters: 50, unitType:  UnitType.DefendingArcherUnit, unitLevelData: new UnitLevelData(1)),
            },
            lootSpawnData: new List<LootItemSpawnData> {
                new LootItemSpawnData(platformType: Constants.Platforms.PlatformType.Top, positionOnPlatformInMeters: 27, lootItemType: LootItemType.Gold, lootItemAmount: 100),
            }
        ),
        new LevelRunData (
            levelNumber: 7,
            lengthInMeters: 55,
            defendingTeamUnitSpawnData: new List<UnitSpawnData> {
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Top, positionOnPlatformInMeters: 10, unitType:  UnitType.DefendingArcherUnit, unitLevelData: new UnitLevelData(1)),
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Top, positionOnPlatformInMeters: 11, unitType:  UnitType.DefendingArcherUnit, unitLevelData: new UnitLevelData(1)),
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Top, positionOnPlatformInMeters: 12, unitType:  UnitType.DefendingArcherUnit, unitLevelData: new UnitLevelData(1)),
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Top, positionOnPlatformInMeters: 13, unitType:  UnitType.DefendingArcherUnit, unitLevelData: new UnitLevelData(1)),
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Bottom, positionOnPlatformInMeters: 20, unitType:  UnitType.DefendingArcherUnit, unitLevelData: new UnitLevelData(1)),
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Bottom, positionOnPlatformInMeters: 21, unitType:  UnitType.DefendingArcherUnit, unitLevelData: new UnitLevelData(1)),
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Bottom, positionOnPlatformInMeters: 22, unitType:  UnitType.DefendingArcherUnit, unitLevelData: new UnitLevelData(1)),
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Bottom, positionOnPlatformInMeters: 23, unitType:  UnitType.DefendingArcherUnit, unitLevelData: new UnitLevelData(1)),
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Top, positionOnPlatformInMeters: 28, unitType:  UnitType.DefendingMeleeUnit, unitLevelData: new UnitLevelData(1)),
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Bottom, positionOnPlatformInMeters: 28, unitType:  UnitType.DefendingMeleeUnit, unitLevelData: new UnitLevelData(1)),
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Top, positionOnPlatformInMeters: 35, unitType:  UnitType.DefendingMeleeUnit, unitLevelData: new UnitLevelData(2)),
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Bottom, positionOnPlatformInMeters: 40, unitType:  UnitType.DefendingMeleeUnit, unitLevelData: new UnitLevelData(2)),
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Top, positionOnPlatformInMeters: 45, unitType:  UnitType.DefendingMeleeUnit, unitLevelData: new UnitLevelData(2)),
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Bottom, positionOnPlatformInMeters: 50, unitType:  UnitType.DefendingMeleeUnit, unitLevelData: new UnitLevelData(2)),
            },
            lootSpawnData: new List<LootItemSpawnData> {
                new LootItemSpawnData(platformType: Constants.Platforms.PlatformType.Top, positionOnPlatformInMeters: 7, lootItemType: LootItemType.Gold, lootItemAmount: 50),
                new LootItemSpawnData(platformType: Constants.Platforms.PlatformType.Top, positionOnPlatformInMeters: 8, lootItemType: LootItemType.Gold, lootItemAmount: 50),
                new LootItemSpawnData(platformType: Constants.Platforms.PlatformType.Top, positionOnPlatformInMeters: 9, lootItemType: LootItemType.Gold, lootItemAmount: 50),
                new LootItemSpawnData(platformType: Constants.Platforms.PlatformType.Top, positionOnPlatformInMeters: 17, lootItemType: LootItemType.Gold, lootItemAmount: 50),
                new LootItemSpawnData(platformType: Constants.Platforms.PlatformType.Top, positionOnPlatformInMeters: 18, lootItemType: LootItemType.Gold, lootItemAmount: 50),
                new LootItemSpawnData(platformType: Constants.Platforms.PlatformType.Top, positionOnPlatformInMeters: 19, lootItemType: LootItemType.Gold, lootItemAmount: 50),
            }
        ),
        new LevelRunData (
            levelNumber: 8,
            lengthInMeters: 60,
            defendingTeamUnitSpawnData: new List<UnitSpawnData> {
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Top, positionOnPlatformInMeters: 12, unitType:  UnitType.DefendingMeleeUnit, unitLevelData: new UnitLevelData(2)),
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Top, positionOnPlatformInMeters: 13, unitType:  UnitType.DefendingArcherUnit, unitLevelData: new UnitLevelData(1)),
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Bottom, positionOnPlatformInMeters: 20, unitType:  UnitType.DefendingMeleeUnit, unitLevelData: new UnitLevelData(2)),
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Bottom, positionOnPlatformInMeters: 21, unitType:  UnitType.DefendingArcherUnit, unitLevelData: new UnitLevelData(1)),
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Top, positionOnPlatformInMeters: 30, unitType:  UnitType.DefendingMeleeUnit, unitLevelData: new UnitLevelData(2)),
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Top, positionOnPlatformInMeters: 31, unitType:  UnitType.DefendingArcherUnit, unitLevelData: new UnitLevelData(1)),
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Bottom, positionOnPlatformInMeters: 40, unitType:  UnitType.DefendingMeleeUnit, unitLevelData: new UnitLevelData(2)),
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Bottom, positionOnPlatformInMeters: 41, unitType:  UnitType.DefendingArcherUnit, unitLevelData: new UnitLevelData(1)),
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Top, positionOnPlatformInMeters: 50, unitType:  UnitType.DefendingMeleeUnit, unitLevelData: new UnitLevelData(2)),
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Top, positionOnPlatformInMeters: 51, unitType:  UnitType.DefendingArcherUnit, unitLevelData: new UnitLevelData(1)),
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Top, positionOnPlatformInMeters: 55, unitType:  UnitType.DefendingMeleeUnit, unitLevelData: new UnitLevelData(2)),
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Bottom, positionOnPlatformInMeters: 55, unitType:  UnitType.DefendingMeleeUnit, unitLevelData: new UnitLevelData(2)),
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Top, positionOnPlatformInMeters: 56, unitType:  UnitType.DefendingMeleeUnit, unitLevelData: new UnitLevelData(2)),
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Bottom, positionOnPlatformInMeters: 56, unitType:  UnitType.DefendingMeleeUnit, unitLevelData: new UnitLevelData(2)),
            },
            lootSpawnData: new List<LootItemSpawnData> {
                new LootItemSpawnData(platformType: Constants.Platforms.PlatformType.Top, positionOnPlatformInMeters: 11, lootItemType: LootItemType.Gold, lootItemAmount: 55),
                new LootItemSpawnData(platformType: Constants.Platforms.PlatformType.Bottom, positionOnPlatformInMeters: 19, lootItemType: LootItemType.Gold, lootItemAmount: 55),
                new LootItemSpawnData(platformType: Constants.Platforms.PlatformType.Top, positionOnPlatformInMeters: 29, lootItemType: LootItemType.Gold, lootItemAmount: 55),
                new LootItemSpawnData(platformType: Constants.Platforms.PlatformType.Bottom, positionOnPlatformInMeters: 39, lootItemType: LootItemType.Gold, lootItemAmount: 55),
                new LootItemSpawnData(platformType: Constants.Platforms.PlatformType.Top, positionOnPlatformInMeters: 49, lootItemType: LootItemType.Gold, lootItemAmount: 55),
                new LootItemSpawnData(platformType: Constants.Platforms.PlatformType.Top, positionOnPlatformInMeters: 28, lootItemType: LootItemType.Ammunition, lootItemAmount: 4),
                new LootItemSpawnData(platformType: Constants.Platforms.PlatformType.Top, positionOnPlatformInMeters: 48, lootItemType: LootItemType.Gold, lootItemAmount: 4),
            }
        ),
        new LevelRunData (
            levelNumber: 9,
            lengthInMeters: 70,
            defendingTeamUnitSpawnData: new List<UnitSpawnData> {
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Top, positionOnPlatformInMeters: 10, unitType:  UnitType.DefendingMeleeUnit, unitLevelData: new UnitLevelData(3)),
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Bottom, positionOnPlatformInMeters: 10, unitType:  UnitType.DefendingMeleeUnit, unitLevelData: new UnitLevelData(3)),
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Top, positionOnPlatformInMeters: 20, unitType:  UnitType.DefendingMeleeUnit, unitLevelData: new UnitLevelData(3)),
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Bottom, positionOnPlatformInMeters: 21, unitType:  UnitType.DefendingMeleeUnit, unitLevelData: new UnitLevelData(3)),
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Bottom, positionOnPlatformInMeters: 30, unitType:  UnitType.DefendingArcherUnit, unitLevelData: new UnitLevelData(2)),
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Bottom, positionOnPlatformInMeters: 31, unitType:  UnitType.DefendingArcherUnit, unitLevelData: new UnitLevelData(2)),
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Bottom, positionOnPlatformInMeters: 32, unitType:  UnitType.DefendingArcherUnit, unitLevelData: new UnitLevelData(2)),
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Top, positionOnPlatformInMeters: 32, unitType:  UnitType.DefendingArcherUnit, unitLevelData: new UnitLevelData(2)),
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Bottom, positionOnPlatformInMeters: 40, unitType:  UnitType.DefendingMeleeUnit, unitLevelData: new UnitLevelData(2)),
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Top, positionOnPlatformInMeters: 41, unitType:  UnitType.DefendingMeleeUnit, unitLevelData: new UnitLevelData(2)),
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Bottom, positionOnPlatformInMeters: 50, unitType:  UnitType.DefendingMeleeUnit, unitLevelData: new UnitLevelData(2)),
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Top, positionOnPlatformInMeters: 51, unitType:  UnitType.DefendingMeleeUnit, unitLevelData: new UnitLevelData(2)),
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Top, positionOnPlatformInMeters: 63, unitType:  UnitType.DefendingArcherUnit, unitLevelData: new UnitLevelData(2)),
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Bottom, positionOnPlatformInMeters: 63, unitType:  UnitType.DefendingArcherUnit, unitLevelData: new UnitLevelData(2)),
            },
            lootSpawnData: new List<LootItemSpawnData> {
                new LootItemSpawnData(platformType: Constants.Platforms.PlatformType.Bottom, positionOnPlatformInMeters: 29, lootItemType: LootItemType.Gold, lootItemAmount: 80),
                new LootItemSpawnData(platformType: Constants.Platforms.PlatformType.Top, positionOnPlatformInMeters: 30, lootItemType: LootItemType.Gold, lootItemAmount: 80),
            }
        ),
        new LevelRunData (
            levelNumber: 10,
            lengthInMeters: 100,
            defendingTeamUnitSpawnData: new List<UnitSpawnData> {
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Top, positionOnPlatformInMeters: 7, unitType:  UnitType.DefendingMeleeUnit, unitLevelData: new UnitLevelData(3)),
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Bottom, positionOnPlatformInMeters: 9, unitType:  UnitType.DefendingMeleeUnit, unitLevelData: new UnitLevelData(3)),
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Top, positionOnPlatformInMeters: 9, unitType:  UnitType.DefendingMeleeUnit, unitLevelData: new UnitLevelData(3)),
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Top, positionOnPlatformInMeters: 11, unitType:  UnitType.DefendingArcherUnit, unitLevelData: new UnitLevelData(2)),
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Bottom, positionOnPlatformInMeters: 17, unitType:  UnitType.DefendingMeleeUnit, unitLevelData: new UnitLevelData(3)),
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Bottom, positionOnPlatformInMeters: 18, unitType:  UnitType.DefendingArcherUnit, unitLevelData: new UnitLevelData(2)),
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Top, positionOnPlatformInMeters: 27, unitType:  UnitType.DefendingMeleeUnit, unitLevelData: new UnitLevelData(4)),
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Top, positionOnPlatformInMeters: 29, unitType:  UnitType.DefendingMeleeUnit, unitLevelData: new UnitLevelData(4)),
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Top, positionOnPlatformInMeters: 31, unitType:  UnitType.DefendingMeleeUnit, unitLevelData: new UnitLevelData(3)),
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Top, positionOnPlatformInMeters: 33, unitType:  UnitType.DefendingMeleeUnit, unitLevelData: new UnitLevelData(4)),
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Top, positionOnPlatformInMeters: 35, unitType:  UnitType.DefendingMeleeUnit, unitLevelData: new UnitLevelData(4)),
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Bottom, positionOnPlatformInMeters: 29, unitType:  UnitType.DefendingMeleeUnit, unitLevelData: new UnitLevelData(4)),
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Bottom, positionOnPlatformInMeters: 31, unitType:  UnitType.DefendingArcherUnit, unitLevelData: new UnitLevelData(3)),
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Bottom, positionOnPlatformInMeters: 40, unitType:  UnitType.DefendingMeleeUnit, unitLevelData: new UnitLevelData(4)),
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Bottom, positionOnPlatformInMeters: 42, unitType:  UnitType.DefendingMeleeUnit, unitLevelData: new UnitLevelData(3)),
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Bottom, positionOnPlatformInMeters: 44, unitType:  UnitType.DefendingArcherUnit, unitLevelData: new UnitLevelData(3)),
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Top, positionOnPlatformInMeters: 50, unitType:  UnitType.DefendingMeleeUnit, unitLevelData: new UnitLevelData(4)),
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Top, positionOnPlatformInMeters: 52, unitType:  UnitType.DefendingMeleeUnit, unitLevelData: new UnitLevelData(3)),
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Bottom, positionOnPlatformInMeters: 52, unitType:  UnitType.DefendingMeleeUnit, unitLevelData: new UnitLevelData(3)),
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Top, positionOnPlatformInMeters: 54, unitType:  UnitType.DefendingArcherUnit, unitLevelData: new UnitLevelData(3)),
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Bottom, positionOnPlatformInMeters: 60, unitType:  UnitType.DefendingMeleeUnit, unitLevelData: new UnitLevelData(4)),
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Bottom, positionOnPlatformInMeters: 62, unitType:  UnitType.DefendingMeleeUnit, unitLevelData: new UnitLevelData(3)),
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Bottom, positionOnPlatformInMeters: 64, unitType:  UnitType.DefendingArcherUnit, unitLevelData: new UnitLevelData(3)),
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Top, positionOnPlatformInMeters: 70, unitType:  UnitType.DefendingMeleeUnit, unitLevelData: new UnitLevelData(4)),
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Top, positionOnPlatformInMeters: 72, unitType:  UnitType.DefendingMeleeUnit, unitLevelData: new UnitLevelData(3)),
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Top, positionOnPlatformInMeters: 74, unitType:  UnitType.DefendingArcherUnit, unitLevelData: new UnitLevelData(3)),
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Bottom, positionOnPlatformInMeters: 82, unitType:  UnitType.DefendingArcherUnit, unitLevelData: new UnitLevelData(3)),
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Bottom, positionOnPlatformInMeters: 83, unitType:  UnitType.DefendingArcherUnit, unitLevelData: new UnitLevelData(3)),
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Bottom, positionOnPlatformInMeters: 84, unitType:  UnitType.DefendingArcherUnit, unitLevelData: new UnitLevelData(3)),
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Top, positionOnPlatformInMeters: 90, unitType:  UnitType.DefendingArcherUnit, unitLevelData: new UnitLevelData(3)),
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Top, positionOnPlatformInMeters: 91, unitType:  UnitType.DefendingArcherUnit, unitLevelData: new UnitLevelData(3)),
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Top, positionOnPlatformInMeters: 92, unitType:  UnitType.DefendingArcherUnit, unitLevelData: new UnitLevelData(3)),
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Top, positionOnPlatformInMeters: 96, unitType:  UnitType.DefendingArcherUnit, unitLevelData: new UnitLevelData(4)),
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Bottom, positionOnPlatformInMeters: 96, unitType:  UnitType.DefendingArcherUnit, unitLevelData: new UnitLevelData(4)),
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Top, positionOnPlatformInMeters: 98, unitType:  UnitType.DefendingArcherUnit, unitLevelData: new UnitLevelData(4)),
                new UnitSpawnData(platformType: Constants.Platforms.PlatformType.Bottom, positionOnPlatformInMeters: 98, unitType:  UnitType.DefendingArcherUnit, unitLevelData: new UnitLevelData(4)),

            },
            lootSpawnData: new List<LootItemSpawnData> {
                new LootItemSpawnData(platformType: Constants.Platforms.PlatformType.Top, positionOnPlatformInMeters: 32, lootItemType: LootItemType.Ammunition, lootItemAmount: 15),
                //new LootItemSpawnData(platformType: Constants.Platforms.PlatformType.Top, positionOnPlatformInMeters: 30, lootItemType: LootItemType.Gold, lootItemAmount: 80),
            }
        ),
    };
}

