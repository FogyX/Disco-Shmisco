using UnityEngine;
using UnityEngine.UI;

public class ChangeLanguage : MonoBehaviour
{
    [SerializeField] private Sprite languageRus;
    [SerializeField] private Sprite languageEng;

    [SerializeField] private Image selectorImage;


    private void Awake()
    {
        /* 
         * If this is the first time player runs game, then Language will have
         * its default value. In such a case, the system language is checked.
         * If it is russian / ukrainian / belarusian, then set Language 
         * to russian. In other case, set it to ENG.
        */

        if (PlayerPrefs.GetString("Language", "notSelected") == "notSelected")  {
            var systemLanguage = Application.systemLanguage;

            switch (systemLanguage)
            {
                case SystemLanguage.Russian:
                case SystemLanguage.Ukrainian:
                case SystemLanguage.Belarusian:
                    PlayerPrefs.SetString("Language", "RUS");
                    break;

                default:
                    PlayerPrefs.SetString("Language", "ENG");
                    break;
            }
        }

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
            selectorImage.sprite = languageRus;
        }

        // If the game language set to RUS...
        else if (PlayerPrefs.GetString("Language") == "RUS")
        {
            // ...set it to ENG.
            PlayerPrefs.SetString("Language", "ENG");
            selectorImage.sprite = languageEng;
        }

        /* There could not be any other cases, because Language pref is set to
         * either English or Russian (depending on the system language).
         */
    }
}
