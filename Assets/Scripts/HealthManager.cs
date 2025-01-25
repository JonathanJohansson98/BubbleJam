using UnityEngine;

public class HealthManager : MonoBehaviour
{
    private int currentHealth;
    public int maxHealth = 1;

    public GameObject playerCharacter;
    private int latestCheckpoint;

    private Renderer rend;
    private Collider col;
    private TrailRenderer trail;
    private ParticleSystem part;

    [SerializeField] private Animator bubblePopAnimator;
    [SerializeField] private FanScript fanScript;


    // Gravity Scale editable on the inspector
    // providing a gravity scale per object

    public float gravityScale = 1.0f;

    // Global Gravity doesn't appear in the inspector. Modify it here in the code
    // (or via scripting) to define a different default gravity for all objects.

    public static float globalGravity = -1.81f;

    [SerializeField] Rigidbody rb;

    void Start()
    {
        currentHealth = maxHealth;
        rend = GetComponent<Renderer>();
        col = GetComponent<Collider>();
        trail = GetComponentInChildren<TrailRenderer>();
        part = GetComponentInChildren<ParticleSystem>();
    }
    void FixedUpdate()
    {
        if (currentHealth <= 0)
        {
            Vector3 gravity = globalGravity * gravityScale * Vector3.up;
            rb.AddForce(gravity, ForceMode.Impulse);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        SafeZoneManager checkpoint = other.GetComponent<SafeZoneManager>();
        if (other.CompareTag("Checkpoint"))
        {
            if (currentHealth > 0)
            {
                latestCheckpoint = checkpoint.thisSafeZone;
                Debug.Log("Player has reached checkpoint: " + checkpoint.thisSafeZone);
            }
            else
            {
                if (checkpoint.thisSafeZone != latestCheckpoint)
                {
                    Debug.Log("Player has respawned at checkpoint: " + checkpoint.thisSafeZone);
                    latestCheckpoint = checkpoint.thisSafeZone;
                    restoreControl();
                }
            }
        } 
        else if (other.CompareTag("Hazard"))
        {
            currentHealth = currentHealth - 1;
            HealthUpdate();
            Debug.Log("YOU DIED TO " + other.gameObject.name);
        }
    }

    public void HealthUpdate()
    {
        if (currentHealth <= 0)
        {
            //play animations or sounds
            Die();
            Debug.Log("HealthUpdate() runs");
            Debug.Log("Current Health = " + currentHealth);
        }
    }

    //[ContextMenu("Die")]
    public void Die()
    {
        bubblePopAnimator.SetTrigger("Pop");
    }

    public void Death()
    {
        fanScript.playerAlive = false;
        playerCharacter.transform.position += new Vector3(0, 0, -4);
        rb.useGravity = false;
        if (rend != null)
        {
            rend.enabled = false;
            trail.enabled = false;
            part.Stop();
        }

        //if (col != null)
        //{
        //    col.enabled = false;
        //}
        Debug.Log("Die() runs");
        //RespawnPlayerAtCheckpoint();
        //yield return new WaitForSeconds(1f);
        //yield return null;
    }

    private void RespawnPlayerAtCheckpoint()
    {
            //playerCharacter.transform.position = currentCheckpoint.transform.position;

            currentHealth = maxHealth;
            Debug.Log("RespawnPlayerAtCheckpoint() runs");

            if (rend != null) rend.enabled = true;
            if (col != null) col.enabled = true;
    }

    private void restoreControl()
    {
        currentHealth = maxHealth;
        Debug.Log("Restore Control");
        fanScript.playerAlive = true;
        playerCharacter.transform.position += new Vector3(0, 0, 4);
        rb.useGravity = true;
        if (rend != null) rend.enabled = true;
        if (trail != null) trail.enabled = true;
        if (part != null) part.Play();


        //if (rend != null) rend.enabled = true;
        //if (col != null) col.enabled = true;
    }

    //public GameObject playerCharacter;
    //public GameObject safeZone;
    //private int latestSafeZone;
    //[SerializeField] private int thisSafeZone;


    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.GetComponent<HealthManager>().currentHealth > 0)
    //    {
    //        latestSafeZone = thisSafeZone;
    //    }
    //    else
    //    {
    //        //restoreControl();
    //    }
    //}
}
