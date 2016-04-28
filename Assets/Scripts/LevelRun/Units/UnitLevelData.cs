using System;
using UnityEngine;

/// <summary>
/// The data that is stored for each hero that the player owns.
/// </summary>
[Serializable]
public class UnitLevelData {

    [field: NonSerialized]
    public event Action ObjectUpdated;

    [SerializeField]
    private int healthUpgradeLevel = 1;
    [SerializeField]
    private int damageUpgradeLevel = 1;
    [SerializeField]
    private int ammunitionUpgradeLevel = 1;

    public int HealthUpgradeLevel {
        get { return this.healthUpgradeLevel; }
        set {
            if (value < 1) {
                throw new ArgumentOutOfRangeException("HealthUpgradeLevel", "Cannot be less than one.");
            }

            this.healthUpgradeLevel = value;
            this.InvokeObjectUpdatedEvent();
        }
    }

    public int DamageUpgradeLevel {
        get { return this.damageUpgradeLevel; }
        set {
            if (value < 1) {
                throw new ArgumentOutOfRangeException("DamageUpgradeLevel", "Cannot be less than one.");
            }

            this.damageUpgradeLevel = value;
            this.InvokeObjectUpdatedEvent();
        }
    }

    public int AmmunitionUpgradeLevel {
        get { return this.ammunitionUpgradeLevel; }
        set {
            if (value < 1) {
                throw new ArgumentOutOfRangeException("AmmunitionUpgradeLevel", "Cannot be less than one.");
            }

            this.ammunitionUpgradeLevel = value;
            this.InvokeObjectUpdatedEvent();
        }
    }

    public UnitLevelData(int healthUpgradeLevel, int damageUpgradeLevel, int ammunitionUpgradeLevel) {
        if (healthUpgradeLevel < 1) {
            throw new ArgumentOutOfRangeException("healthUpgradeLevel", "Cannot be less than one.");
        }

        if (damageUpgradeLevel < 1) {
            throw new ArgumentOutOfRangeException("damageUpgradeLevel", "Cannot be less than one.");
        }

        if (ammunitionUpgradeLevel < 1) {
            throw new ArgumentOutOfRangeException("ammunitionUpgradeLevel", "Cannot be less than one.");
        }

        this.healthUpgradeLevel = healthUpgradeLevel;
        this.damageUpgradeLevel = damageUpgradeLevel;
        this.ammunitionUpgradeLevel = ammunitionUpgradeLevel;
    }

    public UnitLevelData(int levelForAllUpgrades)
            : this(healthUpgradeLevel: levelForAllUpgrades,
                  damageUpgradeLevel: levelForAllUpgrades,
                  ammunitionUpgradeLevel: levelForAllUpgrades) {
    }

    public int GetAverageLevel() {
        return (int)((this.healthUpgradeLevel + this.damageUpgradeLevel + this.ammunitionUpgradeLevel) / 3f);
    }

    private void InvokeObjectUpdatedEvent() {
        if (this.ObjectUpdated != null) {
            this.ObjectUpdated();
        }
    }
}
