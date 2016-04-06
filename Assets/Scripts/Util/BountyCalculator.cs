using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Responsible for calculation of the rewards acquired via various ways (killing a unit)
/// </summary>
public static class BountyCalculator {
    public static IList<LootItem> GetLootForUnit(Unit unit) {
        if (unit == null) {
            throw new ArgumentNullException("unit");
        }

        IList<LootItem> loot = new List<LootItem>();
        IList<LootItemProgressionData> lootProgression = GameMechanicsManager.GetLootTableProgressionData().GetLootProgressionData(unit.UnitType);
        for (int i = 0; i < lootProgression.Count; i++) {
            LootItem lootItem = CreateLootItem(lootProgression[i], unit.Level, unit.PositionInMeters);
            loot.Add(lootItem);
        }

        return loot;
    }

    private static LootItem CreateLootItem(LootItemProgressionData progressionData, int level, Vector2 spawnPosition) {
        float lootAmount = progressionData.AmountPerLevel * level;
        float minValueFactorRatio = 1f - progressionData.RandomFactorRatio;
        float maxValueFactorRatio = 1f + progressionData.RandomFactorRatio;
        float variableAmount = UnityEngine.Random.Range(lootAmount * minValueFactorRatio, lootAmount * maxValueFactorRatio);
        LootItem lootItem = new LootItem(lootItemType: progressionData.LootItemType, amount: (int)variableAmount, spawnPosition: spawnPosition);
        return lootItem;
    }
}