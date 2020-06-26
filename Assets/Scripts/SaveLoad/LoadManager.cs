using System;
using UnityEngine;

public class LoadManager : MonoBehaviour {
    //write here the variables that you need to save
    public bool Sound = true;
    public bool DarkTheme = false;
    public int timeInGame = 0;
    public int totalMoves = 0;
    public int numberOfWin = 0;
    public bool frstStart = true;
    public bool settingFrstStart = true;

    public void Awake () {
        GameData data = SaveSystem.LoadSave ();
        Sound = data.sound;
        DarkTheme = data.DarkTheme;
        timeInGame = data.timeInGame;
        totalMoves = data.totalMoves;
        numberOfWin = data.numberOfWin;
        frstStart = data.frstStart;
        settingFrstStart=data.settingFrstStart;
    }
    private void OnApplicationPause (bool pause) {
        if (pause)
            Save ();
    }
    private void OnApplicationQuit () {
        Save ();
    }
    private void Save () {
        timeInGame += (int) Time.time;
        SaveSystem.SaveData (this);
    }
}