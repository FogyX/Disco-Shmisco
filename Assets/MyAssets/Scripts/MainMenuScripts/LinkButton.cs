using UnityEngine;

[RequireComponent(typeof(UnityEngine.UI.Button))]
public class LinkButton: MonoBehaviour
{
    [SerializeField] string linkToGo;

    public void LinkTouch()
    {
        if (!string.IsNullOrEmpty(linkToGo))
        {
            Application.OpenURL(linkToGo);
        }
    }
}
