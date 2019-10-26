using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

[CustomEditor(typeof(MegaCorp))]
public class MegaCorpEditor : Editor
{
    private ReorderableList list;

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        
        var levels = serializedObject.FindProperty("levels");
        if (list == null) list = ReorderableListUtility.CreateAutoLayout(levels);
        list.drawElementCallback = (rect, index, active, focused) => {
                EditorGUI.PropertyField(rect, levels.GetArrayElementAtIndex(index), GUIContent.none); };
        list.onAddCallback = (x) => levels.InsertArrayElementAtIndex(levels.arraySize);
        ReorderableListUtility.DoLayoutListWithFoldout(list);
        
        serializedObject.ApplyModifiedProperties();
    }
}