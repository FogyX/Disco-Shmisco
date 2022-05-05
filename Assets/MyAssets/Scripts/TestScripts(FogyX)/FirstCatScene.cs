using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

/// <summary>
/// This class is used for handling stuff in the first cat scene
/// </summary>
public class FirstCatScene : MonoBehaviour
{
    [SerializeField] private string mainMenuScene;

    [SerializeField] private LocalizableText textObject;

    [SerializeField] private GameObject testCube;

    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip clip;

    public UnityEvent startDialog = new UnityEvent();


    private void Awake()
    {
        PlayerPrefs.SetInt("NotFirstPlay", 1);
    }

    private void Start()
    {
        StartCoroutine(StartDialogWait());
    }

    private IEnumerator StartDialogWait()
    {
        yield return new WaitForSeconds(1);
        startDialog.Invoke();
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
        textObject.ChangeTextVariants("Успешно удалён преф NotFirstPLay!", "Succesfully deleted NotFirstPlay pref!");

        yield return new WaitForSeconds(3f);

        textObject.ChangeTextVariants("", "");
    }

    public void ChangeCubeColor()
    {
        Color32 newColor = new Color32((byte)Random.Range(0, 255), (byte)Random.Range(0, 255), (byte)Random.Range(0, 255), 255);
        testCube.GetComponent<SpriteRenderer>().DOColor(newColor, 0.5f);
    }

    public void MoveCubeRandom()
    {
        float x = Random.Range(-5f, 5f);
        float y = Random.Range(-5f, 5f);
        testCube.transform.DOMove(new Vector3(x, y, 0), 0.5f);
    }

    public void SetMusic()
    {
        audioSource.clip = clip;
        audioSource.Play();
    }
}
