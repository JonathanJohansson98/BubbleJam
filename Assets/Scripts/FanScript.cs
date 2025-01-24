using UnityEngine;

public class FanScript : MonoBehaviour
{
    public GameObject targetObject;
    public float forceMagnitude = 10f;

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 clickPosition = Input.mousePosition;
            Vector3 targetScreenPosition = Camera.main.WorldToScreenPoint(targetObject.transform.position);

            Vector3 direction = clickPosition - targetScreenPosition;

            Rigidbody targetRigidbody = targetObject.GetComponent<Rigidbody>();
            if (targetRigidbody != null)
            {
                direction.z = 0;
                Vector3 force = -direction.normalized * forceMagnitude;
                targetRigidbody.AddForce(force, ForceMode.Impulse);
            }
        }
    }
}
