using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

/// <summary>
/// Responsible for saving and loading the game data of the players.
/// </summary>
public static class PlayerGameDataManager {
    public static PlayerGameData LoadGameData() {
        PlayerGameData playerGameData;
        if (File.Exists(GetPlayersGameDataPath())) {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream file = File.Open(GetPlayersGameDataPath(), FileMode.Open);
            playerGameData = (PlayerGameData)binaryFormatter.Deserialize(file);
            file.Close();
        }
        else {
            playerGameData = new PlayerGameData();
            SaveGameData(playerGameData);
        }

        return playerGameData;
    }

    public static void SaveGameData(PlayerGameData playerGameData) {
        if (playerGameData == null) {
            throw new ArgumentNullException("playerGameData");
        }

        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream file = File.Create(GetPlayersGameDataPath());
        binaryFormatter.Serialize(file, playerGameData);
        file.Close();
    }

    private static string GetPlayersGameDataPath() {
        return Path.Combine(Application.persistentDataPath, Constants.GameData.PlayersGameDataFileName);
    }
}
