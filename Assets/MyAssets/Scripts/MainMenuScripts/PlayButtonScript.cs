using UnityEngine;

public class PlayButtonScript : MonoBehaviour
{
    public void OnPlayTouch()
    {
        // If this is the first time the player is running the game, "NotFirstPlay" equals 0
        // If isn't, then "NotFirstPlay" equals 1
        // DON'T FORGET TO CHANGE THIS VALUE IN THE CATSCENE!!!
        if (PlayerPrefs.GetInt("NotFirstPlay", 0) != 1)
        {
            StartCoroutine(AsyncSceneLoading.AsyncLoader(this, 1));
        }
        else
        {
            StartCoroutine(AsyncSceneLoading.AsyncLoader(this, 3));
        }
    }
}
