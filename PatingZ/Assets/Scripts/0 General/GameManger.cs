using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//���� ���ư��� �ɷ� �ۼ�

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
        //ĳ���� ��ư on�̸� �ش� ��ġ�� ī�޶� �̵���
        if(uiManager.CharacterToggleState())
        {

        }
        else
        {

        }

    }



    //�ʻ���
    void GenerateMap()
    {
        noiseMapGenerator.GenerateMap();
        doorwayObjectGenerator.GenerateMap();
    }
    //�ʱ� ī�޶� ��ġ
    void InitCameraPosition()
    {
        int x = doorwayObjectGenerator.entrancePosition.x;
        int y = doorwayObjectGenerator.entrancePosition.y;
        int z = -10;
        mainCamera.transform.position = new Vector3(x, y, z);
    }
}
