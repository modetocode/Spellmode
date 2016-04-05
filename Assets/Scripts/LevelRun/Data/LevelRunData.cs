using System;
using System.Collections.Generic;
/// <summary>
/// The data that is needed for level run
/// </summary>
public class LevelRunData {
    public int LevelNumber { get; private set; }
    public float LengthInMeters { get; private set; }
    public IList<UnitSpawnData> DefendingTeamUnitSpawnData { get; private set; }

    public LevelRunData(int levelNumber, float lengthInMeters, IList<UnitSpawnData> defendingTeamUnitSpawnData) {
        if (levelNumber <= 0f) {
            throw new ArgumentOutOfRangeException("levelNumber", "Cannot be zero or less.");
        }

        if (lengthInMeters <= 0f) {
            throw new ArgumentOutOfRangeException("lengthInMeters", "Cannot be zero or less.");
        }

        if (defendingTeamUnitSpawnData == null) {
            throw new ArgumentNullException("defendingTeamUnitSpawnData");
        }

        this.LevelNumber = levelNumber;
        this.LengthInMeters = lengthInMeters;
        this.DefendingTeamUnitSpawnData = defendingTeamUnitSpawnData;
    }
}
