using System.Collections;
using UnityEngine;

public class ExitButtonTouch : MonoBehaviour
{
    private bool _isExiting;

    private float _fadeSpeed;

    [SerializeField]
    private float fadeDuration;

    [SerializeField]
    private float _soundFadeRatio;

    private void Awake()
    {
        _fadeSpeed = 1f / fadeDuration;
    }

    public void OnExitTouch()
    {
        if (_isExiting)
        {
            return;
        }

        _isExiting = true;

        StartCoroutine(FadeInForExit(_fadeSpeed));
    }

    private IEnumerator FadeInForExit(float fadeOutSpeed)
    {
        if (FaderScript.instance.animator != null && FaderScript.instance.animator.isActiveAndEnabled)
        {
            FaderScript.instance.animator.speed = fadeOutSpeed;
        }

        bool waitForFadeEnding = true;

        FaderScript.instance.FadeIn(() => waitForFadeEnding = false);

        StartCoroutine(FadeSoundsIn(fadeOutSpeed));

        while (waitForFadeEnding)
        {
            yield return null;
        }

        Debug.Log("Succesfully exited!");
        Application.Quit();
    }

    private IEnumerator FadeSoundsIn(float fadeSpeed)
    {
        float fadeSoundsSpeed = fadeSpeed / _soundFadeRatio;

        float volume = AudioListener.volume;

        for ( ; volume > 0; )
        {
            volume -= fadeSoundsSpeed;
            AudioListener.volume = volume;
            
            yield return null;
        }
    }
}
