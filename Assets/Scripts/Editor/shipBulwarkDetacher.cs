using UnityEngine;
using UnityEditor;
using System.Collections;

[CanEditMultipleObjects]
[CustomEditor(typeof(shipComponentControl))]
public class shipBulwarkDetacher : Editor
{

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Detach"))
        {
            ((shipComponentControl)target).Detach();
        }
    }
}
