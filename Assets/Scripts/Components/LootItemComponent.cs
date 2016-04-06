using System;
using UnityEngine;

/// <summary>
/// Component that represents visual object of one loot item.
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class LootItemComponent : MonoBehaviour {

    public LootItem LootItem { get; private set; }

    public void Initialize(LootItem lootItem) {
        if (lootItem == null) {
            throw new ArgumentNullException("lootItem");
        }

        this.LootItem = lootItem;
    }

    void OnTriggerEnter2D(Collider2D other) {
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
            Destroy(this.gameObject);
        }
    }

    internal void Destroy() {
        Destroy(this.gameObject);
    }
}