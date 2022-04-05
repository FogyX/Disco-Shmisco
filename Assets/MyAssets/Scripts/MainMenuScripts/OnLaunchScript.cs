using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnLaunchScript : MonoBehaviour
{
    private GameObject darkPanel;

    private GameObject darkPanelInstance;

    private Image darkPanelImage;

    private void Awake()
    {
        darkPanel = Resources.Load<GameObject>("DarkPanel");
        darkPanelInstance = Instantiate(darkPanel, Vector3.zero, Quaternion.identity);
        darkPanelImage = darkPanelInstance.GetComponentInChildren<Image>();
        darkPanelImage.color = new Color(0, 0, 0, 1);
        AudioListener.volume = 0;
    }

    private void Start()
    {
        StartCoroutine(SceneStarting());
    }

    private IEnumerator SceneStarting()
    {
        Debug.Log("Starting the scene");

        Color temporaryColor = darkPanelImage.color;
        // Change the opacity of the panel
        for (float a = 1; a > 0; a -= 0.01f)
        {
            temporaryColor.a = a;
            darkPanelImage.color = temporaryColor;

            AudioListener.volume += 0.01f;

            yield return null;
        }

    }
}
