using UnityEngine;

[CreateAssetMenu(fileName = "dialog", menuName = "Create Dialog Scratch", order = 51)]
public class DialogAsset : ScriptableObject
{
    public DialogSentence[] dialogSentences;
}
