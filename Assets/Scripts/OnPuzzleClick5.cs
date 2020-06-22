using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnPuzzleClick5 : MonoBehaviour
{
    public PuzzleManager5 PuzzleManager;
    public int ID;

    private void OnMouseDown()
    {
        PuzzleManager.onAdPuzlleClick(this.gameObject,ID);
    }
}
