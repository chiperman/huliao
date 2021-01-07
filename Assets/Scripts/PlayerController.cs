using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    public Collider2D coll;
    public float speed;
    public float JumpForce;
    public AudioSource jumpAudio, coinAudio, hurtAudio, GameOver;
    public GameObject enterDialog;
    public LayerMask ground;
    public int Score;
    public Text ScoreNum;
    public int Life = 3;

    public Text lifeNum;

    private bool isHurt;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if (!isHurt)
        {
            Movement();
        }
        SwitchAnim();
    }

    // plaer move
    void Movement()
    {
        float horizontalMove = Input.GetAxis("Horizontal");
        float facedirection = Input.GetAxisRaw("Horizontal");

        // Player Move
        if (horizontalMove != 0)
        {
            rb.velocity = new Vector2(horizontalMove * speed * Time.deltaTime, rb.velocity.y);
            anim.SetFloat("running", Mathf.Abs(facedirection));
        }
        if (facedirection != 0)
        {
            transform.localScale = new Vector3(facedirection, 1, 1);
        }

        // Player Jump
        if (Input.GetButtonDown("Jump") && coll.IsTouchingLayers(ground))
        {
            rb.velocity = new Vector2(rb.velocity.x, JumpForce * Time.deltaTime);
            jumpAudio.Play();
            anim.SetBool("jumping", true);
        }
    }

    // siwtch Animation
    void SwitchAnim()
    {
        anim.SetBool("idle", false);
        if (rb.velocity.y < 0.1f && !coll.IsTouchingLayers(ground))
        {
            anim.SetBool("falling", true);
        }
        if (anim.GetBool("jumping"))
        {
            if (rb.velocity.y < 0)
            {
                anim.SetBool("jumping", false);
                anim.SetBool("falling", true);
            }
        }
        else if (isHurt)
        {
            anim.SetBool("hurt", true);
            anim.SetFloat("running", 0);
            if (Mathf.Abs(rb.velocity.x) < 7f)
            {
                anim.SetBool("hurt", false);
                anim.SetBool("idle", true);
                isHurt = false;
            }
        }
        else if (coll.IsTouchingLayers(ground))
        {
            anim.SetBool("falling", false);
            anim.SetBool("idle", true);
        }
    }


    // trigger
    void OnTriggerEnter2D(Collider2D collison)
    {
        // collect item
        if (collison.tag == "Collection")
        {
            coinAudio.Play();
            Destroy(collison.gameObject);
            Score += 100;
            ScoreNum.text = Score.ToString();
            if (Score >= 1000)
            {
                Score -= 1000;
                Life += 1;
                ScoreNum.text = Score.ToString();
                lifeNum.text = Life.ToString();
            }
        }
        else if (collison.tag == "Collection_1")
        {
            coinAudio.Play();
            Destroy(collison.gameObject);
            Score += 500;
            ScoreNum.text = Score.ToString();
            if (Score >= 1000)
            {
                Score -= 1000;
                Life += 1;
                ScoreNum.text = Score.ToString();
                lifeNum.text = Life.ToString();
            }
        }
        if (collison.tag == "dead")
        {
            GameOver.Play();
            Invoke("RestartGame", 2f);
        }


    }

    // kill enemy
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            // kill
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (anim.GetBool("falling"))
            {
                enemy.JupmOn();
                rb.velocity = new Vector2(rb.velocity.x, JumpForce * Time.deltaTime);
                anim.SetBool("jumping", true);
            }// hurt
            else if (transform.position.x < collision.gameObject.transform.position.x)
            {
                rb.velocity = new Vector2(-10, rb.velocity.y);
                hurtAudio.Play();
                isHurt = true;
                Life -= 1;
                lifeNum.text = Life.ToString();
                if (Life <= 0)
                {
                    enterDialog.SetActive(true);
                    GameOver.Play();
                    Invoke("RestartGame", 2f);
                }
            }
            else if (transform.position.x > collision.gameObject.transform.position.x)
            {
                rb.velocity = new Vector2(10, rb.velocity.y);
                hurtAudio.Play();
                isHurt = true;
                Life -= 1;
                lifeNum.text = Life.ToString();
                if (Life <= 0)
                {
                    enterDialog.SetActive(true);
                    GameOver.Play();
                    Invoke("RestartGame", 2f);
                }
            }
        }
    }

    void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
