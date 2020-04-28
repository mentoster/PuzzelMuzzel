using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnPuzzeLarge4 : MonoBehaviour
{

    public GameObject PuzzleManager;
    public int ID;

    private void OnMouseDown()
    {
        PuzzleManager.GetComponent<PuzzleManagerLarge4>().OnPuzzleClick(ID);
    }
    
}
