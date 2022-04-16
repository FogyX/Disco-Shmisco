using UnityEngine;

public class PlayButtonScript : MonoBehaviour
{
    [SerializeField]
    private float _fadeInDuration;

    [SerializeField]
    private float _fadeOutDuration;


    [SerializeField]
    private string _catSceneName;

    [SerializeField]
    private string _levelMapSceneName;


    public void OnPlayTouch()
    {
        // If this is the first time the player is running the game, "NotFirstPlay" equals 0
        // If isn't, then "NotFirstPlay" equals 1
        // DON'T FORGET TO CHANGE THIS VALUE IN THE CATSCENE!!!
        if (PlayerPrefs.GetInt("NotFirstPlay", 0) != 1)
        {
            StartCoroutine(AsyncSceneLoader.instance.AsyncSceneLoad(_catSceneName, fadeInDuration: _fadeInDuration, fadeOutDuration: _fadeOutDuration));
        }
        else
        {
            StartCoroutine(AsyncSceneLoader.instance.AsyncSceneLoad(_catSceneName, fadeInDuration: _fadeInDuration, fadeOutDuration: _fadeInDuration));
        }

    }
}
