using System;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bird : MonoBehaviour
{
    private Vector3 _initialPosition;
    private bool _birdWasLaunched = false;
    private float _timeSittingAround;
    public Transform birdRestPosition;
    private Rigidbody2D rb;
    
    public BirdState State
    {
        get;
        set;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true;
        State = BirdState.BeforeThrown;
    }

    public void OnThrow()
    {
        rb.isKinematic = false;
        _birdWasLaunched = true;
        State = BirdState.Thrown;
    }

    private void Awake() 
    {
        _initialPosition = new Vector3(birdRestPosition.position.x, 
                                                birdRestPosition.position.y,0);
    }

    private void Update()
    {
        if(_birdWasLaunched && GetComponent<Rigidbody2D>().velocity.magnitude <= 0.1)
        {
            _timeSittingAround += Time.deltaTime;
        }

        if (transform.position.y > 20 || transform.position.y < -20 || 
            transform.position.x > 30 || transform.position.x < -20 ||
            _timeSittingAround > 3)
        {
            String currentSceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentSceneName);
        }
    }
}
