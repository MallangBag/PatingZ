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

    private void Start()
    {
        nomal = this.transform.GetChild(0).GetComponent<Image>();
        mute = this.transform.GetChild(1).GetComponent<Image>();
    }
    public void Enter()
    {
        //���� isOn�� ����-- > ��ũ��Ʈ �����ϰ� isOn�� �ٲ�
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
        //���� isOn�� ���� --> ��ũ��Ʈ �����ϰ� isOn�� �ٲ�
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
