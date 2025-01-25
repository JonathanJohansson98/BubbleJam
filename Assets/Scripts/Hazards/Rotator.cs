using UnityEngine;

public class RotateGameObject : MonoBehaviour
{
    public float degreesPerFixedUpdate = 0.25f;

    public Vector3 rotationAxis = Vector3.forward;

    void FixedUpdate()
    {
        transform.Rotate(rotationAxis.normalized * degreesPerFixedUpdate);
    }
}