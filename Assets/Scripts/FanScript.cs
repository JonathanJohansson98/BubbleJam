using Unity.VisualScripting;
using UnityEngine;

public class FanScript : MonoBehaviour
{
    [SerializeField] private GameObject targetObject;
    [SerializeField] private Rigidbody targetRigidBody;
    [SerializeField] private AnimationCurve fanForceCurve;

    public bool playerAlive = true;

    //[SerializeField, Range(1, 300)] private float forceMagnitude = 10f;

    Vector3 clickPosition;
    Vector3 targetScreenPosition;
    float ClickDistanceFromBubbleCenter;

    Vector3 forceDirection;
    Vector3 force;
    [SerializeField] float maximumForce = -3;
    //[SerializeField] float minimumForce = 3;

    //[SerializeField] float offset = 3;

    [SerializeField, Range(0.1f, 5f)] private float rotationSmoothing = 1f;
    private float currentRotationSpeed = 0f;
    private int rotationDirection = 1; // 1 for clockwise, -1 for counterclockwise

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // On mouse press, decide rotation direction
        {
            rotationDirection = Random.value > 0.5f ? 1 : -1;
        }

        if (Input.GetMouseButton(0))
        {
            clickPosition = Input.mousePosition;
            targetScreenPosition = Camera.main.WorldToScreenPoint(targetObject.transform.position);
            ClickDistanceFromBubbleCenter = (clickPosition - targetScreenPosition).magnitude;

            forceDirection = clickPosition - targetScreenPosition;

            if (targetRigidBody != null)
            {
                var forceExert = maximumForce * - fanForceCurve.Evaluate(ClickDistanceFromBubbleCenter);
                //var forceExert = Mathf.Max(maximumForce, forceMagnitude / ClickDistanceFromBubbleCenter);
                //forceExert = Mathf.Min(minimumForce, forceMagnitude / ClickDistanceFromBubbleCenter);
                forceDirection.z = 0;
                force = -forceDirection.normalized * forceExert;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            clickPosition = Vector3.zero;
            targetScreenPosition = Vector3.zero;
            ClickDistanceFromBubbleCenter = 0;

            force = Vector3.zero;
        }
    }

    private void FixedUpdate()
    {
        if (!playerAlive)
            return;

        if (force != Vector3.zero)
        {
            targetRigidBody.AddForce(force, ForceMode.Impulse);
            StartRotating(force.magnitude);
        }
        else
        {
            StopRotating();
        }

        RotateTarget();
    }

    private void StartRotating(float appliedForceMagnitude)
    {
        float targetSpeed = Mathf.Clamp(appliedForceMagnitude, 0f, 100f);
        currentRotationSpeed = Mathf.Lerp(currentRotationSpeed, targetSpeed, Time.fixedDeltaTime * rotationSmoothing);
    }

    private void StopRotating()
    {
        currentRotationSpeed = Mathf.Lerp(currentRotationSpeed, 0f, Time.fixedDeltaTime * rotationSmoothing);
    }

    private void RotateTarget()
    {
        if (currentRotationSpeed > 0.01f)
        {
            targetObject.transform.Rotate(0, 0, rotationDirection * currentRotationSpeed * Time.fixedDeltaTime * 100);
        }
        else
        {
            currentRotationSpeed = 0f; 
        }
    }
}
