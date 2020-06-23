using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnPuzzleClick3 : MonoBehaviour
{
    [SerializeField]private PuzzleManagerLarge3 PuzzleManager;
    [SerializeField]private int ID;

    private void OnMouseDown()
    {
        PuzzleManager.onAdPuzlleClick(this.gameObject,ID);
    }
}