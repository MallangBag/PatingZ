using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleButton : MonoBehaviour
{
    [SerializeField]
    Canvas optionCanvas;

    public void _Start()
    {
        SceneManager.LoadScene("Load");
    }
    public void Option()
    {
        optionCanvas.enabled = true;
    }
    public void Achievements()
    {

    }
    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    
}
