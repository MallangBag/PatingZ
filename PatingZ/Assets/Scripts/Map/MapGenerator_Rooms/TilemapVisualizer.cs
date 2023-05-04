/*
 * 타일맵 그리기
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapVisualizer : MonoBehaviour
{
    [SerializeField]
    private Tilemap floorTilemap, wallTilemap;
    [SerializeField]
    private TileBase floorTile, wallTop, wallSideRight, wallSideLeft , wallBottom, wallFull;

    /// <summary>
    /// 타일맵 만들 좌표값들 받는 메서드
    /// </summary>
    /// <param name="floorPositions"> 타일맵 생성 시작 위치 </param>
    public void PaintFloorTiles(IEnumerable<Vector2Int> floorPositions)
    {
        PaintTiles(floorPositions, floorTilemap, floorTile);
    }

    /// <summary>
    /// (internal) 벽 만들 좌표값 받는 메서드
    /// </summary>
    /// <param name="position"></param>
    internal void PaintSingleBasicWall(Vector2Int position, string binaryType)
    {
        int typeAsInt = Convert.ToInt32(binaryType, 2);
        TileBase tile = null;
        if(WallTypeshelper.wallTop.Contains(typeAsInt))
        {
            tile = wallTop;
        }
        else if(WallTypeshelper.wallSideRight.Contains(typeAsInt))
        {
            tile = wallSideRight;
        }
        else if (WallTypeshelper.wallSideLeft.Contains(typeAsInt))
        {
            tile = wallSideLeft;
        }
        else if (WallTypeshelper.wallBottom.Contains(typeAsInt))
        {
            tile = wallBottom;
        }
        else if (WallTypeshelper.wallFull.Contains(typeAsInt))
        {
            tile = wallFull;
        }
        if (tile != null)
            PaintSingleTile(wallTilemap, tile, position);
    }

    /// <summary>
    /// 타일맵 생성할 위치 찾는 메서드: '타일 그리는(생성) 함수' 실행
    /// </summary>
    /// <param name="positions"> 칠해야하는 위치들 (인터페이스) </param>
    /// <param name="tilemap"> 타일 그릴 타일맵 (cf. 레이어) </param>
    /// <param name="tile"> 칠 할 타일맵 블록 </param>
    private void PaintTiles(IEnumerable<Vector2Int> positions, Tilemap tilemap, TileBase tile)
    {
        foreach (var position in positions)
        {
            PaintSingleTile(tilemap, tile, position);
        }
    }

    internal void PaintSingleCornerWall(Vector2Int position, string neighboursBinaryType)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// 해당 위치에 타일 만드는 메서드
    /// </summary>
    /// <param name="tilemap"> 타일 그릴 타일맵 (cf. 레이어) </param>
    /// <param name="tile"> 칠 할 타일맵 블록 </param>
    /// <param name="position"> 칠 할 좌표 </param>
    private void PaintSingleTile(Tilemap tilemap, TileBase tile, Vector2Int position)
    {
        var tilePosition = tilemap.WorldToCell((Vector3Int)position);
        tilemap.SetTile(tilePosition, tile);
    }

    /// <summary>
    /// 현재 칠해져 있는 타일들 삭제하는 메서드
    /// </summary>
    public void Clear()
    {
        floorTilemap.ClearAllTiles();
        wallTilemap.ClearAllTiles();
    }
}
