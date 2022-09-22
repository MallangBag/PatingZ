using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TitleButtonEE : MonoBehaviour
{
    public void Enter()
    {
        this.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color(0.5f, 0.5f, 0.5f);
    }
    public void Exit()
    {
        this.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color(0f, 0f, 0f);
    }
}
