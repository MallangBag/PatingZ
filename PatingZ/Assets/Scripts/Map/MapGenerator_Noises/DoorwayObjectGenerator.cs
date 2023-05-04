using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 맵 오브젝트: 출입구 만드는 클래스
/// 3X3 공간
/// 탐색: 왼쪽위 오른쪽 아래
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
        #region DFS식
        //y: max -> 0 (위 -> 아래)
        //for (int y = noiseMapVisualizer.mapHeight; y >= 0 && !isCreate; y--)
        //{
        //    //x는 0 부터 max까지 왼쪽으로 
        //    for (int x = 0; x <= noiseMapVisualizer.mapWidth && !isCreate; x++)
        //    {
        //        isCreate = AccordCondition(new Vector2Int(x, y));      //조건에 맞는거 찾음

        //    }
        //}
        #endregion

        //x축 기준으로 BFS 함
        //문 생성은 왼쪽 상단 기준임
        bool isCreate = false;              //문 생성 확인 플래그
        int x = 0, y = noiseMap.mapHeight;  //왼쪽 상단부터 시작함
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

    //출구는 일단 하나
    private void MakeExit()
    {
        #region DFS식 
        //문 만들면 반복문 종료
        //세로로 탐색함
        //x: max -> 0 (오른쪽 -> 왼쪽)
        //bool isCreate = false;
        //for (int x = noiseMap.mapWidth; x >= 0 && !isCreate; x--)
        //{
        //    //y: 0 -> max (아래 -> 위)
        //    for (int y = 0; y <= noiseMap.mapHeight && !isCreate; y++)
        //    {
        //        isCreate = AccordDoorCreating(new Vector2Int(x, y));      //조건에 맞는거 찾음
        //    }
        //}

        //isCreate = false;
        //for (int y = noiseMap.mapHeight; y >= 0 && !isCreate; y--)
        //{
        //    for (int x = noiseMap.mapWidth; x >= 0 && !isCreate; x--)

        //    {
        //        isCreate = AccordDoorCreating(new Vector2Int(x, y));      //조건에 맞는거 찾음
        //    }
        //}
        #endregion

        //x축 기준으로 BFS 함
        //문 생성은 오른쪽 하단 기준임
        bool isCreate = false;              //문 생성 확인 플래그
        int x = noiseMap.mapWidth, y = 0;  //왼쪽 상단부터 시작함
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
        //현재 위치 x y 값
        int xRound = currentPosition.x;
        int yRound = currentPosition.y;
        
        //맵
        var tilemap = noiseMap.mapTilemap;
        var wall = noiseMap.wallTileBase;

        //지금은 하나씩 탐색하는데 최적화 하려면 currentPosition 값 하나씩 증가 하는게 조건에 맞춰서 해야함
        //□ □ □
        //□ □ □ 모양일때 생성
        //x ■ x

        int spaceCount = 0;
        //테두리 부분은 안찾아도 됨 
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

