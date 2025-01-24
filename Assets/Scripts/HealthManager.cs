using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    private int currentHealth;
    public int maxHealth = 1;

    public GameObject playerCharacter;
    //public GameObject currentCheckpoint;
    private int latestCheckpoint;

    private Renderer rend;
    private Collider col;

    void Start()
    {
        currentHealth = maxHealth;
        rend = GetComponent<Renderer>();
        col = GetComponent<Collider>();
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
                    restoreControl();
                }
            }
        } 
        else if (other.CompareTag("Hazard"))
        {
            currentHealth = currentHealth - 1;
            HealthUpdate();
            Debug.Log("YOU DIED TO" + other.gameObject.name);
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

    private void Die()
    {

        FanScript fanScript = GetComponent<FanScript>();
        fanScript.playerAlive = false;
        playerCharacter.transform.position += new Vector3(0,0,3);
        //if (rend != null)
        //{
        //    rend.enabled = false;
        //}

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
        FanScript fanScript = GetComponent<FanScript>();
        fanScript.playerAlive = true;
        playerCharacter.transform.position += new Vector3(0, 0, -3);


        if (rend != null) rend.enabled = true;
        if (col != null) col.enabled = true;
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
