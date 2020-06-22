using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnPuzzleClick3 : MonoBehaviour
{
    public PuzzleManagerLarge3 PuzzleManager;
    public int ID;

    private void OnMouseDown()
    {
        PuzzleManager.onAdPuzlleClick(this.gameObject,ID);
    }
}