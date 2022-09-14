using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// ��� Event Enter, Exit�� Ŭ����
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
        //���� isOn�� ���� --> ��ũ��Ʈ �����ϰ� isOn�� �ٲ�
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
        //���� isOn�� ����-- > ��ũ��Ʈ �����ϰ� isOn�� �ٲ�
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
        //���� isOn�� ���� --> ��ũ��Ʈ �����ϰ� isOn�� �ٲ�
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
