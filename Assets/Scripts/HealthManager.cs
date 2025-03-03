using UnityEngine;

public class HealthManager : MonoBehaviour
{
    private int currentHealth;
    public int maxHealth = 1;

    public GameObject playerCharacter;
    private int latestCheckpoint;

    [SerializeField] private SpriteRenderer rend;
    private TrailRenderer trailLine;
    private ParticleSystem trailParticle;
    [SerializeField] private SoundManager sound;
    private int soundBuffer = 1500;

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
        trailLine = GetComponentInChildren<TrailRenderer>();
        trailParticle = GetComponentInChildren<ParticleSystem>();
        Cursor.visible = false;
    }
    void FixedUpdate()
    {
        if (currentHealth <= 0)
        {
            Vector3 gravity = globalGravity * gravityScale * Vector3.up;
            rb.AddForce(gravity, ForceMode.Impulse);
        }

        if (sound != null && sound.audioSource != null) 
        {
            soundBuffer++;
            if (!sound.audioSource.isPlaying && soundBuffer > 1500)
            {
                sound.PlayRandomSound("Music");
            }
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
                    Cursor.visible = false;
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
        else if (other.CompareTag("Progress"))
        {
            if (soundBuffer > 1500)
            {
                sound.PlayRandomSound("Motivational");
            }
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
        if (currentHealth >= 0)
        {
            currentHealth = 0;
        }
        if (soundBuffer > 1500)
        {
            //sound.StopAllSound();
            soundBuffer = 0;
            sound.PlayRandomSound("Demotivational");
        }
        sound.PlaySound("Death", 0);
        if (fanScript.playerAlive)
        {
            playerCharacter.transform.position += new Vector3(0, 0, -4);
        }

        fanScript.playerAlive = false;
        rb.useGravity = false;
        if (rend != null)
        {
            rend.enabled = false;
        }

        Cursor.visible = true;
        bubblePopAnimator.SetTrigger("Pop");
    }

    public void Death()
    {
        if (rend != null)
        {
            rend.enabled = false;
        }
        Debug.Log("Death() runs");
    }

    private void restoreControl()
    {
        currentHealth = maxHealth;
        Debug.Log("Restore Control");
        fanScript.playerAlive = true;
        playerCharacter.transform.position = new Vector3(playerCharacter.transform.position.x, playerCharacter.transform.position.y, 0);
        rb.useGravity = true;
        if (rend != null) rend.enabled = true;
        if (trailLine != null) trailLine.enabled = true;
        if (trailParticle != null) trailParticle.Play();
    }
}
