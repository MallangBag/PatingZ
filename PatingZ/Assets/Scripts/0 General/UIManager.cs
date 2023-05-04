using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    Toggle character1;

    Toggle currentCharacter;
    public Toggle CurrentCharacter
    {
        get { return currentCharacter; }
    }



    //��ū�� ������ 1���� �־�� ��
    public bool CharacterToggleState()
    {
        return character1.GetComponent<Toggle>().isOn;
    }
}
