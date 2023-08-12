using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyButtonManager : MonoBehaviour
{
    [SerializeField]
    private Canvas ShopCanvas;


    public void DungeonButton_OnClick()
    {
        SceneManager.LoadScene("NoiseDungeon");
    }

    public void ShopButton_OnClick()
    {
        ShopCanvas.enabled = true;
    }
}
