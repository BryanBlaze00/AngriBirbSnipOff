using UnityEngine;

public class SlingSound : MonoBehaviour
{
    [SerializeField] private AudioSource pullSling;
    [SerializeField] private AudioSource releaseSling;

    void OnCollisionEnter2D(Collision2D other)
    {
        pullSling.Play();
    }

    void OnCollisionExit2D(Collision2D other) 
    { 
        pullSling.Stop();
        releaseSling.Play();
    }
}
