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

    private Team attackingTeam;
    private Team defendingTeam;

    public void InitializeComponent(Team attackingTeam, Team defendingTeam) {
        //TODO arg check
        this.attackingTeam = attackingTeam;
        this.defendingTeam = defendingTeam;
        this.attackingTeam.UnitAdded += OnUnitAddedHandler;
        this.defendingTeam.UnitAdded += OnUnitAddedHandler;
        //TODO unsubscribe from these events
    }

    private void OnUnitAddedHandler(Unit newUnit) {
        //TODO instantiate the proper game object
        //TODO object pool?
        UnitComponent instantiatedComponent = (UnitComponent)Instantiate(heroUnitTemplate, newUnit.PositionInMeters, Quaternion.identity);
        instantiatedComponent.Initialize(newUnit);
        if (this.HeroUnitInstantiated != null) {
            this.HeroUnitInstantiated(instantiatedComponent);
        }
    }
}

