using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData {

    // Adapted from: https://github.com/UnityTechnologies/UniteNow20-Persistent-Data
    // https://unity.com/blog/games/persistent-data-how-to-save-your-game-states-and-settings
    
    [System.Serializable]
    public struct PlayerData {
        public string uuid;
        public int health;
        public int syringes;
        public int bloodBombs;
    }
    
    public int m_Score;
    // Will save data for the enemies that are alive.
    // public List<EnemyData> m_EnemyData = new List<EnemyData>();
    
    public PlayerData playerData = PlayerData
    
    public string ToJson() {
        return JsonUtility.ToJson(this);
    }

    public void LoadFromJson(string a_Json) {
        JsonUtility.FromJsonOverwrite(a_Json, this);
    }
}

public interface ISaveable {
    void PopulateSaveData(SaveData a_SaveData);
    void LoadFromSaveData(SaveData a_SaveData);
}