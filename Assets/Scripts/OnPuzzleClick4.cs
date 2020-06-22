using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnPuzzleClick4 : MonoBehaviour
{
    public PuzzleManagerLarge4 PuzzleManager;
    public int ID;

    private void OnMouseDown()
    {
        PuzzleManager.onAdPuzlleClick(this.gameObject,ID);
    }
}
