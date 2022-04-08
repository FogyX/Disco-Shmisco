using UnityEngine;
using UnityEngine.UI;

public class SoundSliderScript : MonoBehaviour
{
    [SerializeField]
    private Slider slider;

    private void Start()
    {
        slider.value = PlayerPrefs.GetFloat("gameVolume");
    }

    public void ChangeSound()
    {
        AudioListener.volume = slider.value;
        PlayerPrefs.SetFloat("gameVolume", slider.value);
    }
}
