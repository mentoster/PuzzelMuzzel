﻿using UnityEditor;
using UnityEngine;

namespace EckTechGames
{
	[InitializeOnLoad]
	public class AutoSaveExtension
	{
        // Static constructor that gets called when unity fires up.
        [System.Obsolete]
        static AutoSaveExtension() => EditorApplication.playmodeStateChanged += AutoSaveWhenPlaymodeStarts;

    [System.Obsolete]
    private static void AutoSaveWhenPlaymodeStarts()
		{
			// If we're about to run the scene...
			if (EditorApplication.isPlayingOrWillChangePlaymode && !EditorApplication.isPlaying)
			{
				// Save the scene and the assets.
				EditorApplication.SaveScene();
				AssetDatabase.SaveAssets();
			}
		}
	}
}