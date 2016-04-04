using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Responsible for spawning units. Decision is made based on the progression of the attacking team and when new units need to spawn.
/// </summary>
public class UnitSpawner : ITickable {

    private Team attackingTeam;
    private Team defendingTeam;
    private IList<UnitSpawnData> attackingTeamUnitSpawnData;
    private IList<UnitSpawnData> defendingTeamUnitSpawnData;
    private ProgressTracker progressTracker;
    private readonly float distanceToTriggerUnitSpawnInMetters;

    public UnitSpawner(ProgressTracker progressTracker, Team attackingTeam, Team defendingTeam, IList<UnitSpawnData> attackingTeamUnitSpawnData, IList<UnitSpawnData> defendingTeamUnitSpawnData) {
        if (progressTracker == null) {
            throw new ArgumentOutOfRangeException("progressTracker");
        }

        if (attackingTeam == null) {
            throw new ArgumentOutOfRangeException("attackingTeam");
        }

        if (defendingTeam == null) {
            throw new ArgumentOutOfRangeException("defendingTeam");
        }

        if (attackingTeamUnitSpawnData == null) {
            throw new ArgumentOutOfRangeException("attackingTeamUnitSpawnData");
        }

        if (defendingTeamUnitSpawnData == null) {
            throw new ArgumentOutOfRangeException("defendingTeamUnitSpawnData");
        }

        this.progressTracker = progressTracker;
        this.attackingTeam = attackingTeam;
        this.defendingTeam = defendingTeam;
        this.attackingTeamUnitSpawnData = attackingTeamUnitSpawnData.OrderBy(x => x.PositionOnPlatformInMeters).ToList();
        this.defendingTeamUnitSpawnData = defendingTeamUnitSpawnData.OrderBy(x => x.PositionOnPlatformInMeters).ToList();
        this.distanceToTriggerUnitSpawnInMetters = Constants.Units.DistanceToTriggerUnitSpawnInMetters;
    }

    public void Tick(float deltaTime) {
        this.SpawnAllVisibleUnits();
    }

    private void SpawnAllVisibleUnits() {
        this.SpawnAllVisibleUnitsForTeam(this.attackingTeam, this.attackingTeamUnitSpawnData);
        this.SpawnAllVisibleUnitsForTeam(this.defendingTeam, this.defendingTeamUnitSpawnData);
    }

    private void SpawnAllVisibleUnitsForTeam(Team team, IList<UnitSpawnData> teamUnitSpawnData) {
        float currentProgressPositionInMeters = this.progressTracker.CurrentProgressInMeters;
        float spawnablePosition = currentProgressPositionInMeters + distanceToTriggerUnitSpawnInMetters;
        for (int i = 0; i < teamUnitSpawnData.Count; i++) {
            if (teamUnitSpawnData[i].PositionOnPlatformInMeters <= spawnablePosition) {
                float unitSpawnYPosition = PlatformManager.GetYCoordinateForPlatform(teamUnitSpawnData[i].PlatformType);
                Vector2 unitSpawnPosition = new Vector2(teamUnitSpawnData[i].PositionOnPlatformInMeters, unitSpawnYPosition);
                Unit newUnit = UnitFactory.CreateUnit(teamUnitSpawnData[i].UnitType, teamUnitSpawnData[i].UnitLevel, unitSpawnPosition, teamUnitSpawnData[i].UnitHasAutoAttack);
                team.AddUnit(newUnit);
                teamUnitSpawnData.RemoveAt(i);
                i--;
            }
            else {
                break;
            }
        }
    }

    public void OnTickingFinished() {
    }
}