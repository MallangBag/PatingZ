/// <summary>
/// https://www.youtube.com/watch?v=yI8fMXbtjew
/// - Create Rule Tiles in SECONDS in Unity - 
/// </summary>


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Tilemaps;

#if UNITY_EDITOR
public class RuleTileGenerator : EditorWindow
{
    [MenuItem("VinTools/Editor Windows/Rule Tile Generator")]
    public static void ShowWindow()
    {
        GetWindow<RuleTileGenerator>("Rule Tile Generator");
    }

    Vector2 scrollpos;

    private void OnGUI()
    {
        scrollpos = GUILayout.BeginScrollView(scrollpos);

        EditorGUILayout.Space();
        GUILayout.Label("Template setup", EditorStyles.boldLabel);
        EditorGUILayout.Space();

        ScriptableObject target = this;
        SerializedObject so = new SerializedObject(target);
        SerializedProperty prp = so.FindProperty("templateSprites");
        EditorGUILayout.PropertyField(prp, true);
        so.ApplyModifiedProperties();



        GUILayout.EndScrollView();
    }
}
#endif
