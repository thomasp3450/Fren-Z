using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressData {

    public int level;

    public delegate void OnProgressDataChange (int level);
    public static event OnProgressDataChange OnDataChange;
    private static ProgressData _instance = null;
    public static ProgressData Instance {
        get {
            if (_instance == null) {
                _instance = new ProgressData(1);
            }
            return _instance;
        }
    }

    private ProgressData(int level) {
        this.level = level;
    }

    public void SetProgressData(int level) {
        this.level = level;
        OnDataChange?.Invoke(level);
    }
}