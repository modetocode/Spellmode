using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

/// <summary>
/// Responsible for saving and loading the game data of the players.
/// </summary>
public static class GameDataManager {

    public static GameData LoadGameData() {
        GameData gameData;
        string gameDataPath = GetGameDataPath();
        if (File.Exists(gameDataPath)) {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream file = File.Open(gameDataPath, FileMode.Open);
            gameData = (GameData)binaryFormatter.Deserialize(file);
            file.Close();
        }
        else {
            gameData = new GameData();
            SaveGameData(gameData);
        }

        return gameData;
    }

    public static void SaveGameData(GameData gameData) {
        if (gameData == null) {
            throw new ArgumentNullException("gameData");
        }

        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream file = File.Create(GetGameDataPath());
        binaryFormatter.Serialize(file, gameData);
        file.Close();
    }

    public static PlayerGameData LoadPlayerGameData(string username) {
        GameData gameData = LoadGameData();
        PlayerGameData playerGameData;
        if (gameData.ContainsPlayerGameData(username)) {
            playerGameData = gameData.GetPlayerGameData(username);
        }
        else {
            playerGameData = new PlayerGameData(username);
            gameData.AddPlayerGameData(playerGameData);
            SaveGameData(gameData);
        }

        return playerGameData;
    }

    public static void SavePlayerGameData(PlayerGameData playerGameData) {
        if (playerGameData == null) {
            throw new ArgumentNullException("playerGameData");
        }

        GameData gameData = LoadGameData();
        gameData.AddPlayerGameData(playerGameData);
        SaveGameData(gameData);
    }

    private static string GetGameDataPath() {
        return Path.Combine(Application.persistentDataPath, Constants.GameData.GameDataFileName);
    }
}
