using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class ExitButtonTouch : MonoBehaviour
{
    private bool _isExiting;

    private float _fadeSpeed;

    [SerializeField] private float fadeDuration;

    [SerializeField] private AudioMixerSnapshot volumeOff;

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

        StartCoroutine(FadeSoundsIn(fadeDuration));

        while (waitForFadeEnding)
        {
            yield return null;
        }

        Debug.Log("Succesfully exited!");
        Application.Quit();
    }

    private IEnumerator FadeSoundsIn(float fadeDuration)
    {
        volumeOff.TransitionTo(fadeDuration);
        yield return null;
    }
}
