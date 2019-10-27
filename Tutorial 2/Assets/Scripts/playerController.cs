using UnityEngine;
using UnityEngine.UI;

public class playerController : MonoBehaviour
{

     private Rigidbody2D rd2d;
    public float speed;
    public Text score;
    public Text wintext;
    public Text livesText;
    private int scoreValue = 0;
    private int livesValue = 3;
    public AudioSource musicSource;
    public AudioSource musicScource2;
    public AudioClip musicClipsong;
    public AudioClip musicClipwin;
    public AudioClip musicClipcoin;
    public AudioClip musicClipjump;
    public AudioClip musicClipoof;
    private bool faceRight;
    private SpriteRenderer flip;
    Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        score.text = "Score: "+ scoreValue.ToString();
        wintext.text = " ";
        livesText.text = "Lives: " +livesValue;
        musicSource.clip = musicClipsong;
        musicSource.Play();
        anim = GetComponent<Animator>();
        flip = GetComponent<SpriteRenderer>();
        faceRight = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }

        float hozMovement = Input.GetAxisRaw("Horizontal");
        float verMovement = Input.GetAxisRaw("Vertical");

        rd2d.AddForce(new Vector2(hozMovement * speed, verMovement * (speed/2)));

        if (Input.GetKey(KeyCode.A))
        {
            anim.SetInteger("state", 1);
            if (faceRight == true)
            {
                flip.flipX = true;
                faceRight = false;
            }
        }


        if (Input.GetKey(KeyCode.D))
        {
            anim.SetInteger("state", 1);
            if (faceRight == false)
            {
                flip.flipX = false;
                faceRight = true;
            }
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            anim.SetInteger("state", 0);
        }

        if (Input.GetKeyUp(KeyCode.D))
        {
            anim.SetInteger("state", 0);
        }

        if(Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.A))
        {
            anim.SetInteger("state", 0);
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            anim.SetInteger("state", 3);
        }

       



        if (scoreValue == 8)
        {
            wintext.text = "You Win! Game Created by Christopher Alfian".ToString();
            if (musicSource.clip == musicClipsong) { 
            musicSource.clip = musicClipwin;
                musicSource.Play();
                musicSource.volume = 0.75f;
                musicSource.loop = false;
            }

        }

        if (livesValue == 0 )
        {
            wintext.text = "You Lose! Game Created by Christopher Alfian".ToString();
            gameObject.SetActive(false);
        }

        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }

    }

     void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("coin"))
        {
            musicScource2.clip = musicClipcoin;
            musicScource2.Play();
            collision.gameObject.SetActive(false);
            scoreValue++;
            score.text ="Score: "+ scoreValue.ToString();

            if (scoreValue == 4)
            {
                transform.position = new Vector2(74.0f, 1.97f);
                livesValue = 3;
                livesText.text = "Lives: " + livesValue.ToString();
                score.text = scoreValue.ToString();
            }

        }
        

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {

           
            collision.gameObject.SetActive(false);
            livesValue--;
            livesText.text = "Lives: " + livesValue.ToString();
            musicScource2.clip = musicClipoof;
            musicScource2.Play();

        }
    }



    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("floor"))
        {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            {
                anim.SetInteger("state", 1);
            }
            else { anim.SetInteger("state", 0); }

            anim.SetBool("ground", true);

          

            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
                musicScource2.clip = musicClipjump;
                musicScource2.Play();
                anim.SetInteger("state", 2);
                anim.SetBool("ground", false);

            }

           
        }
       

    }

}
