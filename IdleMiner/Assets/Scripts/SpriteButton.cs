using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider2D))]
public class SpriteButton : MonoBehaviour {

    private TextMesh textMesh = null;

    public UnityEvent MouseDown;

    public void SetText(string text)
    {
        if (textMesh != null)
        {
            textMesh.text = text;
        }
        else
        {
            textMesh = GetComponentInChildren<TextMesh>();
            SetText(text);
        }
    }

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
