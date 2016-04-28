using System;
using UnityEngine;

[Serializable]
public class PlayerGameData {

    [field: NonSerialized]
    public event Action ObjectUpdated;

    [SerializeField]
    private int goldAmount = 0;
    [SerializeField]
    private int highestCompletedLevelNumber = 0;
    [SerializeField]
    private UnitLevelData heroUnitLevelData = new UnitLevelData(1);
    [SerializeField]
    private UnitType heroUnitType = UnitType.HeroUnit;

    public int GoldAmount {
        get { return this.goldAmount; }
        set {
            this.goldAmount = value;
            this.InvokeObjectUpdatedEvent();
        }
    }

    public int HighestCompletedLevelNumber {
        get { return this.highestCompletedLevelNumber; }
        set {
            this.highestCompletedLevelNumber = value;
            this.InvokeObjectUpdatedEvent();
        }
    }

    public UnitLevelData HeroUnitLevelData {
        get { return this.heroUnitLevelData; }
        set {
            this.heroUnitLevelData = value;
            this.InvokeObjectUpdatedEvent();
        }
    }

    public UnitType HeroUnitType {
        get { return this.heroUnitType; }
    }

    private void InvokeObjectUpdatedEvent() {
        if (this.ObjectUpdated != null) {
            this.ObjectUpdated();
        }
    }
}