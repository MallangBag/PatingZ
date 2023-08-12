/*
 * "�߻� Ŭ����"
 * Ÿ�ϸ� �׸��� ������ ����
 * ���� ��ǥ vec2 �ʱ�ȭ
 * Ÿ�ϸ� ����� �Լ�
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractNoiseMapGenerator : MonoBehaviour
{
    [SerializeField]
    protected NoiseMapVisualizer noiseMap = null;

    //�� �ʱ�ȭ �� ����; Ÿ�ϸ��� Ÿ�� ���ļ� �����
    public void GenerateMap()
    {
        RunProceduralGeneration();
    }

    //���ʱ�ȭ
    public void ClearMap()
    {
        noiseMap.Clear();
    }

    protected abstract void RunProceduralGeneration();
}
