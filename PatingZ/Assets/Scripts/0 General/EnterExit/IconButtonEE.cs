using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconButtonEE : MonoBehaviour
{
    public void Enter()
    {
        this.GetComponent < Outline>().effectColor = new Color(0.5f, 0.5f, 0.5f);
    }
    public void Exit()
    {
        this.GetComponent<Outline>().effectColor = new Color(1.0f, 1.0f, 1.0f);
    }
}
