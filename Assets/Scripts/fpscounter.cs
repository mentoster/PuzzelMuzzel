﻿﻿// Author: Daniele Giardini http://www.demigiant.com
// Created: 2018/07/13

using UnityEngine;


// An FPS counter.
// It calculates frames/second over each updateInterval,
// so the display does not keep changing wildly.
public class fpscounter : MonoBehaviour
{
    public float updateInterval = 0.5F;
    private double lastInterval;
    private int frames = 0;
    private float fps;
    void Start()
    {
        lastInterval = Time.realtimeSinceStartup;
        frames = 0;
    }

    void OnGUI() => GUILayout.Label("" + fps.ToString("f2"));

    void Update()
    {
        ++frames;
        var timeNow = Time.realtimeSinceStartup;
        if (timeNow > lastInterval + updateInterval)
        {
            fps = (float)(frames / (timeNow - lastInterval));
            frames = 0;
            lastInterval = timeNow;
        }

    }
}