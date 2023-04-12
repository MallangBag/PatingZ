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
        foreach (var position in roomPoints)
        {
            float currentDistance = Vector2.Distance(currentPoint, position);

            //첫번째보다 작은지
            if(currentDistance < distance1)
            {
                distance2 = distance1;
                distance1 = currentDistance;
                closest[1] = closest[0];
                closest[0] = position;
            }
            //두번째부다 작은지
            else if(currentDistance < distance2)
            {
                distance2 = currentDistance;
                closest[1] = position;
            }
        }
        return closest;
    }
    private HashSet<Vector2Int> CreateCorridor(Vector2Int currentPoint, Vector2Int destination)
    {
        Debug.Log("CreateCorridor");

        HashSet<Vector2Int> corridor = new();
        var position = currentPoint;
        corridor.Add(position);

        //목표까지 거리
        int dx = destination.x - currentPoint.x; 
        int dy = destination.y - currentPoint.y;

        //방향
        int xDir = dx > 0 ? 1 : -1;
        int yDir = dy > 0 ? 1 : -1;

        int x = 0, y = 0;

        //x값만 급경사 생기게
        while (position.x != destination.x || position.y != destination.y)
        {
            int randomX = Random.Range(0, 3);
            int randomY = Random.Range(0, 3);
            if (Mathf.Abs(x + xDir * randomX) <= Mathf.Abs(dx))
            {
                x += (xDir * randomX);
            }
            if (Mathf.Abs(y + yDir * randomY) <= Mathf.Abs(dy))
            {
                y += (yDir * randomY);
            }
            position += new Vector2Int(x, y);
            corridor.Add(position);
            Debug.Log("경사: " + position);
        }

        //대각선으로 x 또는 y의 위치가 같아졌으면 평지로
        while (position.x != destination.x)
        {
            position.x = destination.x;
            position += new Vector2Int(xDir, 0);
            corridor.Add(position);
        }

        while (position.y != destination.y)
        {
            position.y = destination.y;
            position += new Vector2Int(0, yDir);
            corridor.Add(position);
        }

        return corridor;
    }
    #endregion



}
