using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class OptionManager : MonoBehaviour
{
    [SerializeField]
    GameObject close;


    private void Start()
    {
        Init();
    }


    public void Init()
    {
        this.GetComponent<Canvas>().enabled = false;
    }

    public void Close()
    {
        if(EventSystem.current.currentSelectedGameObject == close)
            this.GetComponent<Canvas>().enabled = false;
    }
}
