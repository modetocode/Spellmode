using UnityEngine;

/// <summary>
/// Component that is responsible for destroying obsolete game object that run past the screen.
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class DestroyerComponent : MonoBehaviour {

    [SerializeField]
    private bool canDestroyUnitComponents = true;

    void OnTriggerEnter2D(Collider2D other) {
        BulletComponent bulletComponent = other.gameObject.GetComponent<BulletComponent>();
        if (bulletComponent != null) {
            bulletComponent.Bullet.Destroy();
        }

        if (!canDestroyUnitComponents) {
            return;
        }

        UnitComponent unitComponent = other.gameObject.GetComponent<UnitComponent>();
        if (unitComponent != null) {
            unitComponent.Destroy();
        }

        LootItemComponent lootItemComponent = other.gameObject.GetComponent<LootItemComponent>();
        if (lootItemComponent != null) {
            lootItemComponent.Destroy();
        }
    }
}