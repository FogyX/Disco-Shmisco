using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Represents a single sentence of the dialog
/// </summary>
[System.Serializable]
public class DialogSentence
{
    public string characterName_rus;
    public string characterName_eng;

    [TextArea(3, 10)] public string sentenceText_rus;
    [TextArea(3, 10)] public string sentenceText_eng;

    public AudioClip characterVoice;

    /// <summary>
    /// Talk Speed 1 => 1 character/word per second
    /// </summary>
    [Range(1, 50)] public int talkSpeed = 10;
    public SpeechType speechType = SpeechType.CharByChar;

    /// <summary>
    /// Voice Frequency [x] => The voice sound plays once per [x] character/word
    /// </summary>
    [Range(1, 10)] public int voiceFrequency = 4;

    public bool isShaking = false;

    /// <summary>
    /// This callback will be called at the start of the showing this sentence;
    /// </summary>
    public UnityEvent callbackEventOnStart;

    /// <summary>
    /// This callback will be called at the end of the showing this sentence;
    /// </summary>
    public UnityEvent callbackEventOnEnd;
}

public enum SpeechType
{
    CharByChar,
    WordByWord
}