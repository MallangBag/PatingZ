using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class NoiseMapVisualizer : MonoBehaviour
{
    //Ÿ�ϸ�: ��
    [SerializeField]
    public Tilemap mapTilemap, tempTilemap; 
    //Ÿ��: ���, ��
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
        //Ÿ�� ĥ �ϴ� �޼���
        var tilePosition = mapTilemap.WorldToCell((Vector3Int)position);
        if (mapTilemap.GetTile(tilePosition) != null)
        {
            mapTilemap.SetTile(tilePosition, null);
        }
        mapTilemap.SetTile(tilePosition, tile);
    }

    public void Clear()
    {
        //Ÿ�� ���� ����
        mapTilemap.ClearAllTiles();
        tempTilemap.ClearAllTiles();
    }
}
