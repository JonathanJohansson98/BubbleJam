using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ParticleOnCollision : MonoBehaviour
{
    ParticleSystem particle;

    private void OnCollisionEnter(Collision collision)
    {
        ParticleSystem particle = GetComponent<ParticleSystem>();
        particle.transform.position = collision.contacts[0].point;
        particle.Play();
    }
}
