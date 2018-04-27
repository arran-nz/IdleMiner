using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider2D))]
public class SpriteButton : MonoBehaviour {

    public UnityEvent MouseDown;

    private void Awake()
    {
        if (MouseDown == null)
        {
            MouseDown = new UnityEvent();
        }
    }

    private void OnMouseDown()
    {
        MouseDown.Invoke();
    }
}
