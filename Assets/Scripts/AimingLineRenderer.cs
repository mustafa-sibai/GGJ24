using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimingLineRenderer : MonoBehaviour
{
    [SerializeField] MonkeyController monkeyController;
    LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        lineRenderer.SetPositions(new Vector3[]
        {
            transform.position,
            monkeyController.aimingSphere.transform.position
        });
    }
}