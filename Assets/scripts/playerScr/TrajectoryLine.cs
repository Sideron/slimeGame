using System;
using UnityEngine;

public class TrajectoryLine : MonoBehaviour
{
    public Transform launchPoint;      // Desde dónde se lanza
    public float launchSpeed = 10f;    // Velocidad inicial
    public float angle = 45f;          // Ángulo de lanzamiento en grados
    public int resolution = 30;        // Cantidad de puntos de la línea

    private LineRenderer lineRenderer;
    private float gravity;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        gravity = 40;

        DrawTrajectory();
    }

    public void DrawTrajectory()
    {
        Vector3[] points = new Vector3[resolution];
        float rad = angle * Mathf.Deg2Rad;
        float sin = Mathf.Sin(rad);

        float totalTime = 2f * launchSpeed * Mathf.Sign(sin) / gravity;

        for (int i = 0; i < resolution; i++)
        {
            float t = i * totalTime / resolution;
            float x = launchSpeed * t * Mathf.Cos(rad) * (sin < 0 ? -1 : 1);
            float y = launchSpeed * t * Mathf.Abs(sin) - 0.5f * gravity * t * t;

            points[i] = launchPoint.position + new Vector3(x, y, 0) - transform.parent.position;
        }

        lineRenderer.positionCount = resolution;
        lineRenderer.SetPositions(points);
    }
}
