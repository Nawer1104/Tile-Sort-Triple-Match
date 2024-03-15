using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obj : MonoBehaviour
{
    public int type;

    bool isClicked;

    private void OnMouseDown()
    {
        if (isClicked) return;

        isClicked = true;

        GameManager.Instance.levels[GameManager.Instance.GetCurrentIndex()].DestroyObjectsByType(gameObject);
    }

    public void Destroy()
    {
        GameManager.Instance.levels[GameManager.Instance.GetCurrentIndex()].gameObjects.Remove(gameObject);
        gameObject.SetActive(false);

        GameManager.Instance.CheckLevelUp();
    }
}
