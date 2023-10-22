using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ObjectFrameHandler))]
public class ObjectFrameHandlerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        ObjectFrameHandler frameHandler = (ObjectFrameHandler)target;

        DrawDefaultInspector();

        if (GUILayout.Button("Save Current State."))
        {
            frameHandler.SaveState();
        }
    }
}