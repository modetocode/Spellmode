using System;
using System.Collections.Generic;

/// <summary>
/// Responsible for storing the data that are needed for all the levels in the game.
/// </summary>
public static class LevelsDataManager {

    private static GameConstants GameConstants { get { return GameMechanicsManager.GetGameConstantsData(); } }

    public static LevelRunData GetLevelData(int levelNumber) {
        List<LevelRunData> levelsData = GameConstants.GetGameLevelsData();
        if (levelNumber < 1 || levelNumber > GetNumberOfLevels()) {
            throw new ArgumentOutOfRangeException("levelNumber", "Cannot be less than one or more than the defined levels");
        }

        return levelsData[levelNumber - 1];
    }

    public static int GetNumberOfLevels() {
        return GameConstants.GetGameLevelsData().Count;
    }
}

