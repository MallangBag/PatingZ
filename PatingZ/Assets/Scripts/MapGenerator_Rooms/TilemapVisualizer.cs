/*
 * Ÿ�ϸ� �׸���
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
    /// Ÿ�ϸ� ���� ��ǥ���� �޴� �޼���
    /// </summary>
    /// <param name="floorPositions"> Ÿ�ϸ� ���� ���� ��ġ </param>
    public void PaintFloorTiles(IEnumerable<Vector2Int> floorPositions)
    {
        PaintTiles(floorPositions, floorTilemap, floorTile);
    }

    /// <summary>
    /// (internal) �� ���� ��ǥ�� �޴� �޼���
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
    /// Ÿ�ϸ� ������ ��ġ ã�� �޼���: 'Ÿ�� �׸���(����) �Լ�' ����
    /// </summary>
    /// <param name="positions"> ĥ�ؾ��ϴ� ��ġ�� (�������̽�) </param>
    /// <param name="tilemap"> Ÿ�� �׸� Ÿ�ϸ� (cf. ���̾�) </param>
    /// <param name="tile"> ĥ �� Ÿ�ϸ� ��� </param>
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
    /// �ش� ��ġ�� Ÿ�� ����� �޼���
    /// </summary>
    /// <param name="tilemap"> Ÿ�� �׸� Ÿ�ϸ� (cf. ���̾�) </param>
    /// <param name="tile"> ĥ �� Ÿ�ϸ� ��� </param>
    /// <param name="position"> ĥ �� ��ǥ </param>
    private void PaintSingleTile(Tilemap tilemap, TileBase tile, Vector2Int position)
    {
        var tilePosition = tilemap.WorldToCell((Vector3Int)position);
        tilemap.SetTile(tilePosition, tile);
    }

    /// <summary>
    /// ���� ĥ���� �ִ� Ÿ�ϵ� �����ϴ� �޼���
    /// </summary>
    public void Clear()
    {
        floorTilemap.ClearAllTiles();
        wallTilemap.ClearAllTiles();
    }
}
