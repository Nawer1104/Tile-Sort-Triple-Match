using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawWithMouse : MonoBehaviour
{
    public static DrawWithMouse Instance;

    public LineRenderer line;
    private Vector3 previousPosition;

    [SerializeField]
    private float minDistance = 0.1f;
    [SerializeField, Range(0.1f, 2f)]
    private float width;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        line = GetComponent<LineRenderer>();
        line.positionCount = 1;
        previousPosition = transform.position;
    }

    public void StartLine(Vector2 position)
    {
        line.positionCount = 1;
        line.SetPosition(0, position);
    }

    public void ResetLine()
    {
        line.positionCount = 1;
        line.SetPosition(0, Vector2.zero);
    }

    public void Updateline()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 currentPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            currentPosition.z = 0f;

            if (Vector3.Distance(currentPosition, previousPosition) > minDistance)
            {
                if (previousPosition == transform.position)
                {
                    //it means its the first point
                    line.SetPosition(0, currentPosition);
                }
                else
                {
                    line.positionCount++;
                    line.SetPosition(line.positionCount - 1, currentPosition);
                }

                previousPosition = currentPosition;

            }
        }
    }
}