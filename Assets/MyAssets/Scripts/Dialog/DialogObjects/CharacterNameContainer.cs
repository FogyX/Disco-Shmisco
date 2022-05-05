using UnityEngine;
using TMPro;

public class CharacterNameContainer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _containedCharName;

    public TextMeshProUGUI ContainedCharName
    {
        get
        {
            if (_containedCharName == null)
            {
                Debug.LogError("There is no character name object attached to the container!");
            }

            return _containedCharName;
        }
    }
}