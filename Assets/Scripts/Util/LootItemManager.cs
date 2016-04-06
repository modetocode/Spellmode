using System;
using System.Collections.Generic;

/// <summary>
/// Responsible for storing data about the loot in a level. Keeps also a list of collected loot
/// </summary>
public class LootItemManager {

    public event Action<LootItem> LootItemAdded;

    private IList<LootItem> lootItems;
    private IList<LootItem> collectedLootItems;

    public LootItemManager() {
        this.lootItems = new List<LootItem>();
        this.collectedLootItems = new List<LootItem>();
    }

    public void AddLootItem(LootItem lootItem) {
        if (lootItem == null) {
            throw new ArgumentNullException("lootItem");
        }

        this.lootItems.Add(lootItem);
        lootItem.Collected += AddToCollectedItems;

        if (this.LootItemAdded != null) {
            this.LootItemAdded(lootItem);
        }
    }

    private void AddToCollectedItems(LootItem lootItem) {
        lootItem.Collected -= AddToCollectedItems;
        this.collectedLootItems.Add(lootItem);
    }
}
