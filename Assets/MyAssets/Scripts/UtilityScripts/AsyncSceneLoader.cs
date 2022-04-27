using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

/// <summary>
/// This class provides you a special method for async loading.
/// </summary>
public class AsyncSceneLoader : MonoBehaviour
{
    private bool _isLoading;

    [SerializeField] private AudioMixerSnapshot volumeOffSnapshot;
    [SerializeField] private AudioMixerSnapshot volumeOnSnapshot;

    private const string ASYNC_LOADER_PATH = @"AsyncSceneLoaderFolder/AsyncSceneLoader";

    private static AsyncSceneLoader _instance;
    public static AsyncSceneLoader instance
    {
        get
        {
            if (_instance == null)
            {
                var prefab = Resources.Load<AsyncSceneLoader>(ASYNC_LOADER_PATH);
                _instance = Instantiate(prefab);
                DontDestroyOnLoad(_instance);
            }

            return _instance;
        }
    }

    /// <summary>
    /// Loads the scene asynchronously (by name). 
    /// </summary>
    /// <param name="sceneName">The name of the scene to load.</param>
    /// <param name="fadeSoundsIn">Do fade-in effect for sounds.</param>
    /// <param name="fadeSoundsOut">Do fade-out effect for sounds.</param>
    /// <param name="fadeInDuration">The duration of the fade-in effect.</param>
    /// <param name="fadeOutDuration">The duration of the fade-out effect.</param>
    /// <returns></returns>
    public IEnumerator AsyncSceneLoad(string sceneName, bool fadeSoundsIn = true, bool fadeSoundsOut = true, float fadeInDuration = 1f, float fadeOutDuration = 1f)
    {
        if (_isLoading)
        {
            print("Is loading already");
            yield break;
        }

        FadeSoundsIn(fadeInDuration, fadeSoundsIn);
        yield return StartCoroutine(StartFading(fadeInDuration));

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);
	
	    asyncOperation.allowSceneActivation = false;

        while (asyncOperation.progress < 0.9f)
	    {
		    yield return null;
	    }

	    asyncOperation.allowSceneActivation = true;

        asyncOperation.completed += (asyncOperation) =>
        {
            FadeSoundsOut(fadeOutDuration, fadeSoundsOut);
            StartCoroutine(EndFading(fadeOutDuration));
        };
    }

    private IEnumerator StartFading(float fadeInDuration)
    {
        if (FaderScript.instance.animator != null && FaderScript.instance.animator.isActiveAndEnabled)
        {
            FaderScript.instance.animator.speed = 1 / fadeInDuration;
        }

        _isLoading = true;

        bool waitForFadeEnding = true;

        FaderScript.instance.FadeIn(SetDefaults);

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

    private IEnumerator EndFading(float fadeOutDuration)
    {
        if (FaderScript.instance.animator != null && FaderScript.instance.animator.isActiveAndEnabled)
        {
            FaderScript.instance.animator.speed = 1 / fadeOutDuration;
        }

        bool waitForFadeEnding = true;

        FaderScript.instance.FadeOut(SetDefaults);

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

    private void FadeSoundsIn(float fadeDuration, bool doFadeSounds)
    {
        if (doFadeSounds)
        {
            volumeOffSnapshot.TransitionTo(fadeDuration / 2);
        }
        else
        {
            // Turns sounds off immediatly
            volumeOffSnapshot.TransitionTo(0);
        }
    }

    private void FadeSoundsOut(float fadeDuration, bool doFadeSounds)
    {
        if (doFadeSounds)
        {
            volumeOnSnapshot.TransitionTo(fadeDuration / 2);
        }
        else
        {
            // Turns sounds on immediatly
            volumeOnSnapshot.TransitionTo(0);
        }
    }
}