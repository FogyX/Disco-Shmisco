using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogComponent : MonoBehaviour
{
    private Queue<DialogSentence> sentencesQueue;

    [SerializeField] 
    private DialogSentence[] dialogSentences;

}
