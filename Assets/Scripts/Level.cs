using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Level : MonoBehaviour
{
    public List<GameObject> collectionTransform = new List<GameObject>();

    public List<GameObject> selectedObjects = new List<GameObject>();

    private List<GameObject> threeMatchedObjects = new List<GameObject>();

    public List<GameObject> gameObjects = new List<GameObject>();

    private int typeToRemove;

    [SerializeField] private GameObject vfxDestroy;

    // Function to handle object selection
    public void SelectObject(GameObject obj)
    {
        // Add the selected object to the list
        selectedObjects.Add(obj);

        obj.transform.DOMove(collectionTransform[selectedObjects.IndexOf(obj)].transform.position, 1f, false).OnComplete(() => {
            if (obj.transform.rotation.z != 0f)
            {
                obj.transform.DORotate(new Vector3(0f, 0f, 0f), 1f);
            }

            obj.GetComponent<Box>().PlayAnimSelected();

            // Check if three objects are selected
            if (selectedObjects.Count >= 3)
            {
                
                // Check if all three selected objects have the same type
                if (AreObjectsMatching(selectedObjects))
                {
                    // Remove the matching objects from the game
                    RemoveMatchedObjects(selectedObjects);
                }
            }
        });
    }

    // Function to check if more than three selected objects have the same type
    private bool AreObjectsMatching(List<GameObject> objects)
    {
        // Dictionary to count the occurrences of each object type
        Dictionary<int, int> typeCounts = new Dictionary<int, int>();

        // Count the occurrences of each object type
        foreach (GameObject obj in objects)
        {
            int objectType = obj.GetComponent<Box>().type;
            if (typeCounts.ContainsKey(objectType))
            {
                typeCounts[objectType]++;
            }
            else
            {
                typeCounts[objectType] = 1;
            }
        }

        // Check if any object type has more than three occurrences
        foreach (KeyValuePair<int, int> pair in typeCounts)
        {
            if (pair.Value >= 3)
            {
                typeToRemove = pair.Key;
                return true;
            }
        }
        return false;
    }

    // Function to remove matched objects from the game
    private void RemoveMatchedObjects(List<GameObject> objects)
    {
        foreach (GameObject obj in objects)
        {
            if (obj.GetComponent<Box>().type == typeToRemove)
            {
                threeMatchedObjects.Add(obj);
            }
        }
        foreach (GameObject obj in threeMatchedObjects)
        {
            obj.GetComponent<Box>().PlayAnimDestroy();
            objects.Remove(obj);
        }
        threeMatchedObjects.Clear();

        foreach (GameObject obj in selectedObjects)
        {
            obj.transform.DOMove(collectionTransform[selectedObjects.IndexOf(obj)].transform.position, 1f, false);
        }
    }

    public void RemoveMatchedObjectsWhenFull(Box box)
    {
        // Dictionary to count the occurrences of each object type
        Dictionary<int, int> typeCounts = new Dictionary<int, int>();

        // Count the occurrences of each object type
        foreach (GameObject obj in selectedObjects)
        {
            int objectType = obj.GetComponent<Box>().type;
            if (typeCounts.ContainsKey(objectType))
            {
                typeCounts[objectType]++;
            }
            else
            {
                typeCounts[objectType] = 1;
            }
        }

        if (typeCounts.ContainsKey(box.type))
        {
            typeCounts[box.type]++;
        }

        // Check if any object type has more than three occurrences
        foreach (KeyValuePair<int, int> pair in typeCounts)
        {
            if (pair.Value >= 3)
            {
                foreach (GameObject obj in selectedObjects)
                {
                    if (obj.GetComponent<Box>().type == box.type)
                    {
                        threeMatchedObjects.Add(obj);
                    }
                }

                foreach (GameObject obj in threeMatchedObjects)
                {
                    obj.GetComponent<Box>().PlayAnimDestroy();
                    selectedObjects.Remove(obj);
                }
                box.PlayAnimDestroy();
                threeMatchedObjects.Clear();

                foreach (GameObject obj in selectedObjects)
                {
                    obj.transform.DOMove(collectionTransform[selectedObjects.IndexOf(obj)].transform.position, 1f, false);
                }
            }
        }

    }

    public void DestroyObjectsByType(GameObject target)
    {
        int targetType = target.GetComponent<Obj>().type;

        Vector2 targetPos = target.transform.position;

        foreach (GameObject obj in gameObjects)
        {
            var a = obj.GetComponent<Obj>();

            if (a.type == targetType)
            {
                a.transform.DOMove(targetPos, 1f, false).OnComplete(() => {
                    a.Destroy();
                });
            }
        }

        GameObject vfx = Instantiate(vfxDestroy, target.transform.position, Quaternion.identity) as GameObject;
        Destroy(vfx, 1f);
    }

}
