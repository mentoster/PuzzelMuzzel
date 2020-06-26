using UnityEngine;

public class OnPuzzleClick3 : MonoBehaviour
{
    [SerializeField] private PuzzleManagerLarge3 PuzzleManager;
    [SerializeField] private int ID = 0;
    private void OnMouseDown() => PuzzleManager.onAdPuzlleClick(this.gameObject, ID);
}