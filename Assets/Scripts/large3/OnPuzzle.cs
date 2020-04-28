using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnPuzzle : MonoBehaviour
{
    public GameObject PuzzleManager;
    public int ID;

    private void OnMouseDown()
    {
      PuzzleManager.GetComponent<PuzzleManagerLarge3>().OnPuzzleClick(ID);
    }
}
