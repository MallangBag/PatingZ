using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class NoiseMapVisualizer : MonoBehaviour
{
    //타일맵: 맵
    [SerializeField]
    public Tilemap mapTilemap, tempTilemap; 
    //타일: 배경, 벽
    [SerializeField]
    public TileBase backgroundTile, wallTile;

    internal void PaintSingleBasicBackground(Vector2Int position)
    {
        PaintSingleTile(position, backgroundTile);
    }
    internal void PaintSingleBasicWall(Vector2Int position)
    {
        PaintSingleTile(position, wallTile);
    }

    private void PaintSingleTile(Vector2Int position, TileBase tile)
    {
        //타일 칠 하는 메서드
        var tilePosition = mapTilemap.WorldToCell((Vector3Int)position);
        if (mapTilemap.GetTile(tilePosition) != null)
        {
            mapTilemap.SetTile(tilePosition, null);
        }
        mapTilemap.SetTile(tilePosition, tile);
    }

    public void Clear()
    {
        //타일 전부 삭제
        mapTilemap.ClearAllTiles();
        tempTilemap.ClearAllTiles();
    }
}
