/*
 * "추상 클래스"
 * 타일맵 그리는 에디터 유무
 * 시작 좌표 vec2 초기화
 * 타일맵 만드는 함수
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class AbstractNoiseMapGenerator : MonoBehaviour
{
    [SerializeField]
    protected NoiseMapVisualizer noiseMapVisualizer = null;

    public void GenerateMap()
    {
        noiseMapVisualizer.Clear();
        RunProceduralGeneration();
    }

    protected abstract void RunProceduralGeneration() ;
}
