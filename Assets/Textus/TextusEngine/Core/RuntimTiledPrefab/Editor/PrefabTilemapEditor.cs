using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(PrefabTilemap))]
public class PrefabTilemapEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Deleta tudo na moral"))
        {
            var prefabTilemap = target as PrefabTilemap;
            prefabTilemap.DeletaTudo();        
        }
    }
}
