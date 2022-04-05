using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScreenTouchEvent : MonoBehaviour
{
    public UnityEvent screenTouch;
    [SerializeField] private GameObject cubeToColor;
    private SpriteRenderer cubeSpriteRenderer;

    private void Start() 
    {
        if (screenTouch == null)
        {
            screenTouch = new UnityEvent();
        }

        screenTouch.AddListener(OnPressKeyR);

        cubeSpriteRenderer = cubeToColor.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Input.touchCount >= 1 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            screenTouch.Invoke();
        }
    }
    private void OnPressKeyR()
    {
        cubeSpriteRenderer.color = new Color(Random.value, Random.value, Random.value);
    }
}