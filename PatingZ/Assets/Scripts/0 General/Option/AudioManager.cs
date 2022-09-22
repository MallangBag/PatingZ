using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.EventSystems;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Threading.Tasks;


public class AudioManager : MonoBehaviour
{
    #region ����
    [Header("---- Audio Mixer ----")]
    public AudioMixer sound;        //����� ���� �ͼ�
    [Header("---- Master ----")]
    //������
    public Slider masterSlider;
    public Toggle masterToggle;
    [Header("---- BGM ----")]
    //BGM
    public Slider bgmSlider;
    public Toggle bgmToggle;
    [Header("---- SFX ----")]
    //SFX
    public Slider sfxSlider;
    public Toggle sfxToggle;
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
    async void DelayedUpdateVolume()
    {
        await Task.Delay(1);
        UpdateVolume();
    }


    private void Start()
    {
        Init();

        //�����̴� ��� ����
        //�����̴�
        masterSlider.onValueChanged.AddListener(delegate { AudioControl("Master", masterSlider, masterToggle); });
        bgmSlider.onValueChanged.AddListener(delegate { AudioControl("BGM", bgmSlider, bgmToggle); });
        sfxSlider.onValueChanged.AddListener(delegate { AudioControl("SFX", sfxSlider, sfxToggle); });

        //���
        masterToggle.GetComponent<Toggle>().onValueChanged.AddListener(delegate { Toggle("Master", masterSlider, masterToggle); });
        bgmToggle.GetComponent<Toggle>().onValueChanged.AddListener(delegate { Toggle("BGM", bgmSlider, bgmToggle); });
        sfxToggle.GetComponent<Toggle>().onValueChanged.AddListener(delegate { Toggle("SFX", sfxSlider, sfxToggle); });
    }

    #endregion
    #region �Լ�
    #region ������
    public void Init()
    {
        this.GetComponent<Canvas>().enabled = false;
    }
    #endregion
    #region ����� ������Ʈ
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
    #endregion
    #region ����� ����
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
        //SliderVolumeText(slider, volume);
        PlayerPrefs.SetFloat(str + "sound", slider.value);       //Save
    }

    //�����̴� �ؽ�Ʈ�� �� �ѱ�� �Լ�
    public void SliderVolumeText(Slider slider, float volume)
    {
        slider.transform.GetChild(2).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text
            = ((int)((volume + 40) / 40 * 100)).ToString();
    }

    //����� ��� �Լ�
    public void Toggle(string str, Slider slider, Toggle toggle)
    {
        if (toggle.GetComponent<Toggle>().isOn)
        {
            toggle.transform.GetChild(0).GetComponent<Image>().enabled = true;
            toggle.transform.GetChild(1).GetComponent<Image>().enabled = false;

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
            toggle.transform.GetChild(0).GetComponent<Image>().enabled = false;
            toggle.transform.GetChild(1).GetComponent<Image>().enabled = true;
            AudioControl(str, slider, toggle);
        }
        PlayerPrefs.SetInt(str + "toggle", System.Convert.ToInt32(toggle.isOn));     //Save
    }
    #endregion
    #endregion
}

