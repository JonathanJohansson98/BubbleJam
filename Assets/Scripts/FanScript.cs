using UnityEngine;

public class FanScript : MonoBehaviour
{
    [SerializeField] private GameObject targetObject;
    [SerializeField] private Rigidbody targetRigidBody;

    [SerializeField,Range(1, 100)] private float forceMagnitude = 10f;

    [SerializeField] private GameObject fanDebugObject;

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
    }

    private void FixedUpdate()
    {
        targetRigidBody.AddForce(force, ForceMode.Impulse);
        fanDebugObject.transform.position = targetObject.transform.position + forceDirection.normalized * ClickDistanceFromBubble / 20;
    }
}
