using System;
using UnityEngine;

[Serializable]
public class PlayerGameData {

    [field: NonSerialized]
    public event Action ObjectUpdated;

    [SerializeField]
    private int goldAmount = 0;
    [SerializeField]
    private int highestUnlockedLevelNumber = 1;

    public int GoldAmount {
        get { return this.goldAmount; }
        set {
            this.goldAmount = value;
            this.InvokeObjectUpdatedEvent();
        }
    }

    public int HighestUnlockedLevelNumber {
        get { return this.highestUnlockedLevelNumber; }
        set {
            this.highestUnlockedLevelNumber = value;
            this.InvokeObjectUpdatedEvent();
        }
    }

    private void InvokeObjectUpdatedEvent() {
        if (this.ObjectUpdated != null) {
            this.ObjectUpdated();
        }
    }
}