using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public static class ProceduralGenerationAlgorithms
{
    public static HashSet<Vector2Int> SimpleRandomWalk(Vector2Int startPosition, int walkLength)
    {
        HashSet<Vector2Int> path = new HashSet<Vector2Int>();

        path.Add(startPosition);
        var previousPosition = startPosition;
        for (int i = 0; i < walkLength; i++)
        {
            var newPosition = previousPosition + Direction2D.GetRandomCardinalDirection();
            path.Add(newPosition);
            previousPosition = newPosition;
        }
        return path;
    }
    //랜덤 모양으로 (길이는 고정) 복도를 생성함
    public static List<Vector2Int> RandomWalkCorridor(Vector2Int startPosition, int corridorLength)
    {
        List<Vector2Int> corridor = new List<Vector2Int>();
        var direction = Direction2D.GetRandomCardinalDirection();
        var currentPosition = startPosition;
        corridor.Add(currentPosition);

        for (int i = 0; i < corridorLength; i++)
        {
            currentPosition += direction;
            corridor.Add(currentPosition);
        }
        return corridor;
    }

    public static int currentRoomCount;

    /// <summary>
    /// 받은 범위 분할 함
    /// </summary>
    /// <param name="spaceToSplit"> 맵 전역 범위 </param>
    /// <param name="minWidth">방: x축</param>
    /// <param name="minHeight">방: y축</param>
    /// <returns></returns>
    public static List<BoundsInt> BinarySpacePartitioning(BoundsInt spaceToSplit, int minWidth, int minHeight)
    {
        currentRoomCount = new();
        Queue<BoundsInt> roomsQueue = new();   //분할한 구역
        List<BoundsInt> roomsList = new();      //분할한 범위 결과값
        roomsQueue.Enqueue(spaceToSplit);
        while (roomsQueue.Count > 0)
        {
            var room = roomsQueue.Dequeue();
            //현재 구역이 xy둘다 최소 값보다 크다면 나누기
            if (room.size.y >= minHeight && room.size.x >= minWidth)
            {
                //현재 구역의 x y가 최소치의 2배 이상이면 구역 가르기
                if (Random.value < 0.5f)
                {
                    //높이 먼저 2배인거 확인
                    if(room.size.y >= minHeight * 2){
                        SplitHorizontally(minHeight, roomsQueue, room);
                    }
                    else if(room.size.x >= minWidth * 2){
                        SplitVertically(minWidth, roomsQueue, room);
                    }
                    //현재 구역의 높이 넓이가 2배가 아닐시 지금 구역은 결과값
                    else if (room.size.x >= minWidth && room.size.y >= minHeight){
                        roomsList.Add(room);
                        currentRoomCount++;
                    }
                }
                else
                {
                    //넓이 먼저 2배인거 확인
                    if (room.size.x >= minWidth * 2)
                    {
                        SplitVertically(minWidth, roomsQueue, room);
                    }
                    else if (room.size.y >= minHeight * 2)
                    {
                        SplitHorizontally(minHeight, roomsQueue, room);
                    }
                    //현재 구역의 높이 넓이가 2배가 아닐시 지금 구역은 결과값
                    else if (room.size.x >= minWidth && room.size.y >= minHeight)
                    {
                        roomsList.Add(room);
                        currentRoomCount++;
                    }
                }
            }
        }
        return roomsList;
    }

    //세로 방향으로 가름 (x축 가르기)
    private static void SplitVertically(int minWidth, Queue<BoundsInt> roomsQueue, BoundsInt room)
    {
        var xSplit = Random.Range(1, room.size.x);  // 1 <= value < room.size.x : Random.Range(int, int)

        //room의 x축을 xSplit에 맞춰서 가름
        //room1은 min의 시작지점에서 xSplit만큼의 크기
        //room2는 (min + xSplit)부터 시작하는 room.size.x - xSplit 만큼의 크기
        BoundsInt room1 = new BoundsInt(room.min, new Vector3Int(xSplit, room.size.y, room.size.z));
        BoundsInt room2 = new BoundsInt(new Vector3Int(room.min.x + xSplit, room.min.y, room.min.z),
            new Vector3Int(room.size.x - xSplit, room.size.y, room.size.z));
        roomsQueue.Enqueue(room1);
        roomsQueue.Enqueue(room2);
    }
    //가로 방향으로 가름 (y축 가르기)
    private static void SplitHorizontally(int minHeight, Queue<BoundsInt> roomsQueue, BoundsInt room)
    {
        var ySplit = Random.Range(1, room.size.y);  //(minHeight, room.size.y - minHeight)
        BoundsInt room1 = new BoundsInt(room.min, new Vector3Int(room.size.x, ySplit, room.size.z));
        BoundsInt room2 = new BoundsInt(new Vector3Int(room.min.x, room.min.y + ySplit, room.min.z),
            new Vector3Int(room.size.x, room.size.y - ySplit, room.size.z));
        roomsQueue.Enqueue(room1);
        roomsQueue.Enqueue(room2);
    }
}

public static class Direction2D
{
    public static List<Vector2Int> cardinalDirectionsList = new()
    {
        new Vector2Int(0,1),    //UP
        new Vector2Int(1,0),    //RIGHT
        new Vector2Int(0,-1),   //DOWN
        new Vector2Int(-1,0),   //LEFT
    };

    public static List<Vector2Int> diagonalDirectionsList = new()
    {
        new Vector2Int(1, 1),   //UP-RIGHT
        new Vector2Int(1, -1),  //RIGHT-DOWN
        new Vector2Int(-1, -1), //DOWN-LEFT
        new Vector2Int(-1, 1),  //LEFT-UP
    };

    public static List<Vector2Int> eightDirectionsList = new()
    {
        new Vector2Int(0, 1),   //UP
        new Vector2Int(1, 1),   //UP-RIGHT
        new Vector2Int(1, 0),   //RIGHT
        new Vector2Int(1, -1),  //RIGHT-DOWN
        new Vector2Int(0, -1),  //DOWN
        new Vector2Int(-1, -1), //DOWN-LEFT
        new Vector2Int(-1, 0),  //LEFT
        new Vector2Int(-1, 1),  //LEFT-UP

    };

    public static Vector2Int GetRandomCardinalDirection()
    {
        return cardinalDirectionsList[Random.Range(0, cardinalDirectionsList.Count)];
    }
}