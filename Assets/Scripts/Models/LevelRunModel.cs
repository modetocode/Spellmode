using System;

/// <summary>
/// Stores information for one level run that the player is currently on.
/// </summary>
public class LevelRunModel : BaseModel<LevelRunModel> {

    /// <summary>
    /// Event that is thrown when the run is finished
    /// </summary>
    public event Action<LevelRunFinishType> RunFinished;

    public LevelRunData LevelRunData { get; private set; }
    public UnitSpawnData HeroSpawnData { get; private set; }
    public int LevelNumber { get; private set; }
    public bool ShowRunTutorial { get; private set; }
    public Unit HeroUnit { get; set; }
    public ProgressTracker ProgressTracker { get; set; }
    public LootItemManager LootItemManager { get; set; }
    public LevelCompetedRewardData LevelCompletedRewardData { get; set; }

    public void Initialize(LevelRunData levelRunData, UnitSpawnData heroSpawnData, int levelNumber, bool showRunTutorial) {
        if (levelRunData == null) {
            throw new ArgumentNullException("levelRunData");
        }

        if (heroSpawnData == null) {
            throw new ArgumentNullException("heroSpawnData");
        }

        if (levelNumber < 1) {
            throw new ArgumentOutOfRangeException("levelNumber", "Cannot be less than one.");
        }

        this.LevelRunData = levelRunData;
        this.HeroSpawnData = heroSpawnData;
        this.LevelNumber = levelNumber;
        this.ShowRunTutorial = showRunTutorial;
    }

    public void FireRunFinishedEvent(LevelRunFinishType finishType) {
        if (this.RunFinished != null) {
            this.RunFinished(finishType);
        }
    }

    public override void Clear() {
        this.LevelRunData = null;
        this.HeroSpawnData = null;
        this.HeroUnit = null;
        this.ProgressTracker = null;
        this.LootItemManager = null;
        this.LevelCompletedRewardData = null;
    }
}
