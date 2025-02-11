using UnityEngine;

public class stopIntroAudio : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {            
            introAudio audioScript = FindObjectOfType<introAudio>(); // Find the audio script
            if (audioScript != null)
            {
                audioScript.StopAudio();
            }
        }
    }
}
