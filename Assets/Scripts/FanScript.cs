using UnityEngine;

public class FanScript : MonoBehaviour
{
    [SerializeField] private GameObject targetObject;
    [SerializeField] private Rigidbody targetRigidBody;

    [SerializeField,Range(.1f, 1)] private float forceMagnitude = 10f;

    [SerializeField] private GameObject fanDebugObject;

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 clickPosition = Input.mousePosition;
            Vector3 targetScreenPosition = Camera.main.WorldToScreenPoint(targetObject.transform.position);

            Vector3 direction = clickPosition - targetScreenPosition;

            if (targetRigidBody != null)
            {
                direction.z = 0;
                Vector3 force = -direction.normalized * forceMagnitude;
                targetRigidBody.AddForce(force, ForceMode.Impulse);
            }

            fanDebugObject.transform.position = targetObject.transform.position + direction.normalized * 2;
        }
    }
}
