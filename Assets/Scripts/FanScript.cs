using Unity.VisualScripting;
using UnityEngine;

public class FanScript : MonoBehaviour
{
    [SerializeField] private GameObject targetObject;
    [SerializeField] private Rigidbody targetRigidBody;
    public bool playerAlive = true;

    [SerializeField,Range(1, 100)] private float forceMagnitude = 10f;

    Vector3 clickPosition;
    Vector3 targetScreenPosition;
    float ClickDistanceFromBubble;
    Vector3 forceDirection;
    Vector3 force;

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            clickPosition = Input.mousePosition;
            targetScreenPosition = Camera.main.WorldToScreenPoint(targetObject.transform.position);
            ClickDistanceFromBubble = (clickPosition - targetScreenPosition).magnitude;

            forceDirection = clickPosition - targetScreenPosition;

            if (targetRigidBody != null)
            {
                forceDirection.z = 0;
                force = -forceDirection.normalized * forceMagnitude / (ClickDistanceFromBubble);
            }

        }

        if (Input.GetMouseButtonUp(0))
        {
            clickPosition = Vector3.zero;
            targetScreenPosition = Vector3.zero;
            ClickDistanceFromBubble = 0;

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

            fanDebugObject.transform.position = fanDebugPosition;
        }
    }
}
