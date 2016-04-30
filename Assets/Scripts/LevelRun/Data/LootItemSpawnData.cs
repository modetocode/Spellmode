using System;
using UnityEngine;

/// <summary>
/// Contains data for spawning of one loot item.
/// </summary>
[Serializable]
public class LootItemSpawnData : SpawnData {

    [SerializeField]
    private LootItemType lootItemType;
    [SerializeField]
    private int lootItemAmount;

    public LootItemType LootItemType { get { return this.lootItemType; } }
    public int LootItemAmount { get { return this.lootItemAmount; } }

    public LootItemSpawnData(Constants.Platforms.PlatformType platformType, float positionOnPlatformInMeters, LootItemType lootItemType, int lootItemAmount)
    : base(platformType, positionOnPlatformInMeters) {
        if (lootItemAmount <= 0) {
            throw new ArgumentOutOfRangeException("lootItemAmount", "Cannot be zero or less.");
        }

        this.lootItemType = lootItemType;
        this.lootItemAmount = lootItemAmount;
    }
}
