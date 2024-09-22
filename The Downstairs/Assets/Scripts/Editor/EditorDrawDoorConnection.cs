using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Door))]
class EditorDrawDoorConnection : Editor
{
    void OnSceneGUI()
    {
        var door = target as Door;
        Handles.DrawLine(door.transform.position, door.target.transform.position);
    }
    
}
