using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class LocalizableText : MonoBehaviour
{
    private TextMeshProUGUI _textToLocalize;

    public string russianVariant;

    public float russianFontSize;

    public string englishVariant;

    public float englishFontSize;

    private void Start()
    {
        _textToLocalize = GetComponent<TextMeshProUGUI>();

        ChangeLanguage.OnLanguageSetRus.AddListener(ChangeTextToRus);
        ChangeLanguage.OnLanguageSetEng.AddListener(ChangeTextToEng);

        if (PlayerPrefs.GetString("Language") == "RUS")
        {
            ChangeTextToRus();
        }
        else
        {
            ChangeTextToEng();
        }
    }

    /// <summary>
    /// Use this method to update the text after changing the variants manually.
    /// </summary>
    public void UpdateText()
    {
        if (PlayerPrefs.GetString("Language") == "RUS")
        {
            ChangeTextToRus();
        }
        else
        {
            ChangeTextToEng();
        }
    }

    /// <summary>
    /// Use this method to change the text variants easier way.
    /// </summary>
    /// <param name="russianVariant"></param>
    /// <param name="englishVariant"></param>
    public void ChangeTextVariants(string russianVariant, string englishVariant)
    {
        this.russianVariant = russianVariant;
        this.englishVariant = englishVariant;
        UpdateText();
    }

    private void ChangeTextToRus()
    {
        _textToLocalize.text = russianVariant;
        _textToLocalize.fontSize = russianFontSize;
    }

    private void ChangeTextToEng()
    {
        _textToLocalize.text = englishVariant;
        _textToLocalize.fontSize = englishFontSize;
    }
}
