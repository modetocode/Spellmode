/// <summary>
/// Stores the data of one player.
/// </summary>
public class PlayerModel : BaseModel<PlayerModel> {

    public PlayerGameData PlayerGameData { get; private set; }

    public void Initialize() {
        this.PlayerGameData = PlayerGameDataManager.LoadGameData();
        this.PlayerGameData.ObjectUpdated += SavePlayerData;
        this.PlayerGameData.HeroUnitLevelData.ObjectUpdated += SavePlayerData;
    }

    private void SavePlayerData() {
        PlayerGameDataManager.SaveGameData(this.PlayerGameData);
    }

    public override void Clear() {
        this.PlayerGameData = null;
        this.PlayerGameData.ObjectUpdated -= SavePlayerData;
        this.PlayerGameData.HeroUnitLevelData.ObjectUpdated -= SavePlayerData;
    }
}