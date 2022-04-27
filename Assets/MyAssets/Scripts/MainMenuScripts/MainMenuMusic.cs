using UnityEngine;

public class MainMenuMusic : MonoBehaviour
{
    [SerializeField] private GameObject audioHolder;

    [SerializeField] private AudioClip audioClip;

    private AudioSource audioSource;

    private void Start()
    {
        if (audioHolder != null)
        {
            audioSource = audioHolder.GetComponent<AudioSource>();
        }

        if (audioSource != null && audioClip != null)
        {
            audioSource.clip = audioClip;
            audioSource.Play();
        }
    }
}
