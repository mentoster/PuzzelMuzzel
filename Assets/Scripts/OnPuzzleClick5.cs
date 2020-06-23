using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnPuzzleClick5 : MonoBehaviour
{
    [SerializeField]private PuzzleManager5 PuzzleManager;
    [SerializeField]private int ID;

    private void OnMouseDown()
    {
        PuzzleManager.onAdPuzlleClick(this.gameObject,ID);
    }
}
