using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    CharacterController2D cc;
    Animator anim;
    Rigidbody2D rigid;

    Transform stampPoint;

    public int Score;
    public Text ScoreNum;
    // 音效
    public AudioSource coinAudio;
    public AudioSource dead;
    // 预制体
    public GameObject prefabDeadAnim;
    public GameObject prefabPlayerDeadAnim;
    public int Life = 3;

    public Text lifeNum;
    public float speed = 5f;
    float move;
    bool jump;


    // Use this for initialization
    void Start()
    {
        cc = GetComponent<CharacterController2D>();
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        stampPoint = transform.Find("StampPoint");
    }

    // Update is called once per frame
    void Update()
    {
        move = Input.GetAxis("Horizontal");
        float temp = move;

        move *= speed;
        jump = Input.GetButton("Jump");

        // 跳跃动画
        if (cc.isGrounded)
        {
            anim.SetFloat("speed", Mathf.Abs(temp));
            anim.SetBool("jump_up", false);
            anim.SetBool("jump_down", false);
        }
        else
        {
            Vector3 vel = rigid.velocity;
            if (vel.y > 0)
            {
                anim.SetBool("jump_up", true);
                anim.SetBool("jump_down", false);
                anim.SetBool("dead", false);
            }
            else
            {
                anim.SetBool("jump_up", false);
                anim.SetBool("jump_down", true);
            }
        }

    }

    private void FixedUpdate()
    {
        StampTest();
        cc.Move(move, jump);
    }

    private void StampTest()
    {
        Collider2D c = Physics2D.OverlapCircle(stampPoint.position, 0.1f, LayerMask.GetMask("Enemy"));
        if (c == null)
        {
            return;
        }

        // 踩到了怪
        Destroy(c.gameObject);
        Score += 100;
        ScoreNum.text = Score.ToString();
        if (Score >= 1000)
        {
            Score -= 1000;
            Life += 1;
            ScoreNum.text = Score.ToString();
            lifeNum.text = Life.ToString();
        }

        GameObject da = Instantiate(prefabDeadAnim, null);
        da.transform.position = c.transform.position;

        StartCoroutine(Wait(0.35f, da));
        rigid.velocity = new Vector2(rigid.velocity.x, 0);
        rigid.AddForce(new Vector2(0, 200));

    }

    IEnumerator Wait(float waitTime, GameObject da)
    {
        yield return new WaitForSeconds(waitTime);
        Destroy(da.gameObject);
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Collection")
        {
            coinAudio.Play();
            Destroy(collision.gameObject);
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
        else if (collision.tag == "Collection_1")
        {
            coinAudio.Play();
            Destroy(collision.gameObject);

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

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy") && Life!=1)
        {
            if (transform.position.x < collision.gameObject.transform.position.x){
                rigid.velocity = new Vector2(-10, rigid.velocity.y);
                dead.Play();
                anim.SetBool("dead", true);
                Life -= 1;
                lifeNum.text = Life.ToString();
            }
            else
            {
                rigid.velocity = new Vector2(10, rigid.velocity.y);
                dead.Play();
                anim.SetBool("dead", true);
                Life -= 1;
                lifeNum.text = Life.ToString();
            }
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy") || collision.gameObject.layer == LayerMask.NameToLayer("DeadLand"))
        {
            anim.SetBool("dead", true);
            rigid.velocity = new Vector2(rigid.velocity.x, 0);
            transform.GetComponent<Collider2D>().enabled = false;
            rigid.gravityScale = 0.2f;
            dead.Play();
            Invoke("ReStart", 2);
        }
    }

    void ReStart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
