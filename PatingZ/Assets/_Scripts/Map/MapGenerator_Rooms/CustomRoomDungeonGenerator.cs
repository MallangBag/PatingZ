using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CustomRoomDungeonGenerator : SimpleRandomWalkDungeonGenerator
{
    [SerializeField]
    private int minRoomWidth = 40, minRoomHeight = 20;
    [SerializeField]
    private int dungeonWidth = 100, dungeonHeight = 100;
    [SerializeField]
    [Range(0, 10)]
    private int offset = 1;
    [SerializeField]
    private bool randomWalkRooms = false;
    protected override void RunProceduralGeneration()
    {
        CreateRooms();
    }


    //방 만듦
    private void CreateRooms()
    {
        //생성위치: startPosition, 던전크기: BoundsInt(dungeonWidth, dungeonHeight), 룸사이즈: (minRoomWidth, minRoomHeight)
        var roomsList = ProceduralGenerationAlgorithms.BinarySpacePartitioning(new BoundsInt((Vector3Int)startPosition, new Vector3Int(dungeonWidth, dungeonHeight, 0)), minRoomWidth, minRoomHeight);
        ////Debug.Log(ProceduralGenerationAlgorithms.currentRoomCount);

        //room 만들기
        HashSet<Vector2Int> floor = new();
        if(randomWalkRooms)
        {
            floor = CreateRoomsRandomly(roomsList);
        }
        else
        {
            floor = CreateRoomsNormally(roomsList);
        }

        //방의 기준점 생성 및 정렬
        List<Vector2Int> roomPoints = new();
        foreach (var room in roomsList)
        {
            roomPoints.Add((Vector2Int)Vector3Int.RoundToInt(room.center));
            ////Debug.Log("x: "+ room.center.x + " y" + room.center.y);
        }
        roomPoints = RoomPointsSort(roomPoints);

        //복도 생성
        HashSet<Vector2Int> corridors = CennectRooms(roomPoints);
        floor.UnionWith(corridors);

        //맵 그리기
        tilemapVisualizer.PaintFloorTiles(floor);
    }
    #region room 만들기
    private HashSet<Vector2Int> CreateRoomsRandomly(List<BoundsInt> roomsList)
    {
        HashSet<Vector2Int> floor = new();
        for (int i = 0; i < roomsList.Count; i++)
        {
            var roomBounds = roomsList[i];
            var roomCenter = new Vector2Int(Mathf.RoundToInt(roomBounds.center.x), Mathf.RoundToInt(roomBounds.center.y));
            var roomFloor = RunRandomWalk(randomWalkParameters, roomCenter);
            foreach (var position in roomFloor)
            {
                if ((roomBounds.xMin + offset) <= position.x && position.x <= (roomBounds.xMax - offset)
                    && (roomBounds.yMin) <= position.y && position.y <= (roomBounds.yMax - offset))
                {
                    floor.Add(position);
                }
            }
        }
        return floor;
    }
    private HashSet<Vector2Int> CreateRoomsNormally(List<BoundsInt> roomsList)
    {
        HashSet<Vector2Int> floor = new();
        foreach (var room in roomsList)
        {
            for (int col = 0; col < room.size.x - offset; col++)
            {
                for (int row = 0; row < room.size.y - offset; row++)
                {
                    Vector2Int position = (Vector2Int)room.min + new Vector2Int(col, row);
                    floor.Add(position);
                }
            }
        }
        return floor;
    }
    #endregion

    #region 방 정렬
    private List<Vector2Int> RoomPointsSort(List<Vector2Int> roomPoints)
    {
        //람다식; 익명함수 생성하고 delegate를 매개변수로 받음
        //x는 오름차순(왼쪽부터)
        //y는 내림차순(위부터)
        //행 우선 (y축 내림차순 우선)
        roomPoints.Sort((v1, v2) =>
        {
            int compareResult = v2.y.CompareTo(v1.y);    //결과값에 따라 양수(v1>v2) 음수(v1<v2) 0(v1=v2) 으로 반환
            if (compareResult != 0)
            {
                return compareResult;
            }
            else
            {
                return v1.x.CompareTo(v2.x);
            }

        });
        return roomPoints;
    }
    #endregion

    #region 복도 생성
    private HashSet<Vector2Int> CennectRooms(List<Vector2Int> roomPoints)
    {
        Debug.Log("CennectRooms");
        HashSet<Vector2Int> corridors = new();
        var currentPoint = roomPoints[0];
        roomPoints.RemoveAt(0);
        while (roomPoints.Count > 0)
        {
            //가까운곳 2지점 찾아서 랜덤으로 연결
            Vector2Int[] closes = FindCloseTwoPoints(currentPoint, roomPoints);
            Vector2Int nextPoint = closes[Random.Range(0, 2)];

            //복도 생성
            HashSet<Vector2Int> newCorridor = CreateCorridor(currentPoint, nextPoint);
            corridors.UnionWith(newCorridor);

            //다음 방으로 이동
            currentPoint = nextPoint;
            roomPoints.Remove(currentPoint);
        }
        return corridors;
    }

    private Vector2Int[] FindCloseTwoPoints(Vector2Int currentPoint, List<Vector2Int> roomPoints)
    {
        Debug.Log("FindCloseTwoPoints");
        Vector2Int[] closest = new Vector2Int[2];
        float distance1 = float.MaxValue;
        float distance2 = float.MaxValue;
        if (roomPoints.Count > 1)
        {
            foreach (var position in roomPoints)
            {
                float currentDistance = Vector2.Distance(currentPoint, position);

                //첫번째보다 작은지
                if (currentDistance < distance1)
                {
                    distance2 = distance1;
                    distance1 = currentDistance;
                    closest[1] = closest[0];
                    closest[0] = position;
                }
                //두번째부다 작은지
                else if (currentDistance < distance2)
                {
                    distance2 = currentDistance;
                    closest[1] = position;
                }
            }
        }
        else
        {
            closest[0] = roomPoints[0];
            closest[1] = roomPoints[0];
        }
        return closest;
    }
    private HashSet<Vector2Int> CreateCorridor(Vector2Int currentPosition, Vector2Int destination)
    {
        Debug.Log("CreateCorridor");

        HashSet<Vector2Int> corridor = new();
        corridor.Add(currentPosition);

        //목표까지 거리
        int dx = Mathf.Abs(destination.x - currentPosition.x);
        int dy = Mathf.Abs(destination.y - currentPosition.y);

        //방향
        int xDir = destination.x - currentPosition.x > 0 ? 1 : -1;
        int yDir = destination.y - currentPosition.y > 0 ? 1 : -1;

        ////랜덤범위 정하기
        //Random 오차 내: range 미만
        int rangeExtend = 3;
        //x축 확장(가로로 확장)
        int expandMinX = (int)minRoomWidth / (5 * 2);
        int expandMaxX = expandMinX + 2;//(int)minRoomWidth / 5;
        //y축 확장(세로로 확장)
        int expandMinY = (int)minRoomHeight / 4;
        int expandMaxY = expandMinY + 2;//(int)minRoomHeight / 5;
        //통로 범위 확장 시킬 범위
        int expandX;
        int expandY;

        //현위치(position)과 x y의 거리가 있다면: 한쪽이 먼저 도착할때까지
        while (dx > rangeExtend && dy > rangeExtend)
        {
            int extendX = Random.Range(1, rangeExtend);
            int extendY = Random.Range(1, rangeExtend);
            expandX = Random.Range(expandMinX, expandMaxX);
            expandY = Random.Range(expandMinY, expandMaxY);

            //목표x에 방향(xDir),거리(randomX)만큼 다가감
            if (dx > rangeExtend)
            {
                for (int i = 0; i < extendX; i++)
                {
                    currentPosition += Vector2Int.right * xDir;
                    dx = Mathf.Abs(destination.x - currentPosition.x);
                    corridor.Add(currentPosition);
                    for (int j = -expandY + 1;  j < expandY; j++)
                    {
                        Vector2Int expandPosition = currentPosition + (Vector2Int.up * j);
                        corridor.Add(expandPosition);
                    }
                }
            }
            //목표y에 다가감
            if (dy > rangeExtend)
            {
                for (int i = 0; i < extendY; i++)
                {
                    currentPosition += Vector2Int.up * yDir;
                    dy = Mathf.Abs(destination.y - currentPosition.y);
                    corridor.Add(currentPosition);
                    for (int j = -expandX + 1; j < expandX; j++)
                    {
                        Vector2Int expandPosition = currentPosition + (Vector2Int.right * j);
                        corridor.Add(expandPosition);
                    }
                }
            }

        }

        //목표x에 도달했다면 남은 y처리
        if (dx <= rangeExtend)
        {
            while (dy > rangeExtend)
            {
                expandX = Random.Range(expandMinX, expandMaxX);
                currentPosition += Vector2Int.up * yDir;
                dy = Mathf.Abs(destination.y - currentPosition.y);
                corridor.Add(currentPosition);
                for (int j = -expandX + 1; j < expandX; j++)
                {
                    Vector2Int expandPosition = currentPosition + (Vector2Int.right * j);
                    corridor.Add(expandPosition);
                }
            }
            
        }

        //y목표에 도달했다면 남은 x처리
        if (dy <= rangeExtend)
        {
            while (dx > rangeExtend)
            {
                expandY = Random.Range(expandMinY, expandMaxY);
                currentPosition += Vector2Int.right * xDir;
                dx = Mathf.Abs(destination.x - currentPosition.x);
                corridor.Add(currentPosition);
                for (int j = -expandY + 1; j < expandY; j++)
                {
                    Vector2Int expandPosition = currentPosition + (Vector2Int.up * j);
                    corridor.Add(expandPosition);
                }
            }
            
        }

        return corridor;
    }
    #endregion



}
