using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class introAudio : MonoBehaviour
{
    public Transform listenerTransform;
    public AudioSource audioSource;
    public float minDist = 1f;
    public float maxDist = 15f;
    public GameObject introText;

    // Reference to the GameUI script (set this via the Inspector)
    public GameUI gameUI;

    void Update()
    {
        float dist = Vector3.Distance(transform.position, listenerTransform.position);

        if (dist < minDist)
        {
            audioSource.volume = 1;
        }
        else if (dist > maxDist)
        {
            audioSource.volume = 0;
        }
        else
        {
            audioSource.volume = 1 - ((dist - minDist) / (maxDist - minDist));
        }
    }

    public void StopAudio()
    {
        audioSource.Stop();
        introText.SetActive(false);
        
        gameUI.DisableRestartTextLayer();
    }
}
