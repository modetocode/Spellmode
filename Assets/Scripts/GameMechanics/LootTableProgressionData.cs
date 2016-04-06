using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents loot gained by unit type defined as progression data.
/// </summary>
public class LootTableProgressionData : ScriptableObject {
    [Serializable]
    private class LootItemPerUnitData {
        [SerializeField]
        public UnitType UnitType;

        [SerializeField]
        public List<LootItemProgressionData> LootProgressionData;

    }

    [SerializeField]
    private List<LootItemPerUnitData> lootTable;

    private IDictionary<UnitType, List<LootItemProgressionData>> unitTypeToLootMap;

    public IList<LootItemProgressionData> GetLootProgressionData(UnitType unitType) {
        if (this.unitTypeToLootMap == null) {
            this.unitTypeToLootMap = new Dictionary<UnitType, List<LootItemProgressionData>>();
            for (int i = 0; i < lootTable.Count; i++) {
                if (this.unitTypeToLootMap.ContainsKey(lootTable[i].UnitType)) {
                    throw new InvalidOperationException("The loot table contains multiple entries for one unit type");
                }

                this.unitTypeToLootMap[lootTable[i].UnitType] = lootTable[i].LootProgressionData;
            }
        }

        if (!this.unitTypeToLootMap.ContainsKey(unitType)) {
            return new List<LootItemProgressionData>();
        }

        return this.unitTypeToLootMap[unitType];
    }

}