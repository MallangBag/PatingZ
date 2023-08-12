using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


/// <summary>
/// 맵 관련 오브젝트(벽, 배경, 문 등)
/// TileBase 지정 -> 생성 하는 클래스
/// </summary>
public class NoiseMapVisualizer : MonoBehaviour
{
    //타일맵: 맵
    [SerializeField]
    public Tilemap mapTilemap, tempTilemap, gridTilemap;
    [SerializeField]
    public int mapWidth, mapHeight;
    //타일: 배경, 벽
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

    //그리드 없이 그냥 그림
    private void PaintSingleTile(Vector2Int position, TileBase tile)
    {
        //vec3좌표를 tile좌표로
        var tilePosition = mapTilemap.WorldToCell((Vector3Int)position);

        //타일 유일하게
        if (mapTilemap.GetTile(tilePosition) != null)
        {
            SingleClear(tilePosition);
        }
        mapTilemap.SetTile(tilePosition, tile);
    }

    //그리드 그림
    private void PaintSingleGridTile(Vector2Int position, TileBase tile)
    {
        //vec3좌표를 tile좌표로
        var tilePosition = mapTilemap.WorldToCell((Vector3Int)position);

        //타일 유일하게
        if (mapTilemap.GetTile(tilePosition) != null)
        {
            SingleClear(tilePosition);
        }
        mapTilemap.SetTile(tilePosition, tile);
        gridTilemap.SetTile(tilePosition, gridTileBase);
    }


    //temp타일 제외(temp의 active는 false라 신경 안써도 됨)
    //타일맵의 특정 위치 타일 삭제
    private void SingleClear(Vector3Int position)
    {
        mapTilemap.SetTile(position, null);
        gridTilemap.SetTile(position, null);
    }


    //타일맵의 타일들 전부 삭제
    public void Clear()
    {
        gridTilemap.ClearAllTiles();
        mapTilemap.ClearAllTiles();
        tempTilemap.ClearAllTiles();
    }
}
