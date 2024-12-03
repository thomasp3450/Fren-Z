using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System.IO;

public class SelectScene : MonoBehaviour {

    private ProgressData progressData;

    void Start() {
        progressData = ProgressData.Instance;
    }
 
    public void startLevel1() {
        SceneManager.LoadSceneAsync("Level 1");
    }

    public void startLevel2() {
        SceneManager.LoadSceneAsync("Level 2");
    }

    public void openMainMenu() {
        SceneManager.LoadSceneAsync("MainMenu");
    }

    public void resetProgress() {
        progressData.SetProgressData(1, 0, 0);
        SaveData();
    }

    public void enterLevel() {
        if (progressData.level == 1) SceneManager.LoadSceneAsync("Level 1");
        if (progressData.level == 2) SceneManager.LoadSceneAsync("Level 2");
        if (progressData.level == 3) SceneManager.LoadSceneAsync("Level 3");
    }

    public void SaveData() {
        string json = JsonUtility.ToJson(progressData);
        Debug.Log(json);

        using(StreamWriter writer = new StreamWriter(Application.dataPath + Path.AltDirectorySeparatorChar + "SaveData.json")) {
            writer.Write(json);
        }
    }

    public void LoadData() {

        string json = string.Empty;

        using (StreamReader reader = new StreamReader(Application.dataPath + Path.AltDirectorySeparatorChar + "SaveData.json")) {
            json = reader.ReadToEnd();
        }
        ProgressData data = JsonUtility.FromJson<ProgressData>(json);
        progressData.SetProgressData(data.level, data.bloodBombs, data.syringes);
    }
}
