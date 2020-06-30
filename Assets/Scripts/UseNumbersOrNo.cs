using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseNumbersOrNo : MonoBehaviour {

    void Start () {
        if (staticsColor.UseMaterial == 2)
            this.gameObject.SetActive (false);
    }

}