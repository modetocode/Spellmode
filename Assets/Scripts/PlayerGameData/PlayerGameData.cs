using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerGameData {

    [field: NonSerialized]
    public event Action ObjectUpdated;

    [SerializeField]
    private string username = string.Empty;
    [SerializeField]
    private int goldAmount = 70;
    [SerializeField]
    private int highestCompletedLevelNumber = 0;
    [SerializeField]
    private List<PlayerHeroData> heroData = new List<PlayerHeroData> { new PlayerHeroData(UnitType.HeroUnit, new UnitLevelData(1)) };
    [SerializeField]
    private bool firstLevelRunTutorialCompleted = false;
    [SerializeField]
    private bool shopIntroTutorialCompleted = false;

    private IDictionary<UnitType, PlayerHeroData> heroTypeToDataMap;

    private IDictionary<UnitType, PlayerHeroData> HeroTypeToDataMap {
        get {
            if (this.heroTypeToDataMap == null) {
                this.heroTypeToDataMap = new Dictionary<UnitType, PlayerHeroData>();
                for (int i = 0; i < heroData.Count; i++) {
                    PlayerHeroData currentHeroData = heroData[i];
                    this.heroTypeToDataMap[currentHeroData.HeroType] = currentHeroData;
                }
            }

            return this.heroTypeToDataMap;
        }
    }

    public PlayerGameData(string username) {
        if (username.Equals(string.Empty)) {
            throw new ArgumentOutOfRangeException("username", "cannot be empty string.");
        }

        this.username = username;
    }

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

    public string Username {
        get { return this.username; }
    }

    public bool FirstLevelRunTutorialCompleted {
        get { return this.firstLevelRunTutorialCompleted; }
        set {
            this.firstLevelRunTutorialCompleted = value;
            this.InvokeObjectUpdatedEvent();
        }
    }

    public bool ShopIntroTutorialCompleted {
        get { return this.shopIntroTutorialCompleted; }
        set {
            this.shopIntroTutorialCompleted = value;
            this.InvokeObjectUpdatedEvent();
        }
    }

    public PlayerHeroData GetHeroData(UnitType heroType) {
        if (!this.HeroTypeToDataMap.ContainsKey(heroType)) {
            throw new InvalidOperationException("The player has no data for the given hero type.");
        }

        return this.HeroTypeToDataMap[heroType];
    }

    public void UpdateHeroData(PlayerHeroData updatedHeroData) {
        if (updatedHeroData == null) {
            throw new ArgumentNullException("updatedHeroData");
        }

        if (!this.HeroTypeToDataMap.ContainsKey(updatedHeroData.HeroType)) {
            throw new InvalidOperationException("The player has no data for the given hero type.");
        }

        this.heroData.Remove(this.HeroTypeToDataMap[updatedHeroData.HeroType]);
        this.HeroTypeToDataMap[updatedHeroData.HeroType] = updatedHeroData;
        this.heroData.Add(updatedHeroData);
        this.InvokeObjectUpdatedEvent();
    }

    private void InvokeObjectUpdatedEvent() {
        if (this.ObjectUpdated != null) {
            this.ObjectUpdated();
        }
    }
}