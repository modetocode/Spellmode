using System;

/// <summary>
/// Stores data for the rewards that the players gets when he completes a level.
/// </summary>
public class LevelCompetedRewardData {
    public int LevelCompletedGoldRewardAmount { get; private set; }
    public int GoldLootedAmount { get; private set; }
    public int PlayerTotalGold { get; private set; }

    public LevelCompetedRewardData(int levelCompletedGoldRewardAmount, int goldLootedAmount, int playersTotalGold) {
        if (levelCompletedGoldRewardAmount < 0) {
            throw new ArgumentOutOfRangeException("levelCompletedGoldRewardAmount", "Cannot be less than zero.");
        }

        if (goldLootedAmount < 0) {
            throw new ArgumentOutOfRangeException("goldLootedAmount", "Cannot be less than zero.");
        }

        if (playersTotalGold < 0) {
            throw new ArgumentOutOfRangeException("playersTotalGold", "Cannot be less than zero.");
        }

        this.LevelCompletedGoldRewardAmount = levelCompletedGoldRewardAmount;
        this.GoldLootedAmount = goldLootedAmount;
        this.PlayerTotalGold = playersTotalGold;
    }
}
