using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Audio;

public class AudioMixerController : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;

    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider SFXSlider;
    [SerializeField] private Text masterText;
    [SerializeField] private Text musicText;
    [SerializeField] private Text SFXText;

    private void Start()
    {
        if (PlayerPrefs.HasKey("MasterVolume") || PlayerPrefs.HasKey("MusicVolume") || PlayerPrefs.HasKey("SFXVolume"))
        {
            LoadVolume();
        }
        else
        {
            SetMasterVolume();
            SetMusicVolume();
            SetSFXVolume();
        }
    }

    public void SetMasterVolume()
    {
        float masterSliderValue = masterSlider.value;
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(masterSliderValue) * 20);
        PlayerPrefs.SetFloat("MasterVolume", masterSliderValue);
        masterText.text = masterSliderValue.ToString("0%");
    }
    public void SetMusicVolume()
    {
        float musicSliderValue = musicSlider.value;
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(musicSliderValue) * 20);
        PlayerPrefs.SetFloat("MusicVolume", musicSliderValue);
        musicText.text = musicSliderValue.ToString("0%");
    }
    public void SetSFXVolume()
    {
        float sFXSliderValue = SFXSlider.value;
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(sFXSliderValue) * 20);
        PlayerPrefs.SetFloat("SFXVolume", sFXSliderValue);
        SFXText.text = sFXSliderValue.ToString("0%");
    }

    private void LoadVolume()
    {
        masterSlider.value = PlayerPrefs.GetFloat("MasterVolume");
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        SFXSlider.value = PlayerPrefs.GetFloat("SFXVolume");
        SetMasterVolume();
        SetMusicVolume();
        SetSFXVolume();
    }
}
