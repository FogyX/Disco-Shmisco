using System;
using System.Collections;
using UnityEngine;

public class OnLaunchScript : MonoBehaviour
{
    [SerializeField]
    private float fadeDuration;

    private float _fadeSpeed;

    [SerializeField]
    private float _soundFadeRatio;

    private void Awake()
    {
        _fadeSpeed = 1f / fadeDuration;
        AudioListener.volume = 0.0f;
    }

    private void Start()
    {
        StartCoroutine(SceneStartWrapper());
    }

    private IEnumerator SceneStartWrapper()
    {
        yield return StartCoroutine(SceneStarting(_fadeSpeed));
    }

    private IEnumerator SceneStarting(float fadeInSpeed)
    {
        Debug.Log("Starting the scene");

        if (FaderScript.instance.animator != null && FaderScript.instance.animator.isActiveAndEnabled)
        {
            FaderScript.instance.animator.speed = fadeInSpeed;
        }

        bool waitForFadeEnding = true;

        FaderScript.instance.FadeOut(SetDefaults);

        StartCoroutine(FadeSoundsOut(fadeInSpeed));

        while (waitForFadeEnding)
        {
            yield return null;
        }

        void SetDefaults()
        {
            waitForFadeEnding = false;
            FaderScript.instance.animator.speed = 1f;
        }
    }

    private IEnumerator FadeSoundsOut(float fadeSpeed)
    {
        float fadeSoundsSpeed = fadeSpeed / _soundFadeRatio;

        float volume = AudioListener.volume;

        float gameVolume = PlayerPrefs.GetFloat("gameVolume");

        for ( ; volume < gameVolume; )
        {
            volume += fadeSoundsSpeed;
            AudioListener.volume = volume;

            yield return null;
        }
    }
}