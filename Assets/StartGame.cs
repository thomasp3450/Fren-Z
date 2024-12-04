using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class StartGame : MonoBehaviour
{

    GameObject player;

    public ProgressData progressData;

    // Start is called before the first frame update
    void Start(){ 
        progressData = ProgressData.Instance;
    }

    // Update is called once per frame
    void Update() {
        progressData.SetProgressData(1, 0, 0);
        SaveData();
    }

    public void SaveData() {
        string json = JsonUtility.ToJson(progressData);

        using(StreamWriter writer = new StreamWriter(Application.dataPath + Path.AltDirectorySeparatorChar + "SaveData.json")) {
            writer.Write(json);
        }
    }
}
