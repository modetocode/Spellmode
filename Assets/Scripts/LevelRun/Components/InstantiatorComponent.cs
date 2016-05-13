using System;
using System.Collections.Generic;
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


    private SimplePool<BulletComponent> bulletsPool;
    private ObjectPoolContainer unitsPoolContainer;
    private ObjectPoolContainer lootItemsPoolContainer;

    public void Awake() {
        this.Initialize();
    }

    private void Initialize() {
        this.bulletsPool = new SimplePool<BulletComponent>(this.InstantiateBulletComponent, 0);
        this.unitsPoolContainer = this.CreateObjectPoolContainer<UnitComponent>(typeof(UnitType), this.GetUnitTypeToComponentMap(), 0);
        this.lootItemsPoolContainer = this.CreateObjectPoolContainer<LootItemComponent>(typeof(LootItemType), this.GetLootItemTypeToComponentMap(), 0);
    }

    public void InstantiateUnit(Unit newUnit) {
        if (newUnit == null) {
            throw new ArgumentNullException("newUnit");
        }

        GameObject instantiatedComponent = this.unitsPoolContainer.FetchObject(newUnit.UnitType.ToString());
        UnitComponent unitComponent = instantiatedComponent.GetComponent<UnitComponent>();
        unitComponent.Initialize(newUnit, this.OnUnitComponentDestroyed);
        if (newUnit.UnitType == UnitType.HeroUnit) {
            if (this.HeroUnitInstantiated != null) {
                this.HeroUnitInstantiated(unitComponent);
            }
        }
    }

    public void InstantiateBullet(Bullet newBullet) {
        if (newBullet == null) {
            throw new ArgumentNullException("newBullet");
        }

        BulletComponent newBulletComponent = this.bulletsPool.Fetch();
        newBulletComponent.Initialize(newBullet, OnBulletComponentDestroyed);
        newBulletComponent.gameObject.SetActive(true);
    }

    public void InstantiateLootItem(LootItem newLootItem) {
        if (newLootItem == null) {
            throw new ArgumentNullException("newLootItem");
        }

        GameObject instantiatedComponent = this.lootItemsPoolContainer.FetchObject(newLootItem.Type.ToString());
        LootItemComponent lootItemComponent = instantiatedComponent.GetComponent<LootItemComponent>();
        lootItemComponent.Initialize(newLootItem, this.OnLootItemComponentDestroyed);
    }

    private IDictionary<object, UnitComponent> GetUnitTypeToComponentMap() {
        IDictionary<object, UnitComponent> unitTypeToComponentMap = new Dictionary<object, UnitComponent> {
             { UnitType.HeroUnit, this.heroUnitTemplate},
             { UnitType.DefendingMeleeUnit, this.defendingMeleeUnitTemplate},
             { UnitType.DefendingArcherUnit, this.defendingArcherUnitTemplate},
        };

        return unitTypeToComponentMap;
    }

    private IDictionary<object, LootItemComponent> GetLootItemTypeToComponentMap() {
        IDictionary<object, LootItemComponent> lootItemTypeToComponentMap = new Dictionary<object, LootItemComponent> {
             { LootItemType.Gold, this.goldLootItemComponentTemplate},
             { LootItemType.Ammunition, this.ammunitionLootItemComponentTemplate},
        };

        return lootItemTypeToComponentMap;
    }

    private BulletComponent InstantiateBulletComponent() {
        return this.InstantiateObject<BulletComponent>(this.arrowTemplate);
    }

    private T InstantiateObject<T>(T objectTemplate) where T : MonoBehaviour {
        T instantiatedObject = Instantiate(objectTemplate);
        instantiatedObject.gameObject.SetActive(false);
        return instantiatedObject;
    }

    private void OnBulletComponentDestroyed(BulletComponent bulletComponent) {
        bulletComponent.gameObject.SetActive(false);
        this.bulletsPool.Release(bulletComponent);
    }

    private void OnUnitComponentDestroyed(UnitComponent unitComponent) {
        UnitType unitType = unitComponent.Unit.UnitType;
        this.ReturnObjectToPoolContainter(this.unitsPoolContainer, unitType.ToString(), unitComponent.gameObject);
    }

    private void OnLootItemComponentDestroyed(LootItemComponent lootItemComponent) {
        LootItemType lootItemType = lootItemComponent.LootItem.Type;
        this.ReturnObjectToPoolContainter(this.lootItemsPoolContainer, lootItemType.ToString(), lootItemComponent.gameObject);
    }

    private void ReturnObjectToPoolContainter(ObjectPoolContainer poolContainer, string poolName, GameObject gameObject) {
        gameObject.SetActive(false);
        poolContainer.ReleaseObject(gameObject, poolName);
    }

    private ObjectPoolContainer CreateObjectPoolContainer<T>(Type enumType, IDictionary<object, T> enumToObjectMap, int initialCapacity) where T : MonoBehaviour {
        ObjectPoolContainer poolContainer = new ObjectPoolContainer();
        foreach (var enumValue in Enum.GetValues(enumType)) {
            var name = enumValue.ToString();
            if (!enumToObjectMap.ContainsKey(enumValue)) {
                throw new InvalidOperationException("There is no game object template specified for type " + name);
            }

            T gameObjectTemplate = enumToObjectMap[enumValue];
            Func<GameObject> factoryFunction = () => { return this.InstantiateObject<T>(gameObjectTemplate).gameObject; };
            poolContainer.CreatePool(name, factoryFunction, initialCapacity);
        }

        return poolContainer;
    }
}