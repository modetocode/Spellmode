using System;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

/// <summary>
/// Responsible for displaying and editing of data for one level run.
/// </summary>
[CustomEditor(typeof(LevelRunData), true)]
public class LevelRunDataEditor : Editor {

    private ReorderableList defendingUnitsSpawnDataList;
    private ReorderableList lootSpawnDataList;

    private ReorderableList DefendingUnitsSpawnDataList {
        get {
            if (this.defendingUnitsSpawnDataList == null) {
                this.defendingUnitsSpawnDataList = new ReorderableList(
                    this.serializedObject,
                    this.serializedObject.FindProperty("defendingTeamUnitSpawnData"),
                    draggable: true,
                    displayHeader: true,
                    displayAddButton: true,
                    displayRemoveButton: true);

                this.defendingUnitsSpawnDataList.drawElementCallback = HandleDrawDefendingUnitListElement;
                this.defendingUnitsSpawnDataList.drawHeaderCallback = (Rect rect) => EditorGUI.LabelField(rect, "Defending units");
                this.defendingUnitsSpawnDataList.onAddCallback = HandleDefendingUnitAddedToList;
            }

            return this.defendingUnitsSpawnDataList;
        }
    }

    private ReorderableList LootSpawnDataList {
        get {
            if (this.lootSpawnDataList == null) {
                this.lootSpawnDataList = new ReorderableList(
                    this.serializedObject,
                    this.serializedObject.FindProperty("lootSpawnData"),
                    draggable: true,
                    displayHeader: true,
                    displayAddButton: true,
                    displayRemoveButton: true);

                this.lootSpawnDataList.drawElementCallback = HandleDrawLootItemListElement;
                this.lootSpawnDataList.drawHeaderCallback = (Rect rect) => EditorGUI.LabelField(rect, "Loot items");
            }

            return this.lootSpawnDataList;
        }
    }

    public override void OnInspectorGUI() {
        LevelRunData levelRunData = (LevelRunData)target;
        float newLevelLength = EditorGUILayout.FloatField("Length (in meters)", levelRunData.LengthInMeters);
        if (!newLevelLength.Equals(levelRunData.LengthInMeters)) {
            levelRunData.LengthInMeters = newLevelLength;
            EditorUtility.SetDirty(levelRunData);
        }

        this.serializedObject.Update();
        this.DefendingUnitsSpawnDataList.DoLayoutList();
        this.LootSpawnDataList.DoLayoutList();
        this.serializedObject.ApplyModifiedProperties();
    }

    private void HandleDrawDefendingUnitListElement(Rect rect, int index, bool isActive, bool isFocused) {
        SerializedProperty elementToDraw = this.DefendingUnitsSpawnDataList.serializedProperty.GetArrayElementAtIndex(index);
        this.HandleDrawSpawnDataListElement(elementToDraw, rect);
        EditorGUI.LabelField(new Rect(rect.x + 130, rect.y, 30, EditorGUIUtility.singleLineHeight), "type");
        SerializedProperty unitTypeProperty = elementToDraw.FindPropertyRelative("unitType");
        UnitType unitType = (UnitType)Enum.GetValues(typeof(UnitType)).GetValue(unitTypeProperty.enumValueIndex);
        UnitType newUnitType = (UnitType)EditorGUI.EnumPopup(new Rect(rect.x + 160, rect.y, 130, EditorGUIUtility.singleLineHeight), unitType);
        if (newUnitType != unitType) {
            unitTypeProperty.enumValueIndex = (int)newUnitType;
        }

        SerializedProperty unitLevelDataProperty = elementToDraw.FindPropertyRelative("unitLevelData");
        SerializedProperty healthUpgradeProperty = unitLevelDataProperty.FindPropertyRelative("healthUpgradeLevel");
        int currentLevel = healthUpgradeProperty.intValue;
        EditorGUI.LabelField(new Rect(rect.x + 300, rect.y, 30, EditorGUIUtility.singleLineHeight), "lvl");
        int newLevel = EditorGUI.IntField(new Rect(rect.x + 330, rect.y, 30, EditorGUIUtility.singleLineHeight), currentLevel);
        if (newLevel != currentLevel) {
            this.SetLevelData(unitLevelDataProperty, newLevel);
        }
    }

