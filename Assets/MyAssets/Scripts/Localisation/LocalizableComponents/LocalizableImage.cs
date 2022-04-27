using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class LocalizableImage : MonoBehaviour
{
    private Image _imageToLocalize;

    [SerializeField] private Sprite russianVariant;

    [SerializeField] private Sprite englishVariant;

    private void Start()
    {
        _imageToLocalize = GetComponent<Image>();

        ChangeLanguage.OnLanguageSetRus.AddListener(ChangeImageToRus);
        ChangeLanguage.OnLanguageSetEng.AddListener(ChangeImageToEng);

        if (PlayerPrefs.GetString("Language") == "RUS")
        {
            ChangeImageToRus();
        }
        else
        {
            ChangeImageToEng();
        }
    }

    private void ChangeImageToRus()
    {
        _imageToLocalize.sprite = russianVariant;
    }

    private void ChangeImageToEng()
    {
        _imageToLocalize.sprite = englishVariant;
    }
}

