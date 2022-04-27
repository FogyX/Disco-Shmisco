using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundSliderScript : MonoBehaviour
{
    [SerializeField] private Slider slider;

    [SerializeField] private AudioMixer sliderAudioMixer;


    /// <summary>
    /// Either MusicVolume or SoundVolume
    /// </summary>
    [SerializeField]
    private string volumeParameterName;

    private void Awake()
    {
        if (volumeParameterName != "MusicVolume" && volumeParameterName != "SoundVolume")
        {
            throw new System.Exception("Volume Parameter Name is either MusicVolume or SoundVolume");
        }
        slider.value = PlayerPrefs.GetFloat(volumeParameterName, 1f);
    }


    public void ChangeVolume()
    {
        PlayerPrefs.SetFloat(volumeParameterName, slider.value);
        sliderAudioMixer.SetFloat(volumeParameterName, Mathf.Log10(slider.value) * 20); 
    }
}
