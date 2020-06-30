using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnPuzzleClick4 : MonoBehaviour {
    [SerializeField] private PuzzleManagerLarge4 PuzzleManager = null;
    [SerializeField] private int ID = 0;

    private void OnMouseDown () => PuzzleManager.onAdPuzlleClick (this.gameObject, ID);
}