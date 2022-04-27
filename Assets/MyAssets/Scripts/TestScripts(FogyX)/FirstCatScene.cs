using System.Collections;
using UnityEngine;

/// <summary>
/// This class is used for handling stuff in the first cat scene
/// </summary>
public class FirstCatScene : MonoBehaviour
{
    [SerializeField]
    private string mainMenuScene;

    [SerializeField]
    private LocalizableText textObject;

    private void Awake()
    {
        PlayerPrefs.SetInt("NotFirstPlay", 1);
    }

    public void OnReturnTouch()
    {
        StartCoroutine(AsyncSceneLoader.instance.AsyncSceneLoad(mainMenuScene, fadeSoundsOut: false));
    }

    public void ClearPrefs()
    {
        PlayerPrefs.DeleteKey("NotFirstPlay");
        StartCoroutine(ChangeText());
    }

    private IEnumerator ChangeText()
    {
        textObject.russianVariant = "Успешно удалён преф NotFirstPLay!";
        textObject.englishVariant = "Succesfully deleted NotFirstPlay pref!";
        textObject.UpdateText();

        yield return new WaitForSeconds(3f);

        textObject.russianVariant = "";
        textObject.englishVariant = "";
        textObject.UpdateText();
    }
}
