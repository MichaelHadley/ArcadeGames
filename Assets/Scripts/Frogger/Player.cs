using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        FroggerManager.Instance.Player = gameObject;
    }

    public AudioClip goalClip;
    public AudioClip deadFrogClip;
    public AudioClip splashClip;

    // FixedUpdate is called once per frame unless the game is paused(timescale = 0)
    void FixedUpdate()
    {
        //If the player goes out of the cameras view kill the player
        Vector3 playerRelPos = Camera.main.WorldToViewportPoint(transform.position);
        if(playerRelPos.x < 0f || playerRelPos.x > 1)
        {
            FroggerManager.Instance.Death();
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Vehicle")
        {
            gameObject.GetComponent<AudioSource>().PlayOneShot(deadFrogClip, .5f);
            FroggerManager.Instance.Death();
        }

        if (col.tag == "DeathZone")
        {
            gameObject.GetComponent<AudioSource>().PlayOneShot(splashClip, .5f);
            FroggerManager.Instance.Death();
        }

        if (col.tag == "Goal")
        {
            if (col.gameObject.transform.GetChild(2).gameObject.activeSelf)
            {
                FroggerManager.Instance.Death();
                gameObject.GetComponent<AudioSource>().PlayOneShot(splashClip, .5f);
            }
            else
            {
                col.gameObject.transform.GetChild(2).gameObject.SetActive(true);
                gameObject.GetComponent<AudioSource>().PlayOneShot(goalClip, .5f);
                FroggerManager.Instance.Goal();
            }
        }
    }
}
