using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnPuzzleLarge5 : MonoBehaviour
{
    public GameObject PuzzleManager;
    public int ID;

    private void OnMouseDown()
    {
        PuzzleManager.GetComponent<PuzzleManager5>().OnPuzzleClick(ID);
    }
}
