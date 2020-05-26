using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollectable : MonoBehaviour
{
    private ParticleSystem system;
    
    private void OnTriggerEnter2D(Collider2D other) {
        system = GetComponent<ParticleSystem>();
        if(other.tag=="Projectile")
        {   
            system.Play(true);
            Destroy(other.gameObject);
            //system.Stop();
        }
    }
}
