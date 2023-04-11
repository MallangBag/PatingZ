using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomRoomDungeonGenerator : SimpleRandomWalkDungeonGenerator
{
    [SerializeField]
    private int minRoomWidth = 4, minRoomHeight = 2;
    [SerializeField]
    private int dungeonWidth = 20, dungeonHeight = 20;
    [SerializeField]
    [Range(0, 10)]
    private int offset = 1;
    [SerializeField]
    private bool randomWalkRooms = false;

    [SerializeField]
    [Range(1, 7)]
    private int splitXPoint = 4;
    [SerializeField]
    [Range(1, 5)]
    private int splitYPoint = 2;

    protected override void RunProceduralGeneration()
    {
        CreateRooms();
    }


    //�� ����
    private void CreateRooms()
    {
        //������ġ: startPosition, ����ũ��: BoundsInt(dungeonWidth, dungeonHeight), �������: (minRoomWidth, minRoomHeight)
        var roomsList = ProceduralGenerationAlgorithms.BinarySpacePartitioning(new BoundsInt((Vector3Int)startPosition, new Vector3Int(dungeonWidth, dungeonHeight, 0)), minRoomWidth, minRoomHeight);
        Debug.Log(ProceduralGenerationAlgorithms.currentRoomCount);

        //��� ������� ����
        HashSet<Vector2Int> floor = new();
        if(randomWalkRooms)
        {
            floor = CreateRoomsRandomly(roomsList);
        }
        else
        {
            floor = CreateRoomsNormally(roomsList);
        }

        //���� �������� ��� ���� ����
        List<Vector2Int> roomPoints = new();
        foreach (var room in roomsList)
        {
            roomPoints.Add((new Vector2Int(Mathf.RoundToInt(room.x / splitXPoint), Mathf.RoundToInt(room.y / splitYPoint))));
        }
        HashSet<Vector2Int> corridors = CennectRooms(roomPoints);

    }


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
                if((roomBounds.xMin + offset) <= position.x && position.x <= (roomBounds.xMax - offset)
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

    private HashSet<Vector2Int> CennectRooms(List<Vector2Int> roomPoints)
    {
        HashSet<Vector2> corridors = new();
        var currentRoomPoint = roomPoints;

        return null;
    }

}
