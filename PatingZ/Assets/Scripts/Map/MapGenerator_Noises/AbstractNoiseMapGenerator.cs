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
    protected NoiseMapVisualizer noiseMap = null;

    //맵 초기화 후 생성; 타일맵은 타일 겹쳐서 깔아짐
    public void GenerateMap()
    {
        RunProceduralGeneration();
    }

    //맵초기화
    public void ClearMap()
    {
        noiseMap.Clear();
    }

    protected abstract void RunProceduralGeneration();
}
