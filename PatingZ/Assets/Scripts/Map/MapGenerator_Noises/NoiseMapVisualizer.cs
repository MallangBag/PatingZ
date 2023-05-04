using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


/// <summary>
/// �� ���� ������Ʈ(��, ���, �� ��)
/// TileBase ���� -> ���� �ϴ� Ŭ����
/// </summary>
public class NoiseMapVisualizer : MonoBehaviour
{
    //Ÿ�ϸ�: ��
    [SerializeField]
    public Tilemap mapTilemap, tempTilemap, gridTilemap;
    [SerializeField]
    public int mapWidth, mapHeight;
    //Ÿ��: ���, ��
    [SerializeField]
    public TileBase backgroundTileBase, wallTileBase, doorTileBase, gridTileBase;


    internal void PaintSingleBasicBackground(Vector2Int position)
    {
        PaintSingleGridTile(position, backgroundTileBase);
    }
    internal void PaintSingleBasicWall(Vector2Int position)
    {
        PaintSingleGridTile(position, wallTileBase);
    }
    internal void PaintSingleBasicDoor(Vector2Int position)
    {
        PaintSingleTile(position, doorTileBase);
    }

    //�׸��� ���� �׳� �׸�
    private void PaintSingleTile(Vector2Int position, TileBase tile)
    {
        //vec3��ǥ�� tile��ǥ��
        var tilePosition = mapTilemap.WorldToCell((Vector3Int)position);

        //Ÿ�� �����ϰ�
        if (mapTilemap.GetTile(tilePosition) != null)
        {
            SingleClear(tilePosition);
        }
        mapTilemap.SetTile(tilePosition, tile);
    }

    //�׸��� �׸�
    private void PaintSingleGridTile(Vector2Int position, TileBase tile)
    {
        //vec3��ǥ�� tile��ǥ��
        var tilePosition = mapTilemap.WorldToCell((Vector3Int)position);

        //Ÿ�� �����ϰ�
        if (mapTilemap.GetTile(tilePosition) != null)
        {
            SingleClear(tilePosition);
        }
        mapTilemap.SetTile(tilePosition, tile);
        gridTilemap.SetTile(tilePosition, gridTileBase);
    }


    //tempŸ�� ����(temp�� active�� false�� �Ű� �Ƚᵵ ��)
    //Ÿ�ϸ��� Ư�� ��ġ Ÿ�� ����
    private void SingleClear(Vector3Int position)
    {
        mapTilemap.SetTile(position, null);
        gridTilemap.SetTile(position, null);
    }


    //Ÿ�ϸ��� Ÿ�ϵ� ���� ����
    public void Clear()
    {
        gridTilemap.ClearAllTiles();
        mapTilemap.ClearAllTiles();
        tempTilemap.ClearAllTiles();
    }
}
