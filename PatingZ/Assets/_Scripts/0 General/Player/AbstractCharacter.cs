using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//플레이어
//NPC APC 공통
//추상 클래스
public abstract class AbstractCharacter : MonoBehaviour
{
    //생성 위치
    [Header("생성 위치")]
    [SerializeField]
    private Transform genPosition;
}
