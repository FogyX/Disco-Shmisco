using UnityEngine;
using UnityEditor;
using TMPro;

[CustomEditor(typeof(LocalizableText))]
[CanEditMultipleObjects]
public class LocalizableTextEditor : Editor
{
    private LocalizableText localizableText;
    private TextMeshProUGUI tmp;

    private void OnEnable()
    {
        localizableText = (LocalizableText)target;
        tmp = localizableText.gameObject.GetComponent<TextMeshProUGUI>();
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Set this text to Rus in the editor"))
        {
            SetToRusEditorSingle();
        }
        if (GUILayout.Button("Set this text to Eng in the editor"))
        {
            SetToEngEditorSingle();
        }
        if (GUILayout.Button("Set all selected to Rus in the editor"))
        {
            SetToRusEditorGlobally();
        }
        if (GUILayout.Button("Set all selected to Eng in the editor"))
        {
            SetToEngEditorGlobally();
        }
    }

    private void SetToRusEditorSingle()
    {
        tmp.text = localizableText.russianVariant;
        tmp.fontSize = localizableText.russianFontSize;
    }

    private void SetToEngEditorSingle()
    {
        tmp.text = localizableText.englishVariant;
        tmp.fontSize = localizableText.englishFontSize;
    }


    private void SetToRusEditorGlobally()
    {
        foreach (GameObject go in Selection.gameObjects)
        {
            TextMeshProUGUI tmp = go.GetComponent<TextMeshProUGUI>();
            LocalizableText lt = go.GetComponent<LocalizableText>();

            tmp.text = lt.russianVariant;
            tmp.fontSize = lt.russianFontSize;
        }
    }

    private void SetToEngEditorGlobally()
    {
        foreach (GameObject go in Selection.gameObjects)
        {
            TextMeshProUGUI tmp = go.GetComponent<TextMeshProUGUI>();
            LocalizableText lt = go.GetComponent<LocalizableText>();

            tmp.text = lt.englishVariant;
            tmp.fontSize = lt.englishFontSize;
        }
    }
}