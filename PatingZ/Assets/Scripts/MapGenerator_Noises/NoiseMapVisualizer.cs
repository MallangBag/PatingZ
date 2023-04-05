using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class NoiseMapVisualizer : MonoBehaviour
{
    //타일맵: 배경, 벽
    [SerializeField]
    public Tilemap backgroundTilemap, wallTilemap; 
    //타일: 배경, 벽
    [SerializeField]
    private TileBase backgroundTile, wallTile;


    /// <summary>
    /// 타일맵 만들 위치 받는 메서드
    /// </summary>
    /// <param name="backgroundPositions"></param>
    internal void PaintSingleBasicBackground(Vector2Int backgroundPosition)
    {
        PaintSingleTile(backgroundPosition, backgroundTilemap, backgroundTile);
    }
    internal void PaintSingleBasicWall(Vector2Int position)
    {
        PaintSingleTile(position, wallTilemap, wallTile);
    }

    /// <summary>
    /// 타일 칠하는 메서드
    /// </summary>
    /// <param name="position"></param>
    /// <param name="tilemap"></param>
    /// <param name="tile"></param>
    private void PaintSingleTile(Vector2Int position, Tilemap tilemap, TileBase tile)
    {
        var tilePosition = tilemap.WorldToCell((Vector3Int)position);
        tilemap.SetTile(tilePosition, tile);
    }
    /// <summary>
    /// 전에 있던 타일들 삭제
    /// </summary>
    public void Clear()
    {
        backgroundTilemap.ClearAllTiles();
        wallTilemap.ClearAllTiles();
    }
}
