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


    //초기 위치값 설정
    private void InitPosition()
    {
        int x = doorwayObjectGenerator.entrancePosition.x;
        int y = doorwayObjectGenerator.entrancePosition.y;
        int z = -10;
        this.transform.position = new Vector3(x, y, z);
        Debug.Log($"char1 Pos: {this.transform.position}");
    }
    //좌우: 기본 생성시 -> 방향으로 이동
    //상: 2칸 이상 높이는 못 올라감
    //하: 5칸 이하 높이는 낙하 데미지 받음


}
