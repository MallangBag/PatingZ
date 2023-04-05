using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class NoiseMapGenerator : AbstractNoiseMapGenerator
{
    [SerializeField]
    private int density = 60;
    [SerializeField]
    [Range(1, 10)]
    private int count;
    [SerializeField]
    private int mapWidth, mapHeight;
    private Vector2Int position;


    protected override void RunProceduralGeneration()
    {
        MakeNoiseMap();
    }

    private void MakeNoiseMap()
    {
        MakeNoisemapAutomaton();
        //MakeNoise();
    }

    private void MakeNoisemapAutomaton()
    {
        for (int i = 1; i < count; i++)
        {
            Grid tempGrid = noiseMapVisualizer.backgroundTilemap.layoutGrid;
            for (int j = 1; j < mapHeight; j++)
            {
                for (int k = 1; k < mapWidth; k++)
                {
                    int neighborWallCount = 0;
                    for (int y = j - 1; y < j + 1; y++)
                    {
                        for (int x = k - 1; x < k + 1; x++)
                        {

                            if (x >= 1 && y >= 1
                                && noiseMapVisualizer.backgroundTilemap.cellBounds.size.x >= x && noiseMapVisualizer.backgroundTilemap.cellBounds.size.y >= y)
                            {
                                if (y != j || x != k)
                                    Debug.Log(noiseMapVisualizer.wallTilemap.GetTile(tempGrid.WorldToCell(new Vector3(y, x, 0))));
                                    if (noiseMapVisualizer.wallTilemap.GetTile(tempGrid.WorldToCell(new Vector3(y, x, 0))) == null)
                                        neighborWallCount++;
                            }
                            else
                                neighborWallCount++;
                        }
                    }
                    Debug.Log(neighborWallCount);
                    if (neighborWallCount > 4)
                    {
                        noiseMapVisualizer.PaintSingleBasicWall(new Vector2Int(j, k));
                    }
                    else
                    {
                        noiseMapVisualizer.PaintSingleBasicBackground(new Vector2Int(j, k));
                    }
                }
            }

        }
    }

    private void MakeNoise()
    {
        for (int height = 0; height < mapHeight; height++)
        {
            for (int width = 0; width < mapWidth; width++)
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


