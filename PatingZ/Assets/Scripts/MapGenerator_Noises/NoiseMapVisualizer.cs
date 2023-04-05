using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class NoiseMapVisualizer : MonoBehaviour
{
    //Ÿ�ϸ�: ���, ��
    [SerializeField]
    public Tilemap backgroundTilemap, wallTilemap; 
    //Ÿ��: ���, ��
    [SerializeField]
    private TileBase backgroundTile, wallTile;


    /// <summary>
    /// Ÿ�ϸ� ���� ��ġ �޴� �޼���
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
    /// Ÿ�� ĥ�ϴ� �޼���
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
    /// ���� �ִ� Ÿ�ϵ� ����
    /// </summary>
    public void Clear()
    {
        backgroundTilemap.ClearAllTiles();
        wallTilemap.ClearAllTiles();
    }
}
