using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AvatarTalk))]
public class EditorAvatarTalk : Editor
{
    public AvatarTalk avatarTalk;

    int index = 0;
    string[] options;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        avatarTalk = target as AvatarTalk;

        options = Microphone.devices;

        Rect r = EditorGUILayout.BeginHorizontal();
        index = EditorGUILayout.Popup("Choose Mic:",
             index, options, EditorStyles.popup);
        if (GUILayout.Button("Start Mic"))
            EnableMic();
        if (GUILayout.Button("Stop Mic"))
            StopMic();
        EditorGUILayout.EndHorizontal();
    }

    public void EnableMic()
    {
        avatarTalk.micName = options[index];
        avatarTalk.StartMicrophone();
    }

    public void StopMic()
    {
        avatarTalk.StopMicrophone();
    }
}
