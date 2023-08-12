using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    [SerializeField]
    private DoorwayObjectGenerator doorwayObjectGenerator;

    void Start()
    {
        InitPosition();
    }

    void Update()
    {

    }


    //�ʱ� ��ġ�� ����
    private void InitPosition()
    {
        int x = doorwayObjectGenerator.entrancePosition.x;
        int y = doorwayObjectGenerator.entrancePosition.y;
        int z = -10;
        this.transform.position = new Vector3(x, y, z);
        Debug.Log($"char1 Pos: {this.transform.position}");
    }
    //�¿�: �⺻ ������ -> �������� �̵�
    //��: 2ĭ �̻� ���̴� �� �ö�
    //��: 5ĭ ���� ���̴� ���� ������ ����


}
