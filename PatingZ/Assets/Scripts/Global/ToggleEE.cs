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
    bool isOn;

    private void Start()
    {
        nomal = this.transform.GetChild(0).GetComponent<Image>();
        mute = this.transform.GetChild(1).GetComponent<Image>();
        isOn = this.GetComponent<Toggle>().isOn;
    }
    public void Toggle()
    {
        isOn = this.GetComponent<Toggle>().isOn;
        //현재 isOn이 뭔지 --> 스크립트 실행하고 isOn이 바뀜
        if (isOn)
        {
            nomal.enabled = true;
            mute.enabled = false;
        }
        else
        {
            nomal.enabled = false;
            mute.enabled = true;
        }
    }



    public void Enter()
    {
        isOn = this.GetComponent<Toggle>().isOn;
        Debug.Log("Enter");
        //현재 isOn이 뭔지-- > 스크립트 실행하고 isOn이 바뀜
        if (isOn)
        {
            Debug.Log("nomal Enter");
            nomal.color = new Color(0.5f, 0.5f, 0.5f);
        }
        else
        {
            Debug.Log("mute Enter");
            mute.color = new Color(0.5f, 0.5f, 0.5f);
        }
    }
    public void Exit()
    {
        isOn = this.GetComponent<Toggle>().isOn;
        Debug.Log("Exit");
        //현재 isOn이 뭔지 --> 스크립트 실행하고 isOn이 바뀜
        if (isOn)
        {
            nomal.color = new Color(0f, 0f, 0f);
        }
        else
        {
            mute.color = new Color(0f, 0f, 0f);
        }
    }
}
