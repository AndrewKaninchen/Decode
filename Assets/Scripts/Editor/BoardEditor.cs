using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;

namespace Decode
{
    [CustomEditor(typeof(Board))]
    public class BoardEditor : Editor
    {
	    public override void OnInspectorGUI()
	    {
		    base.OnInspectorGUI();
		    var board = target as Board;

			if (GUILayout.Button("Clear Tiles"))
			{
			}

		    if (GUILayout.Button("Generate Tiles"))
		    {
		    }
	    }
    }
}