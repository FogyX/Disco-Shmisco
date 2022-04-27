using UnityEngine;
using UnityEngine.Audio;

public class GameSetupScript : MonoBehaviour
{
    private static GameSetupScript _instance;

    // Fields for mixer setup
    [SerializeField] private AudioMixer mainMixer;
    [SerializeField] private string musicParameterName;
    [SerializeField] private string soundParameterName;

    private void Awake()
    {
        CheckInstance();
        SetupMixers();
        SetupLanguage();
    }

    private void CheckInstance()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    private void SetupMixers()
    {
        mainMixer.SetFloat(musicParameterName, Mathf.Log10(PlayerPrefs.GetFloat(musicParameterName, 1f)) * 20);
        mainMixer.SetFloat(soundParameterName, Mathf.Log10(PlayerPrefs.GetFloat(soundParameterName, 1f)) * 20);
    }

    private void SetupLanguage()
    {
        /* 
         * If this is the first time player runs game, then Language will have
         * its default value. In such a case, the system language is checked.
         * If it is russian / ukrainian / belarusian, then set Language 
         * to russian. In other case, set it to ENG.
        */

        if (PlayerPrefs.GetString("Language", "notSelected") == "notSelected")
        {
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


        if (PlayerPrefs.GetString("Language", "ENG") == "ENG")
        {
            ChangeLanguage.OnLanguageSetEng.Invoke();
        }
        else if (PlayerPrefs.GetString("Language") == "RUS")
        {
            ChangeLanguage.OnLanguageSetRus.Invoke();
        }
    }
}
