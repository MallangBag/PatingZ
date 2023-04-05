/*
 * "추상 클래스"
 * 타일맵 그리는 에디터 유무
 * 시작 좌표 vec2 초기화
 * 타일맵 만드는 함수
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class AbstractDungeonGenerator : MonoBehaviour
{
    [SerializeField]
    protected TilemapVisualizer tilemapVisualizer = null;
    [SerializeField]
    protected Vector2Int startPosition = Vector2Int.zero;

    public void GenerateDungeon()
    {
        tilemapVisualizer.Clear();
        RunProceduralGeneration();
    }

    
    protected abstract void RunProceduralGeneration() ;
}
