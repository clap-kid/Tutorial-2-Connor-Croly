using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    public AudioClip winmusic;

    public AudioSource musicSource;

    private Rigidbody2D rd2d;
    public float speed;
    public Text score;
    private int scoreValue = 0;
    private int deathcounter = 0;
    public GameObject life1;
    public GameObject life2;
    public GameObject life3;
    public GameObject WIN;
    public GameObject LOSE;

    private int teleported = 0;
    private int stage = 0;

    private int vinethud = 0;

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        score.text = scoreValue.ToString();
        
        WIN.SetActive(false);
        LOSE.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float verMovement = Input.GetAxis("Vertical");

        rd2d.AddForce(new Vector2(hozMovement * speed, verMovement * speed));

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       if (collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = scoreValue.ToString();
            Destroy(collision.collider.gameObject);
        }

        if (scoreValue >= 8 && vinethud == 0)
        {
            WIN.SetActive(true);
            Destroy(this);
            musicSource.clip = winmusic;
            musicSource.Play();
            vinethud = vinethud +1;
        }

        if (scoreValue >= 4 && teleported == 0)
        {
            teleported = teleported + 1;
            transform.position = new Vector2(67.5f, 3.0f);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if(Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
            }
        }

        if (collision.collider.tag == "OOB")
        {
            deathcounter = deathcounter + 1;
            if (deathcounter != 3 && stage == 0)
            {
                transform.position = new Vector2(0.0f, 0.0f);
            }

            if (deathcounter != 3 && stage == 1)
            {
                transform.position = new Vector2(67.5f, 3.0f);
            }
        }
    }

    void Update()
    {

        if (deathcounter == 1)
        {
            life1.SetActive(false);
        }

        if (deathcounter == 2)
        {
            life2.SetActive(false);
        }

        if (deathcounter == 3)
        {
            life3.SetActive(false);
            LOSE.SetActive(true);
            Destroy(this);
        }

        //LIFE RESET
        if (teleported == 1 && stage == 0)
        {
            life1.SetActive(true);
            life2.SetActive(true);
            life3.SetActive(true);
            deathcounter = 0;
            stage = stage +1;
        }
    }

}
