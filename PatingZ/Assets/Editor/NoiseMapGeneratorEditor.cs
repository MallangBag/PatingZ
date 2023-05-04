using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AbstractNoiseMapGenerator), true)]
public class NoiseMapGeneratorEditor : Editor
{
    AbstractNoiseMapGenerator generator;

    private void Awake()
    {
        generator = (AbstractNoiseMapGenerator)target;
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if(GUILayout.Button("Create"))
        {
            generator.GenerateMap();
        }
        if (GUILayout.Button("Clear"))
        {
            generator.ClearMap();
        }
    }

}
