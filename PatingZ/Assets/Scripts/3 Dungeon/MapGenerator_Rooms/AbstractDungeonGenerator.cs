/*
 * "추상 클래스"
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class AbstractDungeonGenerator : MonoBehaviour
{
    [SerializeField]
    protected TilemapVisualizer tilemapVisualizer = null;       //타일맵 TilemapVisualizer 참조 가능
    [SerializeField]
    protected Vector2Int startPosition = Vector2Int.zero;      

    public void GenerateDungeon()
    {
        tilemapVisualizer.Clear();
        RunProceduralGeneration();
    }

    
    protected abstract void RunProceduralGeneration() ;
}
