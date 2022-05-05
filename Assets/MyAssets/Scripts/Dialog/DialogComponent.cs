using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using DG.Tweening;
using TMPro;

public class DialogComponent : MonoBehaviour
{
    [SerializeField] private float fadePanelInDuration;
    [SerializeField] private float fadePanelOutDuration;

    // Callbacks for dialog starting and ending
    [SerializeField] private UnityEvent onStartDialogEvent = new UnityEvent();
    [SerializeField] private UnityEvent onEndDialogEvent = new UnityEvent();

    // I'm to lazy to make a custom editor for queue
    private Queue<DialogSentence> _sentencesQueue = new Queue<DialogSentence>();
    [SerializeField] private DialogSentence[] dialogSentences;

    private const string PANEL_PATH = @"Dialog/DialogPanel";

    private GameObject _dialogPanelInstance;
    private CanvasGroup _panelCanvasGroup;

    private Button _buttonNext;

    private TextMeshProUGUI _dialogText;
    private LocalizableText _dialogText_loc;
    private VertexJitter _dialogTextJitter;

    private TextMeshProUGUI _characterName;
    private LocalizableText _characterName_loc;

    private static bool _isShowingDialog = false;

    private AudioSource _soundSource;

    private void Awake()
    {
        CheckInstance();
        InitComponents();
        FillSentencesQueue(dialogSentences);
    }

    private void CheckInstance()
    {
        if (_dialogPanelInstance == null)
        {
            var prefab = Resources.Load<GameObject>(PANEL_PATH);
            _dialogPanelInstance = Instantiate(prefab);
        }
        _panelCanvasGroup = _dialogPanelInstance.GetComponent<CanvasGroup>();

        // To make sure the opacity will be 0 at the start
        _panelCanvasGroup.alpha = 0f;

        _panelCanvasGroup.blocksRaycasts = false;
    }

    private void InitComponents()
    {
        InitButtonNext();
        InitDialogText();
        InitCharacterName();
        InitSoundSource();
    }

    private void InitButtonNext()
    {
        _buttonNext = _dialogPanelInstance.
            GetComponent<ButtonNextContainer>().
            ContainedButton;

        _buttonNext.onClick.AddListener(NextSentence);
    }

    private void InitDialogText()
    {
        _dialogText = _dialogPanelInstance.
            GetComponent<DialogTextContainer>().
            ContainedDialogText;

        _dialogText_loc = _dialogText.gameObject.GetComponent<LocalizableText>();

        _dialogTextJitter = _dialogText.gameObject.GetComponent<VertexJitter>();
        _dialogTextJitter.enabled = false;
    }

    private void InitCharacterName()
    {
        _characterName = _dialogPanelInstance.
            GetComponent<CharacterNameContainer>().
            ContainedCharName;

        _characterName_loc = _characterName.gameObject.GetComponent<LocalizableText>();
    }

    private void InitSoundSource()
    {
        _soundSource = GetComponent<AudioSource>();
    }

    private void FillSentencesQueue(DialogSentence[] sentences)
    {
        foreach (var sentence in sentences)
        {
            _sentencesQueue.Enqueue(sentence);
        }
    }

    public void StartDialog()
    {
        if (_isShowingDialog)
        {
            Debug.LogWarning("Some dialog is showing already!");
            return;
        }

        // Fill the queue if it is null, just in case.
        // Maybe you want to start the same dialog more than once...
        if (_sentencesQueue.Count == 0)
        {
            FillSentencesQueue(dialogSentences);
        }

        _isShowingDialog = true;
        onStartDialogEvent.Invoke();
        StartCoroutine(StartDialogCoroutine());
    }

    private IEnumerator StartDialogCoroutine()
    {
        DOTween.To(
            () => _panelCanvasGroup.alpha,
            x => _panelCanvasGroup.alpha = x,
            1f,
            fadePanelInDuration
            );

        _panelCanvasGroup.blocksRaycasts = true;

        yield return new WaitForSeconds(0.5f);

        NextSentence();
    }

