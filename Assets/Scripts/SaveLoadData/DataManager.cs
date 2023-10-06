using System.IO;
using UnityEngine;
using System;
using  System.Collections.Generic;

namespace DefaultNamespace.SaveLoadData
{
    public class DataManager : MonoBehaviour
    {
        private string savePath;
        private GameData gameData;
        
        public static DataManager Instance;
        
        private void Awake()
        {
            Instance = this;
            savePath = Application.persistentDataPath + "/gameData.json";
            LoadGameData();
        }

        public void LoadGameData()
        {
            if (File.Exists(savePath))
            {
                try
                {
                    string json = File.ReadAllText(savePath);
                    gameData = JsonUtility.FromJson<GameData>(json);
                    gameData.CharacterState.SetCharacterData(gameData.CharacterStateData);
                    gameData.Inventory.SetInventoryDate(gameData.InventoryData);
                }
                catch (Exception e)
                {
                    Debug.LogError("Error loading game data: " + e.Message);
                    gameData = CreateDefaultGameData();
                }
            }
            else
            {
                gameData = CreateDefaultGameData();
            }
        }

        public void SaveGameData()
        {
            try
            {
                gameData.CharacterStateData = gameData.CharacterState.GetCharacterData();
                gameData.InventoryData = gameData.Inventory.GetInventoryDate();
                string json = JsonUtility.ToJson(gameData);
                File.WriteAllText(savePath, json);
            }
            catch (Exception e)
            {
                Debug.LogError("Error saving game data: " + e.Message);
            }
        }

        private GameData CreateDefaultGameData()
        {
            return new GameData
            {
                CharacterState = FindObjectOfType<CharacterState>(),
                Inventory = FindObjectOfType<Inventory>(true),
            };
        }

        private void OnApplicationQuit()
        {
            SaveGameData();
        }
        
        private bool isApplicationPaused = false;

        private void OnApplicationPause(bool pauseStatus)
        {
            if (pauseStatus && !isApplicationPaused)
            {
                SaveGameData();
                isApplicationPaused = true;
            }
            else if (!pauseStatus && isApplicationPaused)
            {
                isApplicationPaused = false;
            }
        }
    }
}

[Serializable]
public class GameData
{
    public CharacterState CharacterState;
    public CharacterStateData CharacterStateData;
    public InventoryData InventoryData;
    public Inventory Inventory;
}

[Serializable]
public class CharacterStateData
{
    public int CurrentHealth;
    public int MaxHealth;
    public int Damage;
    public int Armor;
    public int AmmoCount;
}

[Serializable]
public class InventoryData
{
    public List<ItemInventory> ItemInventory;
}