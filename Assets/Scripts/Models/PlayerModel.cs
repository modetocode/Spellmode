/// <summary>
/// Stores the data of one player.
/// </summary>
public class PlayerModel : BaseModel<PlayerModel> {

    public PlayerGameData PlayerGameData { get; private set; }

    public void Initialize(string username) {
        this.PlayerGameData = GameDataManager.LoadPlayerGameData(username);
        this.PlayerGameData.ObjectUpdated += SavePlayerData;
    }

    private void SavePlayerData() {
        GameDataManager.SavePlayerGameData(this.PlayerGameData);
    }

    public override void Clear() {
        this.PlayerGameData.ObjectUpdated -= SavePlayerData;
        this.PlayerGameData = null;
    }
}