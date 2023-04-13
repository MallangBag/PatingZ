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
    [SerializeField]
    private int mapWidth, mapHeight;
    private Vector2Int position;
    [SerializeField]
    Tilemap tempTilemap;



    protected override void RunProceduralGeneration()
    {
        MakeNoiseMap();
    }

    private void MakeNoiseMap()
    {
        MakeNoise();
        MakeNoisemapAutomaton();
    }

    private void MakeNoisemapAutomaton()
    {
        for (int i = 0; i < count; i++)
        {
            CopyTilemap();
            for (int x = 0; x <= mapWidth; x++)
            {
                for (int y = 0; y <= mapHeight; y++)
                {
                    int neighborWallCount = 0;
                    bool border = false;
                    for (int xTemp = x - 1; xTemp <= x + 1; xTemp++)
                    {
                        for (int yTemp = y - 1; yTemp <= y + 1; yTemp++)
                        {
                            if (1 <= xTemp && xTemp < mapWidth && 1 <= yTemp && yTemp < mapHeight)
                            {
                                if (xTemp != x || yTemp != y)
                                    if (tempTilemap.GetTile(new Vector3Int(xTemp, yTemp, 0)) == noiseMapVisualizer.wallTile)
                                        neighborWallCount++;
                            }
                            else
                                border = true;
                            }
                    }
                    if (neighborWallCount > 4 || border)
                    {
                        noiseMapVisualizer.PaintSingleBasicWall(new Vector2Int(x, y));
                    }
                    else
                    {
                        noiseMapVisualizer.PaintSingleBasicBackground(new Vector2Int(x, y));
                    }
                }
            }
        }
    }

    private void CopyTilemap()
    {
        BoundsInt bounds = noiseMapVisualizer.mapTilemap.cellBounds;

        for (int x = bounds.min.x; x < bounds.max.x; x++)
        {
            for (int y = bounds.min.y; y < bounds.max.y; y++)
            {
                Vector3Int tilePos = new Vector3Int(x, y, 0);
                TileBase originalTile = noiseMapVisualizer.mapTilemap.GetTile(tilePos);
                tempTilemap.SetTile(tilePos, originalTile);
            }
        }

        //GPT가 말한 추가 소스
        //// 추가로 복제할 Tile이 있다면, 해당 Tile을 추가
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

    private void MakeNoise()
    {
        for (int height = 0; height <= mapHeight; height++)
        {
            for (int width = 0; width <= mapWidth; width++)
            {
                position.x = width;
                position.y = height;
                if (Random.Range(1, 100) > density)
                    noiseMapVisualizer.PaintSingleBasicBackground(position);
                else
                    noiseMapVisualizer.PaintSingleBasicWall(position);
            }
        }
    }
}


