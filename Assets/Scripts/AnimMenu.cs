using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class AnimMenu : MonoBehaviour {
    [SerializeField] private RectTransform thisUI = null;
    private Vector2 startPositionOfAnim;
    public float duration = 0.4f;
    private void Start () => startPositionOfAnim = thisUI.position;
    private void OnEnable () => thisUI.DOAnchorPos (Vector2.zero, duration);
    private void OnDisable () => thisUI.position = startPositionOfAnim;
}