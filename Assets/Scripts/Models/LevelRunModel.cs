using System;

/// <summary>
/// Stores information for one level run that the player is currently on.
/// </summary>
public class LevelRunModel : BaseModel<LevelRunModel> {

    public LevelRunData LevelRunData { get; private set; }

    public void Initialize(LevelRunData levelRunData) {
        if (levelRunData == null) {
            throw new ArgumentNullException("levelRunData");
        }

        this.LevelRunData = levelRunData;
    }

    public override void Clear() {
        this.LevelRunData = null;
    }
}
