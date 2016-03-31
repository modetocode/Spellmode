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
    private BulletComponent arrowTemplate;

    private void OnBulletAddedHandler(Bullet newBullet) {
        BulletComponent instantiatedComponent = (BulletComponent)Instantiate(arrowTemplate, newBullet.CurrentPosition, Quaternion.identity);
        instantiatedComponent.Initialize(newBullet);
    }

    public void InstantiateUnit(Unit newUnit) {
        if (newUnit == null) {
            throw new ArgumentNullException("newUnit");
        }

        //TODO instantiate the proper game object
        //TODO object pool?
        UnitComponent instantiatedComponent = (UnitComponent)Instantiate(heroUnitTemplate, newUnit.PositionInMeters, Quaternion.identity);
        instantiatedComponent.Initialize(newUnit);
        if (this.HeroUnitInstantiated != null) {
            this.HeroUnitInstantiated(instantiatedComponent);
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
}

