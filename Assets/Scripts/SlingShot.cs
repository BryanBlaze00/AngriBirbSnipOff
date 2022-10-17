using Assets.Scripts;
using UnityEngine;

public class SlingShot : MonoBehaviour
{
    [SerializeField] private AudioSource pullSling;
    [SerializeField] private AudioSource releaseSling;
    public Transform leftSlingOrigin, rightSlingOrigin;
    public LineRenderer leftRubber, rightRubber;
    public Transform birdRestPosition;
    public Bird bird;
    public float throwSpeed;
    public SlingShotState slingShotState;
    private Vector3 slingMiddleVector;

    void Start()
    {
        slingShotState = SlingShotState.Idle;
        leftRubber.SetPosition(0, leftSlingOrigin.position);
        rightRubber.SetPosition(0, rightSlingOrigin.position);

        slingMiddleVector = new Vector3 ((leftSlingOrigin.position.x + 
                                                rightSlingOrigin.position.x) / 2,
                                                (leftSlingOrigin.position.y + 
                                                rightSlingOrigin.position.y) / 2, 0);
    }

    void SetRubbersActive(bool active)
    {
        leftRubber.enabled = active;
        rightRubber.enabled = active;
    }

    void ShowRubbers()
    {
        leftRubber.SetPosition(1, bird.transform.position);
        rightRubber.SetPosition(1, bird.transform.position);
    }

    void InitializeThrow()
    {
        bird.transform.position = new Vector3(birdRestPosition.position.x, 
                                                birdRestPosition.position.y,0);
        slingShotState = SlingShotState.Idle;
        SetRubbersActive(true);
    }

    void Update()
    {
        switch (slingShotState)
        {
            case SlingShotState.Idle:
                InitializeThrow();
                ShowRubbers();

                if(Input.GetMouseButton(0)) // LeftClick-Holding
                {
                    Vector3 location = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                    if(bird.GetComponent<CircleCollider2D>() == 
                        Physics2D.OverlapPoint(location))
                        {
                            pullSling.Play();
                            slingShotState = SlingShotState.Pulling;
                        }                        
                }
                break;

            case SlingShotState.Pulling:

                ShowRubbers();
                if (Input.GetMouseButton(0)) // LeftClick-Holding
                {
                    Vector3 location = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    location.z = 0;

                    if (Vector3.Distance(location, slingMiddleVector) > 2.5f)
                    {
                        var maxPosition = (location - slingMiddleVector).normalized * 
                                                            2.5f + slingMiddleVector;
                    }
                    else
                    {
                    bird.transform.position = location;
                    }
                }
                else
                {
                    float distance = Vector3.Distance(slingMiddleVector, bird.transform.position);
                    if (distance > 0.5)
                    {
                        pullSling.Stop();
                        releaseSling.Play();
                        SetRubbersActive(false);
                        slingShotState = SlingShotState.Released;
                        ThrowBird(distance);
                    }
                }
                break;

            case SlingShotState.Released:
                
                break;

            default:
            break;
        }
    }

    void ThrowBird(float distance)
    {
        Vector3 velocity = slingMiddleVector - bird.transform.position;
        bird.GetComponent<Bird>().OnThrow();
        bird.GetComponent<Rigidbody2D>().velocity = new Vector2(velocity.x, velocity.y) * 
                                                                throwSpeed * distance;
        bird.GetComponent<Rigidbody2D>().gravityScale = 1;
    }
}
