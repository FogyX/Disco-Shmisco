using UnityEngine;
using UnityEngine.UI;

public class ButtonNextContainer : MonoBehaviour
{
    [SerializeField] private Button _containedButton;

    public Button ContainedButton
    {
        get
        {
            if (_containedButton == null)
            {
                Debug.LogError("There is no button attached to the container!");
            }

            return _containedButton;
        }
    }
}