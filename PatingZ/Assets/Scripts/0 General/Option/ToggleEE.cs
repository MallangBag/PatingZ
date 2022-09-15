using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// 토글 Event Enter, Exit용 클래스
/// </summary>
public class ToggleEE : MonoBehaviour 
{
    Image nomal;
    Image mute;

    private void Start()
    {
        nomal = this.transform.GetChild(0).GetComponent<Image>();
        mute = this.transform.GetChild(1).GetComponent<Image>();
    }
    public void Enter()
    {
        //현재 isOn이 뭔지-- > 스크립트 실행하고 isOn이 바뀜
        if (this.GetComponent<Toggle>().isOn)
        {
            nomal.color = new Color(0.5f, 0.5f, 0.5f);
        }
        else
        {
            mute.color = new Color(0.5f, 0.5f, 0.5f);
        }
    }
    public void Exit()
    {
        //현재 isOn이 뭔지 --> 스크립트 실행하고 isOn이 바뀜
        if (this.GetComponent<Toggle>().isOn)
        {
            nomal.color = new Color(0f, 0f, 0f);
        }
        else
        {
            mute.color = new Color(0f, 0f, 0f);
        }
    }
}
