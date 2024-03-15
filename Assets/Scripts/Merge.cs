using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Merge : MonoBehaviour
{
    int ID;
    Transform Block1;
    Transform Block2;
    public float Distance;
    public float MergeSpeed;
    public GameObject vfxMerge;
    bool CanMerge;

    void Start()
    {
        ID = GetInstanceID();
    }

    private void FixedUpdate()
    {
        MoveTowards();
    }
    public void MoveTowards()
    {
        if (CanMerge)
        {
            transform.position = Vector2.MoveTowards(Block1.position, Block2.position, MergeSpeed);
            if (Vector2.Distance(Block1.position, Block2.position) < Distance)
            {
                if (ID < Block2.gameObject.GetComponent<Merge>().ID) { return; }
                GameObject vfx = Instantiate(vfxMerge, transform.position, Quaternion.identity) as GameObject;

                Destroy(vfx, 1f);
/*                GameManager.Instance.levels[GameManager.Instance.GetCurrentIndex()].gameObjects.Remove(gameObject);
                GameManager.Instance.levels[GameManager.Instance.GetCurrentIndex()].gameObjects.Remove(Block2.gameObject);*/
                Destroy(Block2.gameObject);
                Destroy(gameObject);
                /*                O.AddComponent<Rigidbody2D>();
                                O.GetComponent<Rigidbody2D>().gravityScale = 0;
                                O.GetComponent<Rigidbody2D>().freezeRotation = true;*/

                GameManager.Instance.CheckLevelUp();
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Block"))
        {
            if (collision.gameObject.GetComponent<SpriteRenderer>().sprite == GetComponent<SpriteRenderer>().sprite)
            {
                Block1 = transform;
                Block2 = collision.transform;
                CanMerge = true;
                Destroy(collision.gameObject.GetComponent<Rigidbody2D>());
                Destroy(GetComponent<Rigidbody2D>());
            }
        }

    }
}