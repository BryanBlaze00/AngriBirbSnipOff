using System;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField] private GameObject _cloudParticlePrefab;
    [SerializeField] private Sprite _deadMonster;
    [SerializeField] private AudioSource audioSource;

    bool isMonsterDead = false;

    void OnCollisionEnter2D(Collision2D collision)
    {
        Bird bird = collision.collider.GetComponent<Bird>();
        if (bird != null)
        {
            Debug.Log("Bird Killed Monster");
            CheckIfDead();
        }

        Monster monster = collision.collider.GetComponent<Monster>();
        if (monster != null)
        {
            // Debug.Log("Monster Killed Monster");
        }

        Crate crate = collision.collider.GetComponent<Crate>();
        if (crate != null && transform.rotation.z < -0.5 || transform.rotation.z > 0.5)
        {
            Debug.Log("Crate Killed Monster");
            CheckIfDead();
        }

        Ground ground = collision.collider.GetComponent<Ground>();
        if (ground != null && collision.contacts[0].normal.y < -0.5 || collision.contacts[0].normal.x < -0.5
                                                && transform.rotation.z < -0.5 || transform.rotation.z > 0.5)
        {
            Debug.Log("Ground Killed Monster");
            CheckIfDead();
        }

        if (collision.gameObject.tag == "CollisionTag" && isMonsterDead)
        {
            CheckIfDead();
        }
    }

    private void CheckIfDead()
    {
        if (!isMonsterDead)
        {
            Instantiate(_cloudParticlePrefab, transform.position, Quaternion.identity);
            GetComponent<SpriteRenderer>().sprite = _deadMonster;
            GetComponent<Animator>().enabled = false;
            audioSource.Play();
        }
        isMonsterDead = true;
    }
}
