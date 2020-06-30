using UnityEngine;

public class OnPuzzleClick5 : MonoBehaviour {
  [SerializeField] private PuzzleManager5 PuzzleManager = null;
  [SerializeField] private int ID = 0;

  private void OnMouseDown () => PuzzleManager.onAdPuzlleClick (this.gameObject, ID);
}