using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

[CustomEditor(typeof(LevelSelector))]
public class LevelSelectorEditor : Editor
{
    private ReorderableList list;
    
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        var levelDisplay = serializedObject.FindProperty("levelDisplay");
        EditorGUILayout.PropertyField(levelDisplay);
        
        var corps = serializedObject.FindProperty("corps");
        if (list == null) list = ReorderableListUtility.CreateAutoLayout(corps, true, false, true, true);
        list.drawElementCallback = (rect, index, active, focused) =>
        {
            EditorGUI.PropertyField(rect, corps.GetArrayElementAtIndex(index), GUIContent.none);
        };
        list.drawHeaderCallback = (x) => {EditorGUI.LabelField(x, "Corps");};

        list.DoLayoutList();
//        ReorderableListUtility.DoLayoutListWithFoldout(list);
        serializedObject.ApplyModifiedProperties();
    }
}