    private void HandleDefendingUnitAddedToList(ReorderableList list) {
        int index = list.serializedProperty.arraySize;
        list.serializedProperty.arraySize++;
        list.index = index;

        SerializedProperty addedElement = list.serializedProperty.GetArrayElementAtIndex(index);
        SerializedProperty unitLevelDataProperty = addedElement.FindPropertyRelative("unitLevelData");
        SerializedProperty healthUpgradeProperty = unitLevelDataProperty.FindPropertyRelative("healthUpgradeLevel");
        int level = healthUpgradeProperty.intValue;
        if (level < 1) {
            this.SetLevelData(unitLevelDataProperty, 1);
        }

        SerializedProperty autoAttackProperty = addedElement.FindPropertyRelative("unitHasAutoAttack");
        autoAttackProperty.boolValue = true;
    }

    private void SetLevelData(SerializedProperty unitLevelDataProperty, int level) {
        SerializedProperty healthUpgradeProperty = unitLevelDataProperty.FindPropertyRelative("healthUpgradeLevel");
        SerializedProperty damageUpgradeProperty = unitLevelDataProperty.FindPropertyRelative("damageUpgradeLevel");
        SerializedProperty ammunitionUpgradeProperty = unitLevelDataProperty.FindPropertyRelative("ammunitionUpgradeLevel");
        healthUpgradeProperty.intValue = level;
        damageUpgradeProperty.intValue = level;
        ammunitionUpgradeProperty.intValue = level;
    }

    private void HandleDrawLootItemListElement(Rect rect, int index, bool isActive, bool isFocused) {
        SerializedProperty elementToDraw = this.LootSpawnDataList.serializedProperty.GetArrayElementAtIndex(index);
        this.HandleDrawSpawnDataListElement(elementToDraw, rect);
        EditorGUI.LabelField(new Rect(rect.x + 130, rect.y, 30, EditorGUIUtility.singleLineHeight), "type");
        SerializedProperty lootTypeProperty = elementToDraw.FindPropertyRelative("lootItemType");
        LootItemType lootItemType = (LootItemType)Enum.GetValues(typeof(LootItemType)).GetValue(lootTypeProperty.enumValueIndex);
        LootItemType newLootItemType = (LootItemType)EditorGUI.EnumPopup(new Rect(rect.x + 160, rect.y, 130, EditorGUIUtility.singleLineHeight), lootItemType);
        if (newLootItemType != lootItemType) {
            lootTypeProperty.enumValueIndex = (int)newLootItemType;
        }

        EditorGUI.LabelField(new Rect(rect.x + 290, rect.y, 50, EditorGUIUtility.singleLineHeight), "amount");
        EditorGUI.PropertyField(new Rect(rect.x + 340, rect.y, 40, EditorGUIUtility.singleLineHeight), elementToDraw.FindPropertyRelative("lootItemAmount"), GUIContent.none);
    }

    private void HandleDrawSpawnDataListElement(SerializedProperty elementToDraw, Rect rect) {
        EditorGUI.LabelField(new Rect(rect.x, rect.y, 30, EditorGUIUtility.singleLineHeight), "pos");
        EditorGUI.PropertyField(new Rect(rect.x + 30, rect.y, 40, EditorGUIUtility.singleLineHeight), elementToDraw.FindPropertyRelative("positionOnPlatformInMeters"), GUIContent.none);
        SerializedProperty platformTypeProperty = elementToDraw.FindPropertyRelative("platformType");
        Constants.Platforms.PlatformType platformType = (Constants.Platforms.PlatformType)Enum.GetValues(typeof(Constants.Platforms.PlatformType)).GetValue(platformTypeProperty.enumValueIndex);
        Constants.Platforms.PlatformType newPlatformType = (Constants.Platforms.PlatformType)EditorGUI.EnumPopup(new Rect(rect.x + 70, rect.y, 60, EditorGUIUtility.singleLineHeight), platformType);
        if (newPlatformType != platformType) {
            platformTypeProperty.enumValueIndex = (int)newPlatformType;
        }
    }
}
