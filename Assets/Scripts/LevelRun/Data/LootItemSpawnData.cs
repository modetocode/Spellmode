using System;

/// <summary>
/// Contains data for spawning of one loot item.
/// </summary>
public class LootItemSpawnData : SpawnData {

    public LootItemType LootItemType { get; private set; }
    public int LootItemAmount { get; private set; }

    public LootItemSpawnData(Constants.Platforms.PlatformType platformType, float positionOnPlatformInMeters, LootItemType lootItemType, int lootItemAmount)
    : base(platformType, positionOnPlatformInMeters) {
        if (lootItemAmount <= 0) {
            throw new ArgumentOutOfRangeException("lootItemAmount", "Cannot be zero or less.");
        }

        this.LootItemType = lootItemType;
        this.LootItemAmount = lootItemAmount;
    }
}
