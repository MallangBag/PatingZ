using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseButton : MonoBehaviour
{
    public void Close()
    {
        this.transform.parent.GetComponent<Canvas>().enabled = false;
    }
}
