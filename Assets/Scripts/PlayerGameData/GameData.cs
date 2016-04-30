using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameData {
    [SerializeField]
    private string lastLogonUsername = string.Empty;
    [SerializeField]
    private List<PlayerGameData> playersGameData = new List<PlayerGameData>();

    private IDictionary<string, PlayerGameData> usernameToPlayerGameDataMap;

    private IDictionary<string, PlayerGameData> UsernameToPlayerGameDataMap {
        get {
            if (this.usernameToPlayerGameDataMap == null) {
                this.usernameToPlayerGameDataMap = new Dictionary<string, PlayerGameData>();
                for (int i = 0; i < this.playersGameData.Count; i++) {
                    this.usernameToPlayerGameDataMap[this.playersGameData[i].Username] = this.playersGameData[i];
                }
            }

            return this.usernameToPlayerGameDataMap;
        }
    }

    public bool ContainsPlayerGameData(string username) {
        return this.GetPlayerGameData(username) != null;
    }

    public PlayerGameData GetPlayerGameData(string username) {
        if (this.UsernameToPlayerGameDataMap.ContainsKey(username)) {
            return this.UsernameToPlayerGameDataMap[username];
        }

        return null;
    }

    public void AddPlayerGameData(PlayerGameData playerGameData) {
        if (playerGameData == null) {
            throw new ArgumentNullException("playerGameData");
        }

        string username = playerGameData.Username;
        if (this.UsernameToPlayerGameDataMap.ContainsKey(username)) {
            PlayerGameData oldGameData = this.UsernameToPlayerGameDataMap[username];
            this.playersGameData.Remove(oldGameData);
        }

        this.UsernameToPlayerGameDataMap[username] = playerGameData;
        this.playersGameData.Add(playerGameData);
    }

    public string LastLogonUsername {
        get { return this.lastLogonUsername; }
        set { this.lastLogonUsername = value; }
    }
}
