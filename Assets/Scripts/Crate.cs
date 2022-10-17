using System;
using UnityEngine;

public class Crate : MonoBehaviour
{
    [SerializeField] private GameObject _woodParticlePrefab;

    [SerializeField] private AudioSource audioSource;

    bool isCrateDestroyed = false;

    void OnCollisionEnter2D(Collision2D collision)
    {
        

        Bird bird = collision.collider.GetComponent<Bird>();
        if (bird != null)
        {
            Debug.Log("Bird Killed Crate");
            CheckIfDead();
        }

        Crate crate = collision.collider.GetComponent<Crate>();
        if (crate != null)
        {
            // Debug.Log("Crate Killed Crate");
        }

        Monster monster = collision.collider.GetComponent<Monster>();
        if (monster != null && transform.rotation.z < -0.5 || transform.rotation.z > 0.5)
        {
            Debug.Log("Monster Killed Crate");
            CheckIfDead();
        }

        Ground ground = collision.collider.GetComponent<Ground>();
        if (ground != null && collision.contacts[0].normal.y < -0.5 || collision.contacts[0].normal.x < -0.5 
                            && transform.rotation.z < -0.5 || transform.rotation.z > 0.5)
        {            
            Debug.Log("Ground Killed Crate");
            CheckIfDead();
        }

        if (collision.gameObject.tag == "CollisionTag" && isCrateDestroyed)
        {
            audioSource.Play();
        }
    }

    private void CheckIfDead()
    {
        isCrateDestroyed = true;
        Instantiate(_woodParticlePrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
