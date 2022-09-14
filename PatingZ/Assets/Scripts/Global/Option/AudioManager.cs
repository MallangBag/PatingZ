using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;


public class AudioManager : MonoBehaviour
{
    #region ����
    [Header("--- ����� ---")]
    [SerializeField]
    AudioMixer sound;

    [Header("--- MASTER ---")]
    [SerializeField]
    Slider masterSlider;
    [SerializeField]
    Toggle masterToggle;


    [Header("--- BGM ---")]
    [SerializeField]
    Slider bgmSlider;
    [SerializeField]
    Toggle bgmToggle;

    [Header("--- SFX ---")]
    [SerializeField]
    Slider sfxSlider;
    [SerializeField]
    Toggle sfxToggle;

    bool isOn;      //��� isOn Ȯ�ο�
    #endregion

    #region ����Ƽ �Լ�
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)     //�� ���۽� �ҷ�����
    {
        DelayedUpdateVolume();
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Start()
    {
        
    }
    #endregion

    #region �Լ�

    async void DelayedUpdateVolume()
    {
        await Task.Delay(1);
        UpdateVolume();
    }
    void UpdateVolume()     //AudioMixer�� SetFloat�� ����� �۵� ���ؼ� ������ �ְ� ���� �ǰ� �ؾ���
    {
        //Load
        //�����̴� �� �ҷ�����
        masterSlider.value = PlayerPrefs.GetFloat("Master" + "sound");
        bgmSlider.value = PlayerPrefs.GetFloat("BGM" + "sound");
        sfxSlider.value = PlayerPrefs.GetFloat("SFX" + "sound");

        AudioControl("Master", masterSlider, masterToggle);
        AudioControl("BGM", bgmSlider, bgmToggle);
        AudioControl("SFX", sfxSlider, sfxToggle);

        //��� �� �ҷ�����
        masterToggle.isOn = System.Convert.ToBoolean(PlayerPrefs.GetInt("Master" + "toggle"));
        bgmToggle.isOn = System.Convert.ToBoolean(PlayerPrefs.GetInt("BGM" + "toggle"));
        sfxToggle.isOn = System.Convert.ToBoolean(PlayerPrefs.GetInt("SFX" + "toggle"));

        Toggle("Master", masterSlider, masterToggle);
        Toggle("BGM", bgmSlider, bgmToggle);
        Toggle("SFX", sfxSlider, sfxToggle);
    }
    //����� �����̴� �Լ�
    public void AudioControl(string str, Slider slider, Toggle toggle)
    {
        //�����̴� �Ҹ� ����
        float volume = slider.value;

        //���ҰŰ� �ƴҽÿ���
        if (!toggle.isOn)
        {
            if (volume == -40f)
            {
                sound.SetFloat(str, -80);     //�Ҹ� �ʹ� ũ�� ������ �ŷ��� ����
            }
            else
            {
                sound.SetFloat(str, volume);
            }
        }
        PlayerPrefs.SetFloat(str + "sound", slider.value);       //Save
    }
    //����� ��� �Լ�
    public void Toggle(string str, Slider slider, Toggle toggle)
    {
        isOn = toggle.GetComponent<Toggle>().isOn;      //���Ұ� on
        if (isOn)
        {
            if (str == "Master")
            {
                sound.FindMatchingGroups("Master")[0].audioMixer.SetFloat(str, -80f);
            }
            else if (str == "BGM")
            {
                sound.FindMatchingGroups("Master")[1].audioMixer.SetFloat(str, -80f);
            }
            else if (str == "SFX")
            {
                sound.FindMatchingGroups("Master")[2].audioMixer.SetFloat(str, -80f);
            }
        }
        else
        {
            AudioControl(str, slider, toggle);
        }
        PlayerPrefs.SetInt(str + "toggle", System.Convert.ToInt32(toggle.isOn));     //Save
    }
    #endregion
}
