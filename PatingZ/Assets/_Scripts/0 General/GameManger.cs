using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//게임 돌아가는 걸로 작성

public class GameManger : MonoBehaviour
{
    [SerializeField]
    NoiseMapGenerator noiseMapGenerator;
    [SerializeField]
    DoorwayObjectGenerator doorwayObjectGenerator;
    [SerializeField]
    Camera mainCamera;
    [SerializeField]
    UIManager uiManager;


    private void Start()
    {
        GenerateMap();
        InitCameraPosition();

    }
    private void Update()
    {
        //캐릭터 버튼 on이면 해당 위치로 카메라 이동함
        if(uiManager.CharacterToggleState())
        {

        }
        else
        {

        }

    }



    //맵생성
    void GenerateMap()
    {
        noiseMapGenerator.GenerateMap();
        doorwayObjectGenerator.GenerateMap();
    }
    //초기 카메라 위치
    void InitCameraPosition()
    {
        int x = doorwayObjectGenerator.entrancePosition.x;
        int y = doorwayObjectGenerator.entrancePosition.y;
        int z = -10;
        mainCamera.transform.position = new Vector3(x, y, z);
    }
}
