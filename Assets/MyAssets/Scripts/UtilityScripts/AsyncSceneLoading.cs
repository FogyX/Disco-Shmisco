using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This class provides you a special method for async loading.
/// </summary>
public static class AsyncSceneLoading
{
    private static float _fadeInSpeed;

    private static float _fadeOutSpeed;

    private static bool _isLoading;

    private static MonoBehaviour _monoBehaviour;

    private const float SOUND_FADE_CONST = 25f;

    /// <summary>
    /// Loads the scene asynchronously. It is highly recommended to read the docs.
    /// </summary>
    /// <param name="mb">Monobehaviour which will call the coroutines. Use "this" here.</param>
    /// <param name="sceneName">The name of the scene to load.</param>
    /// <param name="doFadeIn">Do fade-in effect.</param>
    /// <param name="doFadeOut">Do fade-out effect.</param>
    /// <param name="fadeSoundsIn">Do fade-in effect for sounds.</param>
    /// <param name="fadeSoundsOut">Do fade-out effect for sounds.</param>
    /// <param name="fadeInDuration">The duration of the fade-in effect.</param>
    /// <param name="fadeOutDuration">The duration of the fade-out effect.</param>
    /// <returns></returns>
    public static IEnumerator AsyncSceneLoad(MonoBehaviour mb, string sceneName, bool doFadeIn = true, bool doFadeOut = true, bool fadeSoundsIn = true, bool fadeSoundsOut = true, float fadeInDuration = 1f, float fadeOutDuration = 1f)
    {
        _monoBehaviour = mb;

        // Don't do anything, if some scene is loading already.
        if (_isLoading)
        {
            yield break;
        }

        if (doFadeIn)
        {
            if (fadeInDuration != 0.0)
            {
                _fadeInSpeed = 1f / fadeInDuration;
            }
            else
            {
                doFadeIn = false;
            }
        }

        if (doFadeOut)
        {
            if (fadeOutDuration != 0.0)
            {
                _fadeOutSpeed = 1f / fadeInDuration;
            }
            else
            {
                doFadeOut = false;
            }
        }

        if (doFadeIn)
        {
            yield return _monoBehaviour.StartCoroutine(StartFading(_fadeInSpeed, fadeSoundsIn, _monoBehaviour));
        }

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);

        asyncOperation.allowSceneActivation = false;

        while (asyncOperation.progress < 0.9)
        {
            yield return null;
        }

        asyncOperation.allowSceneActivation = true;

        if (doFadeOut)
        {
            yield return _monoBehaviour.StartCoroutine(EndFading(_fadeOutSpeed, fadeSoundsOut, _monoBehaviour));
        }
    }

    private static IEnumerator StartFading(float fadeInSpeed, bool doFadeSounds, MonoBehaviour monoBehaviour)
    {
        if (FaderScript.instance.animator != null && FaderScript.instance.animator.isActiveAndEnabled)
        {
            FaderScript.instance.animator.speed = fadeInSpeed;
        }

        _isLoading = true;

        bool waitForFadeEnding = true;

        FaderScript.instance.FadeIn(SetDefaults);

        if (doFadeSounds)
        {
            monoBehaviour.StartCoroutine(FadeSoundsIn(fadeInSpeed));
        }

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

    private static IEnumerator EndFading(float fadeOutSpeed, bool doFadeSounds, MonoBehaviour monoBehaviour)
    {
        if (FaderScript.instance.animator != null && FaderScript.instance.animator.isActiveAndEnabled)
        {
            FaderScript.instance.animator.speed = fadeOutSpeed;
        }

        bool waitForFadeEnding = true;

        FaderScript.instance.FadeOut(SetDefaults);

        if (doFadeSounds)
        {
            monoBehaviour.StartCoroutine(FadeSoundsOut(fadeOutSpeed));
        }

        while (waitForFadeEnding)
        {
            yield return null;
        }

        _isLoading = false;

        void SetDefaults()
        {
            waitForFadeEnding = false;
            FaderScript.instance.animator.speed = 1f;
        }

    }

    private static IEnumerator FadeSoundsIn(float fadeSpeed)
    {
        float fadeSoundsSpeed = fadeSpeed / SOUND_FADE_CONST;

        float volume = AudioListener.volume;

        for ( ; volume > 0; )
        {
            volume -= fadeSoundsSpeed;
            AudioListener.volume = volume;

            yield return null;
        }
    }

    private static IEnumerator FadeSoundsOut(float fadeSpeed)
    {
        float fadeSoundsSpeed = fadeSpeed / SOUND_FADE_CONST;

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