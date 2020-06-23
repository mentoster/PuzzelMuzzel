using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnPuzzleClick4 : MonoBehaviour
{
    [SerializeField]private PuzzleManagerLarge4 PuzzleManager;
    [SerializeField]private int ID;

    private void OnMouseDown()
    {
        PuzzleManager.onAdPuzlleClick(this.gameObject,ID);
    }
}
