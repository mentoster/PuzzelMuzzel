using UnityEngine;
using UnityEngine.UI;
public class ImagineCut : MonoBehaviour {

	[SerializeField] private SpriteRenderer[] puzzles = null;
	//[SerializeField] private const int _large = 3;
	[SerializeField] private Texture2D source = null;
	private Texture2D[] pieces;
	//[SerializeField] private int size = 500;
	[SerializeField] private int PuzzlesLarge = 3;
	[SerializeField] private float _pieceZoom = 5;

	void Start () { if (staticsColor.UseMaterial == 2) BuildPieces (); }

	//divide into pieces
	//https://forum.unity.com/threads/cut-an-image-into-9-pieces-c.527748/
	void BuildPieces () {
		//set one piece size
		// var add=(source.width- source.height)/2;
		if (source == null)
			source = statics.source;
		var PiecSize = 0;
		bool heightBigger = source.height > source.width;
		var add = 0;
		if (heightBigger) {
			PiecSize = source.width;
			add = (source.height - source.width) / 2;
		} else {
			PiecSize = source.height;
			add = (source.width - source.height) / 2;
		}
		PiecSize /= PuzzlesLarge;
		pieces = new Texture2D[PuzzlesLarge * PuzzlesLarge];
		for (int i = 0; i < PuzzlesLarge; i++)
			for (int j = 0; j < PuzzlesLarge; j++) {
				int index = i * PuzzlesLarge + j;
				pieces[index] = new Texture2D (PiecSize, PiecSize);
				Color[] pixels;
				//select the square in the picture
				if (heightBigger)
					pixels = source.GetPixels (PiecSize * i, PiecSize * j + add, PiecSize, PiecSize);
				else
					pixels = source.GetPixels (PiecSize * i + add, PiecSize * j, PiecSize, PiecSize);
				pieces[index].SetPixels (pixels);
				pieces[index].Apply ();
			}
		for (int i = 0; i < PuzzlesLarge * PuzzlesLarge; i++)
			//Convert from texture to sprite
			puzzles[i].sprite = Sprite.Create (pieces[i], new Rect (0.0f, 0.0f, pieces[i].width, pieces[i].height), new Vector2 (0.5f, 0.5f), PiecSize / _pieceZoom);
	}
}