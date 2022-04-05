using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ExitButtonTouch : MonoBehaviour
{
    private GameObject darkPanel;

    private bool isExiting = false;

    private void Awake()
    {
        darkPanel = Resources.Load<GameObject>("DarkPanel");
    }

    public void OnExitTouch()
    {
        if (isExiting)
            return;

        isExiting = true;
        StartCoroutine(FadeOutForExit());
    }

    private IEnumerator FadeOutForExit()
    {
        GameObject darkPanelCanvas = Instantiate(darkPanel, Vector3.zero, Quaternion.identity);

        Image darkPanelImage = darkPanelCanvas.GetComponentInChildren<Image>();

        Color temporaryColor = darkPanelImage.color;

        // Change the opacity of the panel
        for (float a = 0; a < 1; a += 0.01f)
        {
            temporaryColor.a = a;
            darkPanelImage.color = temporaryColor;

            AudioListener.volume -= 0.01f;

            yield return null;
        }

        Application.Quit();
    }
}
