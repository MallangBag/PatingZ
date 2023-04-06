using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class NoiseMapCreate : MonoBehaviour
{

    Dictionary<int, TileBase> tileSet;
    Dictionary<int, TileBase> tileGroups;
    public TileBase background;
    public TileBase wall;


    // Start is called before the first frame update
    void Start()
    {
        //CreateTileSet();
        //CreateTileGroups();
    }
    private void CreateTileSet()
    {
        tileSet = new();
        tileSet.Add(0, background);
        tileSet.Add(1, wall);
    }
    private void CreateTileGroups()
    {
        tileGroups = new();
        foreach (KeyValuePair<int, TileBase> prefabPair in tileSet)
        {
        }
    }
}