    private void NextSentence()
    {
        _dialogText_loc.ChangeTextVariants("", "");
        _characterName_loc.ChangeTextVariants("", "");
        _buttonNext.enabled = false;

        if (_sentencesQueue.Count > 0)
        {
            DialogSentence currentSentence = _sentencesQueue.Dequeue();

            currentSentence.callbackEventOnStart.Invoke();

            if (currentSentence.speechType == SpeechType.CharByChar)
            {
                StartCoroutine(ShowSentence_CharByChar(currentSentence));
            }

            else if (currentSentence.speechType == SpeechType.WordByWord)
            {
                StartCoroutine(ShowSentence_WordByWord(currentSentence));
            }
        }
        else
        {
            StartCoroutine(EndDialogCoroutine());
        }
    }

    private IEnumerator EndDialogCoroutine()
    {
        DOTween.To(
            () => _panelCanvasGroup.alpha,
            x => _panelCanvasGroup.alpha = x,
            0f,
            fadePanelOutDuration
            );

        _panelCanvasGroup.blocksRaycasts = false;

        yield return new WaitForSeconds(1f);

        onEndDialogEvent.Invoke();
        _isShowingDialog = false;
    }
    private IEnumerator ShowSentence_CharByChar(DialogSentence sentence)
    {
        if (sentence.isShaking)
        {
            _dialogTextJitter.enabled = true;
        }

        _characterName_loc.ChangeTextVariants(sentence.characterName_rus, sentence.characterName_eng);

        _dialogText.maxVisibleCharacters = 0;

        _dialogText_loc.ChangeTextVariants(sentence.sentenceText_rus, sentence.sentenceText_eng);


        float waitTime = 1f / sentence.talkSpeed;
        YieldInstruction waitInst = new WaitForSeconds(waitTime);

        for (int i = 0; i < _dialogText.text.Length; i++)
        {
            _dialogText.maxVisibleCharacters++;

            if (sentence.characterVoice != null)
            {
                if (i % sentence.voiceFrequency == 0)
                {
                    _soundSource.pitch = Random.Range(0.9f, 1.1f);
                    _soundSource.PlayOneShot(sentence.characterVoice);
                }
            }
            yield return waitInst;
        }

        if (sentence.isShaking)
        {
            _dialogTextJitter.enabled = false;
        }

        _buttonNext.enabled = true;
        sentence.callbackEventOnEnd.Invoke();
        _dialogText.maxVisibleCharacters = 99999;
    }

    private IEnumerator ShowSentence_WordByWord(DialogSentence sentence)
    {
        if (sentence.isShaking)
        {
            _dialogTextJitter.enabled = true;
        }

        _characterName_loc.ChangeTextVariants(sentence.characterName_rus, sentence.characterName_eng);

        _dialogText.maxVisibleWords = 0;

        _dialogText_loc.ChangeTextVariants(sentence.sentenceText_rus, sentence.sentenceText_eng);

        float waitTime = 1f / sentence.talkSpeed;
        YieldInstruction waitInst = new WaitForSeconds(waitTime);


        for (int i = 0; i < _dialogText.text.Split(' ').Length; i++)
        {
            _dialogText.maxVisibleWords++;

            if (sentence.characterVoice != null)
            {
                if (i % sentence.voiceFrequency == 0)
                {
                    _soundSource.pitch = Random.Range(0.9f, 1.1f);
                    _soundSource.PlayOneShot(sentence.characterVoice);
                }
            }
            yield return waitInst;
        }

        if (sentence.isShaking)
        {
            _dialogTextJitter.enabled = false;
        }

        _buttonNext.enabled = true;
        sentence.callbackEventOnEnd.Invoke();
        _dialogText.maxVisibleWords = 99999;
    }
}