using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KeyDownEvent : MonoBehaviour
{
    public UnityEvent keyDownR;
    [SerializeField] private GameObject cubeToColor;
    private SpriteRenderer cubeSpriteRenderer;

    private void Start() 
    {
        if (keyDownR == null)
        {
            keyDownR = new UnityEvent();
        }

        keyDownR.AddListener(OnPressKeyR);

        cubeSpriteRenderer = cubeToColor.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            keyDownR.Invoke();
        }
    }
    private void OnPressKeyR()
    {
        cubeSpriteRenderer.color = new Color(Random.value, Random.value, Random.value);
    }
}