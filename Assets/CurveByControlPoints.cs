using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurveByMouse : MonoBehaviour
{
    [SerializeField]
    public Vector3[] controlPoints;

    public GameObject circle;
    public Canvas canvas;

    private Vector3 startPos;

    private GameObject[] drawnLineObjects;
    public int numCircles;

    public void Start()
    {
        drawnLineObjects = new GameObject[numCircles];

        for(var i = 0; i < numCircles; i++)
        {
            drawnLineObjects[i] = Instantiate(circle);
            drawnLineObjects[i].transform.SetParent(canvas.transform);
            float t = (1 / (float)numCircles) * i;
            float onCurve = Mathf.Sqrt(t);
            drawnLineObjects[i].transform.localScale = new Vector3(onCurve, onCurve);
        }

        controlPoints = new Vector3[4];
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Vector2 pos;
            // RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, canvas.worldCamera, out pos);
            startPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y);
        }
        if (Input.GetMouseButton(0))
        {
            // Vector2 pos;
            // RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, canvas.worldCamera, out pos);
            Vector3 endPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y);

            controlPoints[0] = startPos;
            controlPoints[1] = new Vector3(startPos.x, endPos.y);
            controlPoints[2] = new Vector3((endPos.x - startPos.x) / 2 + startPos.x, endPos.y);
            controlPoints[3] = endPos;
            Debug.Log("start " + startPos);
            Debug.Log("end " + endPos);
            for (int i = 0; i < numCircles; i += 1)
            {
                float t = (1 / (float)numCircles) * i;
                Vector2 circlePosition = Mathf.Pow(1 - t, 3) * controlPoints[0] +
                    3 * Mathf.Pow(1 - t, 2) * t * controlPoints[1] +
                    3 * (1 - t) * Mathf.Pow(t, 2) * controlPoints[2] +
                    Mathf.Pow(t, 3) * controlPoints[3];

                drawnLineObjects[i].SetActive(true);
                drawnLineObjects[i].transform.position = circlePosition;
                Debug.Log("circle Pos" + circlePosition);
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            foreach (GameObject obj in drawnLineObjects)
            {
                obj.SetActive(false);
            }
        }
    }
}
