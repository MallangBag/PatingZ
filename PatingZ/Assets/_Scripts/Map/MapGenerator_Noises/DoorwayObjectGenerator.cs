using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �� ������Ʈ: ���Ա� ����� Ŭ����
/// 3X3 ����
/// Ž��: ������ ������ �Ʒ�
/// </summary>

public class DoorwayObjectGenerator : AbstractNoiseMapGenerator
{
    public Vector2Int entrancePosition;
    public Vector2Int exitPosition;


    protected override void RunProceduralGeneration()
    {
        MakeEntrance();
        MakeExit();
    }


    private void MakeEntrance()
    {
        #region DFS��
        //y: max -> 0 (�� -> �Ʒ�)
        //for (int y = noiseMapVisualizer.mapHeight; y >= 0 && !isCreate; y--)
        //{
        //    //x�� 0 ���� max���� �������� 
        //    for (int x = 0; x <= noiseMapVisualizer.mapWidth && !isCreate; x++)
        //    {
        //        isCreate = AccordCondition(new Vector2Int(x, y));      //���ǿ� �´°� ã��

        //    }
        //}
        #endregion

        //x�� �������� BFS ��
        //�� ������ ���� ��� ������
        bool isCreate = false;              //�� ���� Ȯ�� �÷���
        int x = 0, y = noiseMap.mapHeight;  //���� ��ܺ��� ������
        int searchNum;
        Vector2Int currentPosition;
        while (x <= noiseMap.mapWidth && !isCreate)
        {
            searchNum = 0;
            while (x - searchNum >= 0 && !isCreate)
            {
                if(y - searchNum >= 0)
                {
                    currentPosition = new Vector2Int(x - searchNum, y - searchNum);
                    //Debug.Log($"x: {currentPosition.x}, y: {currentPosition.y}");
                    isCreate = AccordDoorCreating(currentPosition);
                    entrancePosition = currentPosition;
                    searchNum++;
                }
                else
                {
                    searchNum++;
                }
            }
            x++;
        }
    }

    //�ⱸ�� �ϴ� �ϳ�
    private void MakeExit()
    {
        #region DFS�� 
        //�� ����� �ݺ��� ����
        //���η� Ž����
        //x: max -> 0 (������ -> ����)
        //bool isCreate = false;
        //for (int x = noiseMap.mapWidth; x >= 0 && !isCreate; x--)
        //{
        //    //y: 0 -> max (�Ʒ� -> ��)
        //    for (int y = 0; y <= noiseMap.mapHeight && !isCreate; y++)
        //    {
        //        isCreate = AccordDoorCreating(new Vector2Int(x, y));      //���ǿ� �´°� ã��
        //    }
        //}

        //isCreate = false;
        //for (int y = noiseMap.mapHeight; y >= 0 && !isCreate; y--)
        //{
        //    for (int x = noiseMap.mapWidth; x >= 0 && !isCreate; x--)

        //    {
        //        isCreate = AccordDoorCreating(new Vector2Int(x, y));      //���ǿ� �´°� ã��
        //    }
        //}
        #endregion

        //x�� �������� BFS ��
        //�� ������ ������ �ϴ� ������
        bool isCreate = false;              //�� ���� Ȯ�� �÷���
        int x = noiseMap.mapWidth, y = 0;  //���� ��ܺ��� ������
        int searchNum;
        Vector2Int currentPosition;
        while (x >= 0 && !isCreate)
        {
            searchNum = 0;
            while (x + searchNum <= noiseMap.mapWidth && !isCreate)
            {
                if (y + searchNum <= noiseMap.mapHeight)
                {
                    currentPosition = new Vector2Int(x + searchNum, y + searchNum);
                    //Debug.Log($"x: {currentPosition.x}, y: {currentPosition.y}");
                    isCreate = AccordDoorCreating(currentPosition);
                    exitPosition = currentPosition;
                    searchNum++;
                }
                else
                {
                    searchNum++;
                }
            }
            x--;
        }

    }


    private bool AccordDoorCreating(Vector2Int currentPosition)
    {
        //���� ��ġ x y ��
        int xRound = currentPosition.x;
        int yRound = currentPosition.y;
        
        //��
        var tilemap = noiseMap.mapTilemap;
        var wall = noiseMap.wallTileBase;

        //������ �ϳ��� Ž���ϴµ� ����ȭ �Ϸ��� currentPosition �� �ϳ��� ���� �ϴ°� ���ǿ� ���缭 �ؾ���
        //�� �� ��
        //�� �� �� ����϶� ����
        //x �� x

        int spaceCount = 0;
        //�׵θ� �κ��� ��ã�Ƶ� �� 
        if(xRound == 0 || xRound == noiseMap.mapWidth || yRound == 0 || yRound == noiseMap.mapHeight)
        {
            return false;
        }
        else
        {
            for (int y = yRound + 1; y >= yRound; y--)
            {
                for (int x = xRound - 1; x <= xRound + 1; x++)
                {
                    if (tilemap.GetTile(new(x, y)) == wall)
                    {
                        return false;
                    }
                    else
                    {
                        spaceCount++;
                    }
                }
            }
        }
        //Debug.Log($"currentPosition: {currentPosition}, spaceCount: {spaceCount}");
        if (spaceCount == 6)
        {
            if(tilemap.GetTile(new Vector3Int(xRound, yRound - 1)) == wall)
            {
                noiseMap.PaintSingleBasicDoor(currentPosition);
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
}

