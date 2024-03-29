﻿using UnityEngine;
using UnityEngine.UI;

public class SetColor : MonoBehaviour {
    [SerializeField] private bool IsThisImage = false;
    [SerializeField] private byte ID = 0;
    [SerializeField] private byte large = 1;

    void Start () {
        if (staticsColor.UseMaterial != 2)
            setColor ();
    }
    private void setColor () {
        Color a;
        if (IsThisImage)
            a = staticsColor.MatPalette[Random.Range (0, staticsColor.MatPalette.Length)];
        else if (staticsColor.UseMaterial == 0)
            a = staticsColor.FullMatPallette[Random.Range (0, staticsColor.FullMatPallette.Length)];
        else {
            large += 7;
            //use desired hue depending on ID
            a = staticsColor.Pallete[staticsColor.RndPalleteColor] * (large - ID) / large;
            a.a = 1;
        }

        if (IsThisImage)
            a = staticsColor.MatPalette[Random.Range (0, staticsColor.MatPalette.Length)];
        //sets a random color from the palette
        if (!IsThisImage)
            this.GetComponent<SpriteRenderer> ().color = a;
        else
            this.GetComponent<Image> ().color = a;
    }
}