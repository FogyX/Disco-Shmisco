using UnityEngine;

public class FunnyButtonTouch : MonoBehaviour
{
    [SerializeField] private AudioClip audioClip;

    [SerializeField] private AudioSource audioSource;

    public void PlayFunnySound()
    {
        audioSource.PlayOneShot(audioClip);
    }
}
