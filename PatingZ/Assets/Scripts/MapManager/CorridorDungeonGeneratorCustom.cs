/*
 * 기존에 있던 CorridorFirstDungeonGenerator.cs에서 커스텀
 * 복도 만드는거 굵기를 랜덤화 함
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorridorDungeonGeneratorCustom : CorridorFirstDungeonGenerator
{
    //[SerializeField]
    //[Range(1, 10)]
    //private int corridorSize = 2;
    //protected override void RunProceduralGeneration()
    //{
    //    CorridorGeneration();
    //}

    //private void CorridorGeneration()
    //{
    //    HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
    //    HashSet<Vector2Int> potentialRoomPositions = new HashSet<Vector2Int>();

    //    CreateCorridors(floorPositions, potentialRoomPositions);    //리뉴얼

    //    HashSet<Vector2Int> roomPositions = CreateRooms(potentialRoomPositions);

    //    List<Vector2Int> deadEnds = FindAllDeadEnds(floorPositions);

    //    CreateRoomsAtDeadEnd(deadEnds, roomPositions);

    //    floorPositions.UnionWith(roomPositions);

    //    tilemapVisualizer.PaintFloorTiles(floorPositions);
    //    WallGenerator.CreateWalls(floorPositions, tilemapVisualizer);
    //}

    //private void CreateCorridors(HashSet<Vector2Int> floorPositions, HashSet<Vector2Int> potentialRoomPositions)
    //{
    //    var currentPosition = startPosition;
    //    potentialRoomPositions.Add(currentPosition);

    //    for (int i = 0; i < corridorCount; i++)
    //    {
    //        for (int j = 0; j < corridorSize; j++)
    //        {
    //            var corridor = ProceduralGenerationAlgorithms.RandomWalkRandomSizeCorridor(currentPosition, corridorLength, corridorSize);
    //            currentPosition = corridor[corridor.Count - 1];
    //            potentialRoomPositions.Add(currentPosition);
    //            floorPositions.UnionWith(corridor);
    //        }
    //    }
    //}
}
