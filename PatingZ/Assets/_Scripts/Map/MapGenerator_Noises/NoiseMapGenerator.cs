using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class NoiseMapGenerator : AbstractNoiseMapGenerator
{
    [SerializeField]
    private int density = 60;
    [SerializeField]
    [Range(0, 10)]
    private int count;
    private Vector2Int position;

    protected override void RunProceduralGeneration()
    {
        MakeNoiseMap();
    }
 

    private void MakeNoiseMap()
    {
        MakeNoise();
        MakeNoisemapCellularAutomata();
    }

    //������ ��� �� ����
    private void MakeNoise()
    {
        for (int height = 0; height < noiseMap.mapHeight; height++)
        {
            for (int width = 0; width < noiseMap.mapWidth; width++)
            {
                position.x = width;
                position.y = height;
                if (Random.Range(1, 100) > density)
                    noiseMap.PaintSingleBasicBackground(position);
                else
                    noiseMap.PaintSingleBasicWall(position);
            }
        }
    }


    //������ ���� Cellular Automata ������� ���� ������
    //�ܺ� 1ĭ �̻��� ���� �ǰ�
    private void MakeNoisemapCellularAutomata()
    {
        for (int i = 0; i < count; i++)
        {
            CopyTilemap();
            for (int x = 0; x < noiseMap.mapWidth; x++)
            {
                for (int y = 0; y < noiseMap.mapHeight; y++)
                {
                    int neighborWallCount = 0;
                    bool border = false;
                    for (int xTemp = x - 1; xTemp <= x + 1; xTemp++)
                    {
                        for (int yTemp = y - 1; yTemp <= y + 1; yTemp++)
                        {
                            if (0 <= xTemp && xTemp < noiseMap.mapWidth && 0 <= yTemp && yTemp < noiseMap.mapHeight)
                            {
                                if (xTemp != x || yTemp != y)
                                    if (noiseMap.tempTilemap.GetTile(new Vector3Int(xTemp, yTemp, 0)) == noiseMap.wallTileBase)
                                        neighborWallCount++;
                            }
                            else
                                border = true;
                            }
                    }
                    if (neighborWallCount > 4 || border)
                    {
                        noiseMap.PaintSingleBasicWall(new Vector2Int(x, y));
                    }
                    else
                    {
                        noiseMap.PaintSingleBasicBackground(new Vector2Int(x, y));
                    }
                }
            }
        }
    }

    private void CopyTilemap()
    {
        BoundsInt bounds = noiseMap.mapTilemap.cellBounds;

        for (int x = bounds.min.x; x < bounds.max.x; x++)
        {
            for (int y = bounds.min.y; y < bounds.max.y; y++)
            {
                Vector3Int tilePos = new Vector3Int(x, y, 0);
                TileBase originalTile = noiseMap.mapTilemap.GetTile(tilePos);
                noiseMap.tempTilemap.SetTile(tilePos, originalTile);
            }
        }

        //GPT�� ���� �߰� �ҽ�
        //// �߰��� ������ Tile�� �ִٸ�, �ش� Tile�� �߰�
        ////if (tileToCopy != null)
        ////{
        ////    for (int x = bounds.min.x; x < bounds.max.x; x++)
        ////    {
        ////        for (int y = bounds.min.y; y < bounds.max.y; y++)
        ////        {
        ////            Vector3Int tilePos = new Vector3Int(x, y, 0);
        ////            TileBase copiedTile = copiedTilemap.GetTile(tilePos);
        ////            if (copiedTile != null)
        ////            {
        ////                copiedTilemap.SetTile(tilePos, tileToCopy);
        ////            }
        ////        }
        ////    }
        ////}
    }

}


