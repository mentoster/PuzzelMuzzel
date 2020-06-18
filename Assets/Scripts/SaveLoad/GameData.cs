[System.Serializable]
public class GameData
{
    //write here the variables that you need to save 
    //there
    public bool sound;
    public bool DarkTheme;
    public int  timeInGame;
    public int totalMoves;
    public int numberOfWin;
    //constructor
    public GameData (LoadManager loadManager)
    {
        //and there
        sound = loadManager.Sound;
        DarkTheme = loadManager.DarkTheme;
        timeInGame = loadManager.timeInGame;
        totalMoves = loadManager.totalMoves;
        numberOfWin = loadManager.numberOfWin;
    }
}
    
