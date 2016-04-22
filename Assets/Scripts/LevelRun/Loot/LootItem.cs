using System;
using UnityEngine;

/// <summary>
/// Represents one loot item.
/// </summary>
public class LootItem {

    /// <summary>
    /// Thrown when the loot item is collected.
    /// </summary>
    public event Action<LootItem> Collected;

    public LootItemType Type { get; private set; }
    public int Amount { get; private set; }
    public Vector2 SpawnPosition { get; private set; }
    public bool IsCollected { get; private set; }

    public LootItem(LootItemType lootItemType, int amount, Vector2 spawnPosition) {
        if (amount <= 0) {
            throw new ArgumentOutOfRangeException("amout", "Cannot be zero or less.");
        }

        this.Type = lootItemType;
        this.Amount = amount;
        this.SpawnPosition = spawnPosition;
        this.IsCollected = false;
    }

    /// <summary>
    /// Marks the item as collected.
    /// </summary>
    public void MarkAsCollected() {
        if (this.IsCollected) {
            throw new InvalidOperationException("The item is already collected");
        }

        if (this.Collected != null) {
            this.Collected(this);
        }
    }
}
