using System;
using UnityEngine;

/// <summary>
/// Stores the data for one hero that one player has.
/// </summary>
[Serializable]
public class PlayerHeroData {
    [SerializeField]
    private UnitType heroType;
    [SerializeField]
    private UnitLevelData heroLevelData;

    public UnitType HeroType { get { return this.heroType; } }
    public UnitLevelData HeroLevelData { get { return this.heroLevelData; } }

    public PlayerHeroData(UnitType heroType, UnitLevelData heroLevelData) {
        if (heroLevelData == null) {
            throw new ArgumentNullException("heroLevelData");
        }

        this.heroType = heroType;
        this.heroLevelData = heroLevelData;
    }
}
