using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DataTestScript))]
public class DataTestScriptEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        DataTestScript script = (DataTestScript)target;

        EditorGUILayout.Space();

        if (GUILayout.Button("Randomize All"))
            script.RandomizeAll();
    }
}