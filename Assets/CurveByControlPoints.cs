﻿using System.Collections;
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
    private GameObject arrow;
    public GameObject arrowPrefab;
    public int numCircles;

    public void Start()
    {
        drawnLineObjects = new GameObject[numCircles];

        for(var i = 0; i < numCircles; i++)
        {
            drawnLineObjects[i] = Instantiate(circle);
            drawnLineObjects[i].transform.SetParent(canvas.transform);
            float t = (1 / (float) numCircles) * i;
            float onCurve = Mathf.Sqrt(t);
            drawnLineObjects[i].transform.localScale = new Vector3(onCurve, onCurve);
            drawnLineObjects[i].SetActive(false);
        }
        arrow = Instantiate(arrowPrefab);
        arrow.SetActive(false);
        arrow.transform.SetParent(canvas.transform);

        controlPoints = new Vector3[4];
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y);
        }
        if (Input.GetMouseButton(0))
        {
            Vector3 endPos = Input.mousePosition;

            controlPoints[0] = startPos;
            controlPoints[1] = new Vector3(startPos.x, endPos.y);
            controlPoints[2] = new Vector3((endPos.x - startPos.x) / 2 + startPos.x, endPos.y);
            controlPoints[3] = endPos;
            for (int i = 0; i < numCircles; i += 1)
            {
                float t = (1 / (float)numCircles) * i;
                Vector2 circlePosition = Mathf.Pow(1 - t, 3) * controlPoints[0] +
                    3 * Mathf.Pow(1 - t, 2) * t * controlPoints[1] +
                    3 * (1 - t) * Mathf.Pow(t, 2) * controlPoints[2] +
                    Mathf.Pow(t, 3) * controlPoints[3];

                drawnLineObjects[i].SetActive(true);
                drawnLineObjects[i].transform.position = circlePosition;
            }
            arrow.transform.right = endPos - drawnLineObjects[20].transform.position;
            arrow.transform.position = endPos;
            arrow.SetActive(true);
        }
        if (Input.GetMouseButtonUp(0))
        {
            foreach (GameObject obj in drawnLineObjects)
            {
                obj.SetActive(false);
            }
            arrow.SetActive(false);
        }
    }
}
