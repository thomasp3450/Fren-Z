using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressData {

    public int level;
    public int bloodBombs;
    public int syringes;

    public delegate void OnProgressDataChange (int level, int bloodBombs, int syringes);
    public static event OnProgressDataChange OnDataChange;
    private static ProgressData _instance = null;
    public static ProgressData Instance {
        get {
            if (_instance == null) {
                _instance = new ProgressData(1, 0, 0);
            }
            return _instance;
        }
    }

    private ProgressData(int level, int bloodBombs, int syringes) {
        this.level = level;
        this.bloodBombs = bloodBombs;
        this.syringes = syringes;
    }

    public void SetProgressData(int level, int bloodBombs, int syringes) {
        this.level = level;
        this.bloodBombs = bloodBombs;
        this.syringes = syringes;
        OnDataChange?.Invoke(level, bloodBombs, syringes);
    }
}