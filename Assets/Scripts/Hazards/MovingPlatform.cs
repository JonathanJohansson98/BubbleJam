using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public AnimationCurve movementCurve;
    public List<Vector3> points;
    public float duration = 2.0f;

    void Start()
    {
        if (points == null || points.Count < 1)
        {
            Debug.LogWarning("No points in list");
            return;
        }

        points.Insert(0, transform.position);

        MovePlatform();
    }

    void MovePlatform()
    {
        Sequence sequence = DOTween.Sequence();
        for (int i = 0; i < points.Count; i++)
        {
            Vector3 targetPoint = points[i];
            sequence.Append(transform.DOMove(targetPoint, duration / points.Count).SetEase(movementCurve));
        }
        sequence.SetLoops(-1, LoopType.Yoyo);
    }

    void OnDrawGizmosSelected()
    {
        if (points == null || points.Count < 1) return;

        Gizmos.color = Color.green;
        for (int i = 0; i < points.Count - 1; i++)
        {
            Gizmos.DrawLine(points[i], points[i + 1]);
        }

        Gizmos.color = Color.red;
        foreach (var point in points)
        {
            Gizmos.DrawSphere(point, 0.1f);
        }
    }
}
