using UnityEngine;

public class AudioController : MonoBehaviour
{
    bool isPlaying = false;

    void Start()
    {
        if (!isPlaying)
        {
            this.enabled = true;
            isPlaying = true;
        }
    }

    void Awake()
    {
        GameObject[] musicObj = GameObject.FindGameObjectsWithTag("GameMusic");
        if (musicObj.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }
}
