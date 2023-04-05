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
    protected NoiseMapVisualizer noiseMapVisualizer = null;

    public void GenerateMap()
    {
        noiseMapVisualizer.Clear();
        RunProceduralGeneration();
    }

    protected abstract void RunProceduralGeneration() ;
}
