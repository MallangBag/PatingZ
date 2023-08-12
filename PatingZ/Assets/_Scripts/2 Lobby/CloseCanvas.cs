using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseCanvas : MonoBehaviour
{
    public void Close()
    {
        this.GetComponent<Canvas>().enabled = false;
    }
}
