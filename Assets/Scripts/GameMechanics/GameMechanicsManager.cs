using System;
using System.Collections.Generic;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public static class GameMechanicsManager {

    private static IDictionary<string, object> instances = new Dictionary<string, object>();

    private static T GetInstance<T>(string relativePath) where T : ScriptableObject {
        object instance;
        if (!instances.TryGetValue(relativePath, out instance) || instance == null) {
            instance = Resources.Load(Constants.GameMechanics.GameMechanicsRelativePath + relativePath) as T;
            if (instance == null) {
                instance = CreateNewDefault<T>(relativePath);
                instances.Add(relativePath, instance);
            }
            else {
                instances.Add(relativePath, instance);
            }
        }
        return instance as T;
    }

    private static T CreateNewDefault<T>(string relativePath) where T : ScriptableObject {
        T instance = ScriptableObject.CreateInstance<T>();
#if UNITY_EDITOR
        string resourcesPath = Path.Combine(Constants.GameMechanics.ResourcesPath, Constants.GameMechanics.GameMechanicsRelativePath);
        string assetPath = string.Concat(relativePath, Constants.GameMechanics.AssetsExtension);
        string fullPath = Path.Combine(resourcesPath, assetPath);
        AssetDatabase.CreateAsset(instance as ScriptableObject, fullPath);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
#endif
        return instance as T;
    }


    public static UnitProgressionData GetUnitProgressionData(UnitType unitType) {
        if (!Constants.GameMechanics.UnitTypeToProgressionAssetNameMap.ContainsKey(unitType)) {
            throw new InvalidOperationException("Fetching the unit progression data asset for the unit type is not implemented");
        }

        string progressionAssetName = Constants.GameMechanics.UnitTypeToProgressionAssetNameMap[unitType];

        UnitProgressionData unitProgressionData = GetInstance<UnitProgressionData>(progressionAssetName);
        if (unitProgressionData.UnitType != unitType) {
            throw new InvalidOperationException("Invalid unit type for fetched unitProgressionData");
        }

        return unitProgressionData;
    }

    public static LootTableProgressionData GetLootTableProgressionData() {
        return GetInstance<LootTableProgressionData>(Constants.GameMechanics.LootTableAssetName);
    }

    public static GameConstants GetGameConstanstsData() {
        return GetInstance<GameConstants>(Constants.GameMechanics.GameConstantsAssetName);
    }

#if UNITY_EDITOR
    [MenuItem(Constants.GameMechanics.GameMechanicsMenuName + "/" + Constants.GameMechanics.UnitsMenuName + "/" + Constants.GameMechanics.HeroUnitMenuName)]
    private static void DisplayHeroUnitProgressionData() {
        Selection.activeObject = GetUnitProgressionData(UnitType.HeroUnit);
    }

    [MenuItem(Constants.GameMechanics.GameMechanicsMenuName + "/" + Constants.GameMechanics.UnitsMenuName + "/" + Constants.GameMechanics.DefendingArcherUnitMenuName)]
    private static void DisplayDefendingArcherUnitProgressionData() {
        Selection.activeObject = GetUnitProgressionData(UnitType.DefendingArcherUnit);
    }

    [MenuItem(Constants.GameMechanics.GameMechanicsMenuName + "/" + Constants.GameMechanics.UnitsMenuName + "/" + Constants.GameMechanics.DefendingMeleeUnitMenuName)]
    private static void DisplayDefendingMeleeUnitProgressionData() {
        Selection.activeObject = GetUnitProgressionData(UnitType.DefendingMeleeUnit);
    }

    [MenuItem(Constants.GameMechanics.GameMechanicsMenuName + "/" + Constants.GameMechanics.LootTableMenuName)]
    private static void DisplayLootTableData() {
        Selection.activeObject = GetLootTableProgressionData();
    }

    [MenuItem(Constants.GameMechanics.GameMechanicsMenuName + "/" + Constants.GameMechanics.GameConstantsMenuName)]
    private static void DisplayGameConstantsData() {
        Selection.activeObject = GetGameConstanstsData();
    }
#endif
}