using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Lazer : MonoBehaviour
{
    [SerializeField] private float lazerLength = 100;
    [SerializeField] private Color lazerColor = Color.red;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private Collider laserColliderArea;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }
    private void Start()
    {
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, transform.position + transform.up * lazerLength);   
        lineRenderer.startWidth = laserColliderArea.bounds.size.x;
        lineRenderer.endWidth = laserColliderArea.bounds.size.x;

        lineRenderer.colorGradient = new Gradient()
        {
            colorKeys = new GradientColorKey[]
            {
                new GradientColorKey(lazerColor, 0),
                new GradientColorKey(lazerColor, 1)
            }
        };
    }

    private void Update()
    {
        var hit = Physics.Raycast(transform.position, transform.up, out RaycastHit hitInfo, lazerLength);

        if (!hit)
            return;

        Debug.Log(hitInfo.transform.name);
        
        
        //if (hitInfo)
        //{
        //    Debug.Log(hit);
        //    //lineRenderer.SetPosition(1, hit.point);
        //    //if (hit.collider.tag == "Player")
        //    //{
        //    //    hit.collider.GetComponent<HealthManager>().Die();
        //    //}
        //}
        //else
        //{
        //    lineRenderer.SetPosition(1, transform.position + transform.up * lazerLength);
        //}
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
