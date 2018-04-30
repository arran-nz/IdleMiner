using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Sprite buttom with Unity Event and text object
/// </summary>
[RequireComponent(typeof(BoxCollider2D))]
public class SpriteButton : MonoBehaviour {

    private TextMesh textMesh;

    public UnityEvent MouseDown;

    public void SetText(string text)
    {
        textMesh.text = text;
    }

    private void Awake()
    {
        textMesh = GetComponentInChildren<TextMesh>();

        if (MouseDown == null)
        {
            MouseDown = new UnityEvent();
        }
    }

    private void OnMouseDown()
    {
        // Don't register mouse down event when UI Panels are active.
        if (!GameController.Instance.UI.IsScreenCovered)
        {
            MouseDown.Invoke();
        }
    }
}
