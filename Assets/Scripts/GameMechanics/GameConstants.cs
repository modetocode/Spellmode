using System;
using UnityEngine;

/// <summary>
/// Containts varius tweakable values connected with the game.
/// </summary>
public class GameConstants : ScriptableObject {
    /// <summary>
    /// The amount of gold that will be multiplied by the level number if the player successfully passes the level.
    /// </summary>
    [SerializeField]
    private int goldIncreaseAmountForLevelCompleted;
    [SerializeField]
    private int goldIncreaseCostForHeroStatUpdate;

    public int GetGoldRewardForLevel(int levelNumber) {
        if (levelNumber < 1) {
            throw new ArgumentOutOfRangeException("levelNumber");
        }

        return levelNumber * goldIncreaseAmountForLevelCompleted;
    }

    public int GetGoldCostForHeroStat(int statLevel) {
        if (statLevel < 1) {
            throw new ArgumentOutOfRangeException("statLevel");
        }

        return statLevel * goldIncreaseCostForHeroStatUpdate;
    }
}
