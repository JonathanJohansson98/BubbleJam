using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    private int currentHealth;
    public int maxHealth = 1;

    public GameObject playerCharacter;
    public GameObject currentCheckpoint;

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
        if (other.CompareTag("Checkpoint"))
        {
            currentCheckpoint = other.gameObject;
            Debug.Log("Player has reached" + currentCheckpoint.name);
        } else if (other.CompareTag("Hazard"))
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
            StartCoroutine(Die());
            Debug.Log("HealthUpdate() runs");
            Debug.Log("Current Health = " + currentHealth);
        }
    }

    public IEnumerator Die() 
    {
    
        if (rend != null)
        {
            rend.enabled = false;
        }

        if (col != null)
        {
            col.enabled = false;
        }
        Debug.Log("Die() runs");
      
      yield return new WaitForSeconds(1f);
      RespawnPlayerAtCheckpoint();
    }

    private void RespawnPlayerAtCheckpoint()
    {
            playerCharacter.transform.position = currentCheckpoint.transform.position;

            currentHealth = maxHealth;
            Debug.Log("RespawnPlayerAtCheckpoint() runs");

            if (rend != null) rend.enabled = true;
            if (col != null) col.enabled = true;
    }
}
