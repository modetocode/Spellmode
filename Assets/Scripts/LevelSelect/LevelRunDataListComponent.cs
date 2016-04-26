using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// List Controller for the level run data list containing all the info for the levels in the game.
/// </summary>
public class LevelRunDataListComponent : MonoBehaviour {

    [SerializeField]
    private LevelRunDataListItemComponent listItemPrefab;
    [SerializeField]
    private GridResizerComponent containter;

    private IList<LevelRunData> listItemData;
    private int highestUnlockedLevelNumber;

    public void Awake() {
        if (this.listItemPrefab == null) {
            throw new NullReferenceException("listItemPrefab is null");
        }

        if (this.containter == null) {
            throw new NullReferenceException("containter is null");
        }
    }

    public void Initialize(IList<LevelRunData> listItemData, int highestUnlockedLevelNumber, Action<LevelRunData> onListItemClickedAction) {
        if (listItemData == null) {
            throw new ArgumentNullException("listItemData");
        }

        if (highestUnlockedLevelNumber < 1) {
            throw new ArgumentOutOfRangeException("highestUnlockedLevelNumber", "Cannot be less than one.");
        }

        if (onListItemClickedAction == null) {
            throw new ArgumentNullException("onListItemClickedAction");
        }

        this.listItemData = listItemData;
        this.highestUnlockedLevelNumber = highestUnlockedLevelNumber;
        for (int i = 0; i < this.listItemData.Count; i++) {
            LevelRunDataListItemComponent instantiatedComponent = Instantiate(this.listItemPrefab);
            instantiatedComponent.transform.SetParent(containter.gameObject.transform);
            instantiatedComponent.transform.localScale = Vector3.one;
            instantiatedComponent.Initialize(
                levelRunData: this.listItemData[i],
                isLocked: i >= this.highestUnlockedLevelNumber,
                onListItemClickedAction: onListItemClickedAction);
        }

        containter.ResizeComponent();
    }
}
