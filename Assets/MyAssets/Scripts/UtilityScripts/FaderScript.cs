using System;
using UnityEngine;

/// <summary>
/// FaderScript class provides a singleton fader. It's highly recommended to read the code first!
/// </summary>
public class FaderScript : MonoBehaviour
{
    /// <summary>
    /// Animator component.
    /// </summary>
    [SerializeField] public Animator animator;

    /// <summary>
    /// Path to the fader prefab, change it if you need so;
    /// </summary>
    private const string FADER_PATH = @"FaderFolder/FaderCanvas";

    private static FaderScript _instance;

    /// <summary>
    /// Singleton property of instance field.
    /// </summary>
    public static FaderScript instance
    {
        get
        {
            if (_instance == null)
            {
                var prefab = Resources.Load<FaderScript>(FADER_PATH);
                _instance = Instantiate(prefab);
                DontDestroyOnLoad(_instance.gameObject);
            }

            return _instance;
        }
    }

    /// <summary>
    /// Is the fader fading right now?
    /// </summary>
    public bool isFading { get; private set; }

    /// <summary>
    /// Callback for fade-in ending.
    /// </summary>
    private Action _fadedInCallBack;

    /// <summary>
    /// Callback for fade-out ending.
    /// </summary>
    private Action _fadedOutCallBack;

    /// <summary>
    /// FadeIn method starts the fade-in animation.
    /// </summary>
    /// <param name="fadedInCallBack">Callback for the end of the animation.</param>
    public void FadeIn(Action fadedInCallBack)
    {
        if (isFading)
            return;

        isFading = true;
        _fadedInCallBack = fadedInCallBack;

        animator.SetBool("faded", true);
    }

    /// <summary>
    ///  FadeOut method starts the fade-out animation.
    /// </summary>
    /// <param name="fadedOutCallBack">Callback for the end of the animation.</param>
    public void FadeOut(Action fadedOutCallBack)
    {
        if (isFading)
            return;

        isFading = true;
        _fadedOutCallBack = fadedOutCallBack;

        animator.SetBool("faded", false);
    }

    /// <summary>
    /// Invokes and clears the callback when the animation ends.
    /// </summary>
    private void Handle_FadeInAnimationOver() {
        Debug.Log("Fade-in animation over!");
        _fadedInCallBack?.Invoke();
        _fadedInCallBack = null;
        isFading = false;
    }
    /// <summary>
    /// Invokes and clears the callback when the animation ends.
    /// </summary>
    private void Handle_FadeOutAnimationOver()
    {
        Debug.Log("Fade-out animation over!");
        _fadedOutCallBack?.Invoke();
        _fadedOutCallBack = null;
        isFading = false;
    }

}
