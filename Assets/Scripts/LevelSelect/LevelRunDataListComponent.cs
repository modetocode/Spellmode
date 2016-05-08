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

    private IList<LevelRunDataListItemComponent> listItemComponents;

    public void Awake() {
        if (this.listItemPrefab == null) {
            throw new NullReferenceException("listItemPrefab is null");
        }

        if (this.containter == null) {
            throw new NullReferenceException("containter is null");
        }
    }

    public void Initialize(int numberOfLevels, int highestUnlockedLevelNumber, Action<int> onListItemClickedAction) {
        if (numberOfLevels < 1) {
            throw new ArgumentOutOfRangeException("numberOfLevels", "Cannot be less than one.");
        }

        if (highestUnlockedLevelNumber > numberOfLevels) {
            throw new ArgumentOutOfRangeException("highestUnlockedLevelNumber", "Cannot be more than the number of levels.");
        }

        if (onListItemClickedAction == null) {
            throw new ArgumentNullException("onListItemClickedAction");
        }

        this.listItemComponents = new List<LevelRunDataListItemComponent>();
        for (int i = 0; i < numberOfLevels; i++) {
            LevelRunDataListItemComponent instantiatedComponent = Instantiate(this.listItemPrefab);
            instantiatedComponent.transform.SetParent(containter.gameObject.transform);
            instantiatedComponent.transform.localScale = Vector3.one;
            instantiatedComponent.Initialize(
                levelNumber: i + 1,
                isLocked: i >= highestUnlockedLevelNumber,
                onListItemClickedAction: onListItemClickedAction);
            this.listItemComponents.Add(instantiatedComponent);
        }

        containter.ResizeComponent();
    }

    public LevelRunDataListItemComponent GetListItemComponent(int index) {
        if (index < 0 || index >= this.listItemComponents.Count) {
            throw new ArgumentOutOfRangeException("index");
        }

        return this.listItemComponents[index];
    }
}
