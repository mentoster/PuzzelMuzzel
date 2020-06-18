using System;
using UnityEngine;

public class LoadManager : MonoBehaviour
{
    //write here the variables that you need to save 
    public bool Sound=true;
    public bool DarkTheme=false;
    public int timeInGame=0;
    public int totalMoves=0;
    public int numberOfWin=0;

    public void Awake()
    {
        GameData data = SaveSystem.LoadSave();
        Sound = data.sound;
        DarkTheme = data.DarkTheme;
        timeInGame = data.timeInGame;
        totalMoves = data.totalMoves;
        numberOfWin = data.numberOfWin;
    }
 #if UNITY_ANDROID && !UNITY_EDITOR
    private void OnApplicationPause(bool pause)
    {
        if (pause)
            SaveSystem.SaveData(this);
    }
#endif
    private void OnApplicationQuit()
    {
        SaveSystem.SaveData(this);
    }
}


