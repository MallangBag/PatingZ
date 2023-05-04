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


    //�� ����
    private void CreateRooms()
    {
        //������ġ: startPosition, ����ũ��: BoundsInt(dungeonWidth, dungeonHeight), �������: (minRoomWidth, minRoomHeight)
        var roomsList = ProceduralGenerationAlgorithms.BinarySpacePartitioning(new BoundsInt((Vector3Int)startPosition, new Vector3Int(dungeonWidth, dungeonHeight, 0)), minRoomWidth, minRoomHeight);
        ////Debug.Log(ProceduralGenerationAlgorithms.currentRoomCount);

        //room �����
        HashSet<Vector2Int> floor = new();
        if(randomWalkRooms)
        {
            floor = CreateRoomsRandomly(roomsList);
        }
        else
        {
            floor = CreateRoomsNormally(roomsList);
        }

        //���� ������ ���� �� ����
        List<Vector2Int> roomPoints = new();
        foreach (var room in roomsList)
        {
            roomPoints.Add((Vector2Int)Vector3Int.RoundToInt(room.center));
            ////Debug.Log("x: "+ room.center.x + " y" + room.center.y);
        }
        roomPoints = RoomPointsSort(roomPoints);

        //���� ����
        HashSet<Vector2Int> corridors = CennectRooms(roomPoints);
        floor.UnionWith(corridors);

        //�� �׸���
        tilemapVisualizer.PaintFloorTiles(floor);
    }
    #region room �����
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

    #region �� ����
    private List<Vector2Int> RoomPointsSort(List<Vector2Int> roomPoints)
    {
        //���ٽ�; �͸��Լ� �����ϰ� delegate�� �Ű������� ����
        //x�� ��������(���ʺ���)
        //y�� ��������(������)
        //�� �켱 (y�� �������� �켱)
        roomPoints.Sort((v1, v2) =>
        {
            int compareResult = v2.y.CompareTo(v1.y);    //������� ���� ���(v1>v2) ����(v1<v2) 0(v1=v2) ���� ��ȯ
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

    #region ���� ����
    private HashSet<Vector2Int> CennectRooms(List<Vector2Int> roomPoints)
    {
        Debug.Log("CennectRooms");
        HashSet<Vector2Int> corridors = new();
        var currentPoint = roomPoints[0];
        roomPoints.RemoveAt(0);
        while (roomPoints.Count > 0)
        {
            //������ 2���� ã�Ƽ� �������� ����
            Vector2Int[] closes = FindCloseTwoPoints(currentPoint, roomPoints);
            Vector2Int nextPoint = closes[Random.Range(0, 2)];

            //���� ����
            HashSet<Vector2Int> newCorridor = CreateCorridor(currentPoint, nextPoint);
            corridors.UnionWith(newCorridor);

            //���� ������ �̵�
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

                //ù��°���� ������
                if (currentDistance < distance1)
                {
                    distance2 = distance1;
                    distance1 = currentDistance;
                    closest[1] = closest[0];
                    closest[0] = position;
                }
                //�ι�°�δ� ������
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

        //��ǥ���� �Ÿ�
        int dx = Mathf.Abs(destination.x - currentPosition.x);
        int dy = Mathf.Abs(destination.y - currentPosition.y);

        //����
        int xDir = destination.x - currentPosition.x > 0 ? 1 : -1;
        int yDir = destination.y - currentPosition.y > 0 ? 1 : -1;

        ////�������� ���ϱ�
        //Random ���� ��: range �̸�
        int rangeExtend = 3;
        //x�� Ȯ��(���η� Ȯ��)
        int expandMinX = (int)minRoomWidth / (5 * 2);
        int expandMaxX = expandMinX + 2;//(int)minRoomWidth / 5;
        //y�� Ȯ��(���η� Ȯ��)
        int expandMinY = (int)minRoomHeight / 4;
        int expandMaxY = expandMinY + 2;//(int)minRoomHeight / 5;
        //��� ���� Ȯ�� ��ų ����
        int expandX;
        int expandY;

        //����ġ(position)�� x y�� �Ÿ��� �ִٸ�: ������ ���� �����Ҷ�����
        while (dx > rangeExtend && dy > rangeExtend)
        {
            int extendX = Random.Range(1, rangeExtend);
            int extendY = Random.Range(1, rangeExtend);
            expandX = Random.Range(expandMinX, expandMaxX);
            expandY = Random.Range(expandMinY, expandMaxY);

            //��ǥx�� ����(xDir),�Ÿ�(randomX)��ŭ �ٰ���
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
            //��ǥy�� �ٰ���
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

        //��ǥx�� �����ߴٸ� ���� yó��
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

        //y��ǥ�� �����ߴٸ� ���� xó��
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
