using UnityEngine;
using DG.Tweening;

public class DiskScratching : MonoBehaviour
{
    [SerializeField] private AudioSource pitchingAudioSource;
    [SerializeField] private Animator animator;
    [SerializeField] private string parameterName;


    public void BeginScratch()
    {
        animator.SetBool(parameterName, true);
        pitchingAudioSource.DOPitch(-1.5f, 0.1f);
    }

    public void EndScratch()
    {
        animator.SetBool(parameterName, false); 
        pitchingAudioSource.DOPitch(1f, 0.1f);
    }
}