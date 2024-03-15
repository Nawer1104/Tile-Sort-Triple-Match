using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    private bool _dragging = true;

    private Vector2 _offset;

    public static bool mouseButtonReleased;

    private void Awake()
    {

    }

    private void OnMouseDown()
    {
        _dragging = true;

        _offset = GetMousePos() - (Vector2)transform.position;
    }

    private void OnMouseDrag()
    {
        if (!_dragging) return;

        var mousePosition = GetMousePos();

        transform.position = mousePosition - _offset;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision != null && collision.gameObject.GetComponent<Base>() != null)
        {
            if (collision.gameObject.GetComponent<Base>().objectList.Contains(this)) return;

            if (collision.gameObject.GetComponent<Base>().objectList.Count < 3)
            {
                _dragging = false;

                collision.gameObject.GetComponent<Base>().AddObject(this);
                return;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision != null && collision.gameObject.GetComponent<Base>() != null)
        {
            collision.gameObject.GetComponent<Base>().RemoveObject(this);
        }
    }

    private void OnMouseUp()
    {
        _dragging = false;

        mouseButtonReleased = true;
    }

    private Vector2 GetMousePos()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}