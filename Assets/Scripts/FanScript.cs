using Unity.VisualScripting;
using UnityEngine;

public class FanScript : MonoBehaviour
{
    [SerializeField] private GameObject targetObject;
    [SerializeField] private Rigidbody targetRigidBody;

    public bool playerAlive = true;

    [SerializeField,Range(1, 300)] private float forceMagnitude = 10f;

    Vector3 clickPosition;
    Vector3 targetScreenPosition;
    float ClickDistanceFromBubbleCenter;
    Vector3 forceDirection;
    Vector3 force;
    [SerializeField] float maximumForce = -3;
    [SerializeField] float minimumForce = 3;

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            clickPosition = Input.mousePosition;
            targetScreenPosition = Camera.main.WorldToScreenPoint(targetObject.transform.position);
            ClickDistanceFromBubbleCenter = (clickPosition - targetScreenPosition).magnitude;

            forceDirection = clickPosition - targetScreenPosition;

            if (targetRigidBody != null)
            {
                var forceExert = Mathf.Max(maximumForce, forceMagnitude / ClickDistanceFromBubbleCenter);
                forceExert = Mathf.Min(minimumForce, forceMagnitude / ClickDistanceFromBubbleCenter);
                forceDirection.z = 0;
                force = -forceDirection.normalized * forceExert;
                Debug.Log("Force: " + force);
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

            if (force == Vector3.zero)
                return;
            targetRigidBody.AddForce(force, ForceMode.Impulse);
    }
}
