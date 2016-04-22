using System;
using UnityEngine;

/// <summary>
/// Responsible for instantiating all of the objects on the scene that will be added on runtime
/// </summary>
public class InstantiatorComponent : MonoBehaviour {

    /// <summary>
    /// Event that is thrown when the main hero unit is instantiated in the scene
    /// </summary>
    public event Action<UnitComponent> HeroUnitInstantiated;

    [SerializeField]
    private UnitComponent heroUnitTemplate;

    [SerializeField]
    private UnitComponent defendingArcherUnitTemplate;

    [SerializeField]
    private UnitComponent defendingMeleeUnitTemplate;

    [SerializeField]
    private BulletComponent arrowTemplate;

    [SerializeField]
    private LootItemComponent goldLootItemComponentTemplate;

    [SerializeField]
    private LootItemComponent ammunitionLootItemComponentTemplate;

    private void OnBulletAddedHandler(Bullet newBullet) {
        BulletComponent instantiatedComponent = (BulletComponent)Instantiate(arrowTemplate, newBullet.CurrentPosition, Quaternion.identity);
        instantiatedComponent.Initialize(newBullet);
    }

    public void InstantiateUnit(Unit newUnit) {
        if (newUnit == null) {
            throw new ArgumentNullException("newUnit");
        }

        //TODO object pool?
        UnitComponent unitTemplate;
        switch (newUnit.UnitType) {
            case UnitType.HeroUnit:
                unitTemplate = this.heroUnitTemplate;
                break;
            case UnitType.DefendingArcherUnit:
                unitTemplate = this.defendingArcherUnitTemplate;
                break;
            case UnitType.DefendingMeleeUnit:
                unitTemplate = this.defendingMeleeUnitTemplate;
                break;
            default:
                throw new InvalidOperationException("Unit type not supported");
        }

        UnitComponent instantiatedComponent = (UnitComponent)Instantiate(unitTemplate, newUnit.PositionInMeters, Quaternion.identity);
        instantiatedComponent.Initialize(newUnit);
        if (newUnit.UnitType == UnitType.HeroUnit) {
            if (this.HeroUnitInstantiated != null) {
                this.HeroUnitInstantiated(instantiatedComponent);
            }
        }
    }

    public void InstantiateBullet(Bullet newBullet) {
        if (newBullet == null) {
            throw new ArgumentNullException("newBullet");
        }

        //TODO object pool?
        BulletComponent instantiatedComponent = (BulletComponent)Instantiate(arrowTemplate, newBullet.CurrentPosition, Quaternion.identity);
        instantiatedComponent.Initialize(newBullet);
    }

    public void InstantiateLootItem(LootItem newLootItem) {
        if (newLootItem == null) {
            throw new ArgumentNullException("newLootItem");
        }

        LootItemComponent lootItemTemplate;
        switch (newLootItem.Type) {
            case LootItemType.Gold:
                lootItemTemplate = this.goldLootItemComponentTemplate;
                break;
            case LootItemType.Ammunition:
                lootItemTemplate = this.ammunitionLootItemComponentTemplate;
                break;
            default:
                throw new InvalidOperationException("Loot item type not supported");
        }

        LootItemComponent instantiatedComponent = (LootItemComponent)Instantiate(lootItemTemplate, newLootItem.SpawnPosition, Quaternion.identity);
        instantiatedComponent.Initialize(newLootItem);
    }
}