using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// This class contains a special method for async loading with fade effect.
/// </summary>
public static class AsyncSceneLoading
{
    private const string PATH_TO_PANEL = "DarkPanel";
    private static GameObject darkPanel;

    private static bool isLoading = false;

    /// <summary>
    /// This coroutine loads the scene in the background.
    /// </summary>
    /// <param name="monoBehaviour">MonoBehaviour that will call the coroutines. You can type "this" to reference to the current instance.</param>
    /// <param name="sceneIndex">Index of the scene you want to load.</param>
    /// <param name="doFade">Do you want to load the scene with Fade-Out effect? (Default valus is true)</param>
    /// <param name="fadeSounds">Do you want to fade sounds too? (Works only if doFade is true)</param>
    /// <returns></returns>
    ///
    public static IEnumerator AsyncLoader(MonoBehaviour monoBehaviour, int sceneIndex, bool doFade = true, bool fadeSounds = true)
    {
        // Don't do anything if the coroutine is already being called
        if (isLoading)
        {
            yield break;
        }

        isLoading = true;

        // Start the fade effect if i want so
        if (doFade) 
        { 
            if (fadeSounds)
            {
                monoBehaviour.StartCoroutine(FadeSounds());
            }
            yield return monoBehaviour.StartCoroutine(FadeScreenOut());
        }

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Single);

        // Wait until the scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        isLoading = false;
    }

    private static IEnumerator FadeScreenOut()
    {
        // Create dark panel
        darkPanel = Resources.Load<GameObject>(PATH_TO_PANEL);
        GameObject darkPanelInstance = Object.Instantiate(darkPanel, Vector3.zero, Quaternion.identity);

        Image darkPanelImage = darkPanelInstance.GetComponentInChildren<Image>();

        Color temporaryColor = darkPanelImage.color;

        // Change the opacity of the panel
        for (float a = 0; a < 1; a += 0.01f)
        {
            temporaryColor.a = a;
            darkPanelImage.color = temporaryColor;

            yield return null;
        }
    }

    private static IEnumerator FadeSounds()
    {
        float temporaryVolume = AudioListener.volume;

        for ( ; temporaryVolume > 0; temporaryVolume -= 0.01f)
        {
            AudioListener.volume = temporaryVolume;

            yield return null;
        }
    }
}
