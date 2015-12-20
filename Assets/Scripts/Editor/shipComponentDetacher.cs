using UnityEngine;
using UnityEditor;
using System.Collections;

[CanEditMultipleObjects]
[CustomEditor(typeof(shipBulwarkControl))]
public class shipComponentDetacher : Editor {

	public override void OnInspectorGUI ()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Detach"))
        {
            ((shipBulwarkControl)target).Detach();
        }
    }
}
