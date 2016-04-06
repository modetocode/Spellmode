using System;
using UnityEngine;

/// <summary>
/// Represents progression data for one loot item.
/// </summary>
[Serializable]
public class LootItemProgressionData {
    [SerializeField]
    private LootItemType lootItemType;

    [SerializeField]
    private int amountPerLevel;

    [SerializeField]
    [Range(0f, 0.99f)]
    private float randomFactorRatio;

    public LootItemType LootItemType { get { return this.lootItemType; } }
    public int AmountPerLevel { get { return this.amountPerLevel; } }
    public float RandomFactorRatio { get { return this.randomFactorRatio; } }
}
