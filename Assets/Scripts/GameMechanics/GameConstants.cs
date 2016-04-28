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
        //TODO arg check
        return levelNumber * goldIncreaseAmountForLevelCompleted;
    }

    public int GetGoldCostForHeroStat(int statLevel) {
        //TODO arg check
        return statLevel * goldIncreaseCostForHeroStatUpdate;
    }
}
