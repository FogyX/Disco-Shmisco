using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogSentence
{
    public string characterName;

    [TextArea(3, 10)]
    public string phraseOfSentence;

    public AudioClip characterVoice;
}
