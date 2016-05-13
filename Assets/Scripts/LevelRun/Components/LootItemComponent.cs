using System;
using UnityEngine;

/// <summary>
/// Component that represents visual object of one loot item.
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class LootItemComponent : MonoBehaviour {

    public LootItem LootItem { get; private set; }
    public Action<LootItemComponent> OnDestroyAction { get; private set; }

    public void Initialize(LootItem lootItem, Action<LootItemComponent> onDestroyAction) {
        if (lootItem == null) {
            throw new ArgumentNullException("lootItem");
        }

        if (onDestroyAction == null) {
            throw new ArgumentNullException("onDestroyAction");
        }

        this.LootItem = lootItem;
        this.OnDestroyAction = onDestroyAction;
        this.transform.position = lootItem.SpawnPosition;
    }

    public void OnTriggerEnter2D(Collider2D other) {
        if (this.LootItem == null) {
            return;
        }

        UnitComponent unitComponent = other.gameObject.GetComponent<UnitComponent>();
        if (unitComponent == null) {
            return;
        }

        //TODO should we check if the loot item can be collected for hardcoded unit type?
        if (unitComponent.Unit.UnitType == UnitType.HeroUnit) {
            this.LootItem.MarkAsCollected();
            this.Destroy();
        }
    }

    public void Destroy() {
        this.OnDestroyAction(this);
        this.LootItem = null;
        this.OnDestroyAction = null;
    }
}