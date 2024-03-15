using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public int type;

    private Animator anim;

    public GameObject vfxDestroy;

    public bool isSelected;

    private void Awake()
    {
        anim = GetComponent<Animator>();

        isSelected = false;
    }

    private void Update()
    {
        if (isSelected) return;
    }

    private void OnMouseDown()
    {

        if (isSelected || GameManager.Instance.levels[GameManager.Instance.GetCurrentIndex()].selectedObjects.Count >= GameManager.Instance.levels[GameManager.Instance.GetCurrentIndex()].collectionTransform.Count)
        {
            GameManager.Instance.levels[GameManager.Instance.GetCurrentIndex()].RemoveMatchedObjectsWhenFull(this);
            return;
        }

        isSelected = true;

        GetComponent<BoxCollider2D>().enabled = false;

        GetComponent<Rigidbody2D>().simulated = false;

        GameManager.Instance.levels[GameManager.Instance.GetCurrentIndex()].SelectObject(gameObject);
    }

    public void PlayAnimSelected()
    {
        anim.SetTrigger("Selected");
    }

    public void PlayAnimDestroy()
    {
        anim.SetTrigger("Destroy");
    }

    public void PlayDestroyVfx()
    {
        GameObject vfx = Instantiate(vfxDestroy, transform.position, Quaternion.identity) as GameObject;
        Destroy(vfx, 1f);
        GameManager.Instance.levels[GameManager.Instance.GetCurrentIndex()].gameObjects.Remove(gameObject);
        GameManager.Instance.CheckLevelUp();
    }
}
