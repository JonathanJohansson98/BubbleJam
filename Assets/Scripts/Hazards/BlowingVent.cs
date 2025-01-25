using UnityEngine;

public class BlowingVent : MonoBehaviour
{
    [SerializeField]private Collider affectedTriggerArea;
    [SerializeField,Range(0.1f, 30)] private float forceMagnitude = 10f;
    [SerializeField] string affectedTag = "Player";

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == affectedTag)
        {
            Vector3 direction = other.transform.position - transform.position;
            direction.z = 0;
            other.GetComponent<Rigidbody>().AddForce(direction.normalized * Vector3.Distance(other.transform.position, transform.position)* forceMagnitude, ForceMode.Force);
        }
    }
}
