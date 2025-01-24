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
            Debug.Log("Player reached the checkpoint!");
        }
    }

    public void HealthUpdate()
    {
        if (currentHealth <= 0)
        {
            Die();
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

      RespawnPlayerAtCheckpoint();
      yield return new WaitForSeconds(1f);
      yield return null;
    }

    private void RespawnPlayerAtCheckpoint()
    {
            playerCharacter.transform.position = currentCheckpoint.transform.position;

            currentHealth = maxHealth;

            if (rend != null) rend.enabled = true;
            if (col != null) col.enabled = true;
    }
}
