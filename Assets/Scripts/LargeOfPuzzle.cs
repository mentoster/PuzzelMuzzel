using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LargeOfPuzzle : MonoBehaviour
{
   public Camera MainCamera;
   public GameObject large9;
   public GameObject large16;
   public GameObject large32;
   public void gameStart()
   {
      switch (Statics.PuzzleLarge)
      {
         case 5:
            GeneratePuzzle(5);
            break;
         case 4:
            GeneratePuzzle(4);
            break;
         default://3
            GeneratePuzzle(3);
            break;
      }
   }

   private void GeneratePuzzle(int Large)
   {
      GetComponent<PuzzlesManager>().puzzleLarge = Large;
   }
  
}
