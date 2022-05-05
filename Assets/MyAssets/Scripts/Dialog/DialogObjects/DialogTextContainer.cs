using UnityEngine;
using TMPro;

public class DialogTextContainer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _containedDialogText;

    public TextMeshProUGUI ContainedDialogText
    {
        get
        {
            if (_containedDialogText == null)
            {
                Debug.LogError("There is no text object attached to the container!");
            }

            return _containedDialogText;
        }
    }
}
