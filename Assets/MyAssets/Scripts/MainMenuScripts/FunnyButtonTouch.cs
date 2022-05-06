using UnityEngine;

public class FunnyButtonTouch : MonoBehaviour
{
    [SerializeField] private AudioClip audioClip;

    [SerializeField] private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayFunnySound()
    {
        audioSource.PlayOneShot(audioClip);
    }
}
