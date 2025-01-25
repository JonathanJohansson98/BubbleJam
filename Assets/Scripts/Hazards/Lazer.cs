using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Lazer : MonoBehaviour
{
    [SerializeField] private float lazerWidth = 1;
    [SerializeField] private float lazerLength = 100;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private BoxCollider laserColliderArea;
    [SerializeField] private float laserBaseOffset;
    [SerializeField] private LayerMask layerMask;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Start()
    {
        laserColliderArea.size = new Vector3(lazerWidth / 10, lazerLength);
        laserColliderArea.center = transform.up * laserBaseOffset;

        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, transform.position - transform.right * lazerWidth / 25);
        lineRenderer.SetPosition(1, transform.position - transform.right * lazerWidth / 25 + transform.up * lazerLength);
        lineRenderer.startWidth = lazerWidth;
        lineRenderer.endWidth = lazerWidth;
    }

    private void Update()
    {
        var hit = Physics.Raycast(transform.position, transform.up, out RaycastHit hitInfo, lazerLength, layerMask);

        if (!hit)
            return;

        Debug.LogError(hitInfo.transform.name);

        if (hitInfo.collider.CompareTag("Player"))
        {
            hitInfo.collider.attachedRigidbody.gameObject.GetComponent<HealthManager>().Die();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.up * lazerLength);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other);
    }
}
