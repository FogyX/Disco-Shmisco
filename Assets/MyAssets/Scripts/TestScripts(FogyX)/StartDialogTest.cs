using UnityEngine;
using UnityEngine.Events;

public class StartDialogTest : MonoBehaviour
{
    public UnityEvent startDialog = new UnityEvent();

    public void StartDialog()
    {
        startDialog.Invoke();
    }
}
