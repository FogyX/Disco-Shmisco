using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ChangeLanguage : MonoBehaviour
{
    [SerializeField] private Sprite languageRus;
    [SerializeField] private Sprite languageEng;

    [SerializeField] private Image selectorImage;

    public static UnityEvent OnLanguageSetRus = new UnityEvent();
    public static UnityEvent OnLanguageSetEng = new UnityEvent();

    private void Awake()
    {
        // Set the button sprite.
        if (PlayerPrefs.GetString("Language") == "RUS")
        {
            selectorImage.sprite = languageRus;
        }
        else
        {
            selectorImage.sprite = languageEng;
        }
    }

    public void ChangeLanguageTouch()
    {
        // If the game language set to ENG...
        if (PlayerPrefs.GetString("Language") == "ENG")
        {
            // ...set it to RUS.
            PlayerPrefs.SetString("Language", "RUS");
            OnLanguageSetRus.Invoke();
            selectorImage.sprite = languageRus;
        }

        // If the game language set to RUS...
        else if (PlayerPrefs.GetString("Language") == "RUS")
        {
            // ...set it to ENG.
            PlayerPrefs.SetString("Language", "ENG");
            OnLanguageSetEng.Invoke();
            selectorImage.sprite = languageEng;
        }

        /* There could not be any other cases, because Language pref is set to
         * either English or Russian (depending on the system language).
         */
    }
}
