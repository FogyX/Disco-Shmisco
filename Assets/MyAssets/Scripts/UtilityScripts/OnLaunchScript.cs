using UnityEngine;

public class OnLaunchScript : MonoBehaviour
{
    [SerializeField] private float fadeDuration;

    [SerializeField] private string mainMenuSceneName;

    private void Start()
    {
        StartCoroutine(AsyncSceneLoader.instance.AsyncSceneLoad(mainMenuSceneName, fadeSoundsOut: false, fadeInDuration: fadeDuration, fadeOutDuration: fadeDuration));
    }
}