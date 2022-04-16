using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

/// <summary>
/// This class provides you a special method for async loading.
/// </summary>
public class AsyncSceneLoader : MonoBehaviour
{
    private static float _fadeInDuration;
    private static float _fadeOutDuration;
    private static bool _isLoading;

    [SerializeField]
    private static AudioMixerGroup masterAudio;

    [SerializeField]
    private static AudioMixerSnapshot volumeOffSnapshot;

    [SerializeField]
    private static AudioMixerSnapshot volumeOnSnapshot;

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
    /// Loads the scene asynchronously. It is highly recommended to read the docs.
    /// </summary>
    /// <param name="sceneName">The name of the scene to load.</param>
    /// <param name="doFadeIn">Do fade-in effect.</param>
    /// <param name="doFadeOut">Do fade-out effect.</param>
    /// <param name="fadeSoundsIn">Do fade-in effect for sounds.</param>
    /// <param name="fadeSoundsOut">Do fade-out effect for sounds.</param>
    /// <param name="fadeInDuration">The duration of the fade-in effect.</param>
    /// <param name="fadeOutDuration">The duration of the fade-out effect.</param>
    /// <returns></returns>
    public IEnumerator AsyncSceneLoad(string sceneName, bool doFadeIn = true, bool doFadeOut = true, bool fadeSoundsIn = true, bool fadeSoundsOut = true, float fadeInDuration = 1f, float fadeOutDuration = 1f)
    {
        // Don't do anything, if some scene is loading already.
        if (_isLoading)
        {
            yield break;
        }

        if (doFadeIn)
        {
            yield return StartCoroutine(StartFading(_fadeInDuration, fadeSoundsIn));
        }

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);
	
	    asyncOperation.allowSceneActivation = false;

        while (asyncOperation.progress < 0.9f)
	    {
		    yield return null;
	    }

	    asyncOperation.allowSceneActivation = true;

        if (doFadeOut)
        {
            yield return StartCoroutine(EndFading(_fadeOutDuration, fadeSoundsOut));
        }
    }

    private IEnumerator StartFading(float fadeInDuration, bool doFadeSounds)
    {
        if (FaderScript.instance.animator != null && FaderScript.instance.animator.isActiveAndEnabled)
        {
            FaderScript.instance.animator.speed = fadeInDuration;
        }

        _isLoading = true;

        bool waitForFadeEnding = true;

        FaderScript.instance.FadeIn(SetDefaults);

        if (doFadeSounds)
        {
            StartCoroutine(FadeSoundsIn(fadeInDuration));
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

    private IEnumerator EndFading(float fadeOutDuration, bool doFadeSounds)
    {
	Debug.Log("Start endfade coro");

        if (FaderScript.instance.animator != null && FaderScript.instance.animator.isActiveAndEnabled)
        {
            FaderScript.instance.animator.speed = fadeOutDuration;
        }

        bool waitForFadeEnding = true;

        FaderScript.instance.FadeOut(SetDefaults);

        if (doFadeSounds)
        {
            StartCoroutine(FadeSoundsOut(fadeOutDuration));
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

    private IEnumerator FadeSoundsIn(float fadeDuration)
    {
        volumeOffSnapshot.TransitionTo(fadeDuration);
        yield return null;
    }

    private IEnumerator FadeSoundsOut(float fadeDuration)
    {
        volumeOnSnapshot.TransitionTo(fadeDuration);
        yield return null;
    }
}