using System;
using System.Collections.Generic;

/// <summary>
/// Responsible for storing data about the loot in a level. Keeps also a list of collected loot
/// </summary>
public class LootItemManager {

    public event Action<LootItem> LootItemAdded;
    public event Action<LootItem> LootItemCollected;

    private IList<LootItem> lootItems;
    private IList<LootItem> collectedLootItems;
    private IDictionary<LootItemType, int> collectedLootAmmountByTypeMap;

    public LootItemManager() {
        this.lootItems = new List<LootItem>();
        this.collectedLootItems = new List<LootItem>();
        this.collectedLootAmmountByTypeMap = new Dictionary<LootItemType, int>();
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
        int previousAmount = 0;
        if (this.collectedLootAmmountByTypeMap.ContainsKey(lootItem.Type)) {
            previousAmount = this.collectedLootAmmountByTypeMap[lootItem.Type];
        }

        this.collectedLootAmmountByTypeMap[lootItem.Type] = previousAmount + lootItem.Amount;

        if (this.LootItemCollected != null) {
            this.LootItemCollected(lootItem);
        }
    }

    public int GetCollectedLootAmountByType(LootItemType lootItemType) {
        if (!this.collectedLootAmmountByTypeMap.ContainsKey(lootItemType)) {
            return 0;
        }

        return this.collectedLootAmmountByTypeMap[lootItemType];
    }
}
