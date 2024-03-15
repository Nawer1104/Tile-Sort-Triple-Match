using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Base : MonoBehaviour
{
    [SerializeField] private Transform[] transforms;

    public List<DragAndDrop> objectList = new List<DragAndDrop>();

    [SerializeField] private GameObject vfxDestroy;
    [SerializeField] private GameObject vfxAdded;

    private bool isCompleted;

    Transform baseParent;

    private void Start()
    {
        baseParent = GameManager.Instance.levels[GameManager.Instance.GetCurrentIndex()].transform;
    }

    private void Update()
    {
        if (isCompleted) return;

        if (objectList.Count == 3)
        {
            if (CheckSprites())
            {
                GameObject vfx = Instantiate(vfxDestroy, transform.position, Quaternion.identity) as GameObject;
                Destroy(vfx, 1f);

                GameManager.Instance.levels[GameManager.Instance.GetCurrentIndex()].gameObjects.Remove(gameObject);
                GameManager.Instance.CheckLevelUp();

                gameObject.transform.DOScale(0f, 1f);

                foreach(DragAndDrop obj in objectList)
                {
                    obj.transform.DOScale(0f, 1f);
                }

                isCompleted = true;
            }
        }
    }
     
    private void SetPos()
    {
        foreach(DragAndDrop obj in objectList)
        {
            if(obj != null)
                obj.transform.position = transforms[objectList.IndexOf(obj)].position;
        }
    }

    private bool CheckSprites()
    {
        SpriteRenderer firstSpriteRenderer = objectList[0].GetComponent<SpriteRenderer>();
        Sprite firstSprite = firstSpriteRenderer.sprite;

        bool allHaveSameSprite = true;

        for (int i = 1; i < objectList.Count; i++)
        {
            SpriteRenderer currentSpriteRenderer = objectList[i].GetComponent<SpriteRenderer>();

            if (currentSpriteRenderer == null)
            {
                return false;
            }

            if (currentSpriteRenderer.sprite != firstSprite)
            {
                allHaveSameSprite = false;
                break;
            }
        }

        if (allHaveSameSprite)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void AddObject(DragAndDrop obj)
    {
        if (objectList.Count < 3)
        {

            objectList.Add(obj);

            obj.transform.SetParent(gameObject.transform);

            obj.transform.position = transforms[objectList.IndexOf(obj)].position;

            GameObject vfx = Instantiate(vfxAdded, transform.position, Quaternion.identity) as GameObject;
            Destroy(vfx, 1f);

            SetPos();

            return;
        }
    }

    public void RemoveObject(DragAndDrop obj)
    {
        if (objectList.Contains(obj))
        {
            objectList.Remove(obj);

            obj.transform.SetParent(baseParent);

            SetPos();
        }
        else return;
    }
}
