using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Lazer : MonoBehaviour
{
    [SerializeField] private float lazerWidth = 1;
    [SerializeField] private float lazerLengthModifier = 100;
    private float lazerLength = 1;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private float laserBaseOffset;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private AnimationCurve laserLenthCurve;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Start()
    {
        UpdateLaser();
    }

    private void Update()
    {
        UpdateLaser();

        var hit = Physics.Raycast(transform.position, transform.up, out RaycastHit hitInfo, lazerLength, layerMask);
        Debug.Log($"Lazer length {evaluatedLength}");


        if (!hit)
            return;

        Debug.LogError(hitInfo.transform.name);

        if (hitInfo.collider.CompareTag("Player"))
        {
            hitInfo.collider.attachedRigidbody.gameObject.GetComponent<HealthManager>().Die();
        }
    }

    float evaluatedLength = 1;

    private void UpdateLaser()
    {
        evaluatedLength = laserLenthCurve.Evaluate(Time.time % laserLenthCurve.keys[laserLenthCurve.length - 1].time);
        lazerLength = evaluatedLength * lazerLengthModifier;

        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, transform.position - transform.right * lazerWidth / 25);
        lineRenderer.SetPosition(1, transform.position - transform.right * lazerWidth / 25 + transform.up * lazerLength);
        lineRenderer.startWidth = lazerWidth;
        lineRenderer.endWidth = lazerWidth;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.up * lazerLength);
    }
}
