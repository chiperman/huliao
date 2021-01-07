using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarioMovo : MonoBehaviour
{
    public float moveSpeed = 500;
    public float JumpSpeed = 250;
    //设置长按速度增加量
    public float jumpAddSpeed = 1500;
    //最大的增加时间
    public float flyCd = 0.5f;
    //增加时间累计量
    private float flyPassTime = 0;
    //分数组件
    public Text score;
    public static int Life = 3;

    public Text lifeNum;


    public static int num = 0;
    //跳跃音乐组件
    private AudioSource music;
    //动画组件
    private Animator animator;
    private float playerHalfWidth;
    private float playerHalfHeight;
    Rigidbody2D rig2d;
    // Use this for initialization
    void Start()
    {
        score = GameObject.Find("Text").GetComponent<Text>();//获取分数
                                                             //获取动画组件
        animator = transform.GetComponent<Animator>();
        //获取音乐组件
        music = transform.GetComponent<AudioSource>();
        rig2d = transform.GetComponent<Rigidbody2D>();
        //获取游戏人物的高和宽
        playerHalfHeight = transform.GetComponent<RectTransform>().sizeDelta.y / 2;
        playerHalfWidth = transform.GetComponent<RectTransform>().sizeDelta.x / 2;
    }
    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
    }
    //碰撞检测函数
    void OnCollisionEnter2D(Collision2D coll)
    {
        //Debug.Log("撞到了砖");
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position - new Vector3(playerHalfWidth - 20, 0), Vector3.up, playerHalfHeight + 1, 1 << LayerMask.NameToLayer("plane"));
        //if (hitInfo.collider == null)
        //{
        RaycastHit2D hitInfo2 = Physics2D.Raycast(transform.position + new Vector3(playerHalfWidth - 30, 0), Vector3.up, playerHalfHeight + 1, 1 << LayerMask.NameToLayer("plane"));

        //}
        if (hitInfo.collider != null)
        {
            Debug.Log("左边撞到石块");
            Cubeaction cubeAction = hitInfo.collider.transform.parent.GetComponent<Cubeaction>();
            if (cubeAction != null)
            {
                cubeAction.UpDown();
            }

        }

        if (hitInfo2.collider != null)
        {
            Debug.Log("右边撞到石块");
            Cubeaction cubeAction = hitInfo2.collider.transform.parent.GetComponent<Cubeaction>();
            if (cubeAction != null)
            {
                cubeAction.UpDown();
            }

        }

        //检测碰到敌人
        if (coll.transform.tag == "enermy")
        {

            Debug.Log("碰到敌人");
            //左边发出射线
            RaycastHit2D hitInfo3 = Physics2D.Raycast(transform.position - new Vector3(playerHalfWidth - 35, 0), Vector3.down, 2 * playerHalfHeight, 1 << LayerMask.NameToLayer("enermy"));
            if (hitInfo3.collider == null)
            {
                //右边发出射线
                hitInfo3 = Physics2D.Raycast(transform.position + new Vector3(playerHalfWidth - 35, 0), Vector3.down, 2 * playerHalfHeight, 1 << LayerMask.NameToLayer("enermy"));

            }
            if (hitInfo3.collider != null)
            {
                if (hitInfo3.distance < playerHalfHeight - 2)
                {
                    //死亡动画
                    hitInfo3.collider.gameObject.GetComponent<Animator>().SetTrigger("emdead");

                    Destroy(hitInfo3.collider.gameObject, 0.3f);
                    Destroy(hitInfo3.collider);
                    Debug.Log("杀死敌人");
                    //加1分
                    num = num + 100;
                    score.text = "Score：" + num;
                    //Debug.Log(hitInfo3.distance);
                    //Debug.Log(playerHalfHeight);
                }
            }
            //玩家死亡
            if (hitInfo.collider == null && hitInfo3.collider == null)
            {
                this.transform.position = Vector3.MoveTowards(this.transform.position, this.transform.position - new Vector3(200, 0, 0), 200);

                Life -= 1;
                lifeNum.text = Life.ToString();
                if (Life <= 0)
                {
                    animator.SetTrigger("isDeath");
                    Destroy(gameObject, 0.1f);
                    Debug.Log("玩家死亡");
                    num = 0;
                    Application.LoadLevel("maliao");
                    Life = 3;
                }
            }

        }
        //碰到樱桃
        if (coll.transform.tag == "cherry")
        {
            Debug.Log("碰到樱桃");
            //左边发出射线
            RaycastHit2D hitInfo3 = Physics2D.Raycast(transform.position - new Vector3(playerHalfWidth - 25, 0), Vector3.down, 2 * playerHalfHeight, 1 << LayerMask.NameToLayer("Cherry"));
            if (hitInfo3.collider == null)
            {
                hitInfo3 = Physics2D.Raycast(transform.position + new Vector3(playerHalfWidth - 25, 0), Vector3.down, 2 * playerHalfHeight, 1 << LayerMask.NameToLayer("Cherry"));
            }

            Destroy(hitInfo3.collider.gameObject);
            //加2分
            num = num + 100;
            score.text = "Score：" + num;
            Debug.Log("吃掉樱桃");
            if (num >= 1000)
            {
                num -= 1000;
                Life += 1;
                score.text = "Score：" + num;
                lifeNum.text = Life.ToString();
            }
        }
        //碰到门
        if (coll.transform.tag == "door")
        {
            Debug.Log("游戏成功！！");
        }
        //碰到刺
        if (coll.transform.tag == "strike")
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, this.transform.position - new Vector3(-200, 0, 0), 200);

            Life -= 1;
            lifeNum.text = Life.ToString();
            if (Life <= 0)
            {
                animator.SetTrigger("isDeath");
                Destroy(gameObject, 0.1f);
                Debug.Log("玩家死亡");
                num = 0;
                Application.LoadLevel("maliao");
                Life = 3;

            }
        }
    }
    //移动函数
    void Move()
    {
        float h = Input.GetAxis("Horizontal");
        animator.SetBool("zuoPao", h < 0 ? true : false);

        if (h == 0)
        {

            animator.SetBool("nengPao", false);
        }
        else
        {
            animator.SetBool("nengPao", true);
            rig2d.velocity = new Vector2(h * moveSpeed, rig2d.velocity.y);

        }
    }
    //跳跃函数
    void Jump()
    {
        //检测空格键是否按下
        if (Input.GetKeyDown(KeyCode.Space))
        {


            RaycastHit2D hitInfo = Physics2D.Raycast(transform.position - new Vector3(playerHalfWidth - 25, 0), Vector3.down, 151, 1 << LayerMask.NameToLayer("plane"));
            if (hitInfo.collider == null)
            {
                hitInfo = Physics2D.Raycast(transform.position + new Vector3(playerHalfWidth - 20, 0), Vector3.down, 151, 1 << LayerMask.NameToLayer("plane"));

            }
            if (hitInfo.collider != null)
            {
                Debug.Log(hitInfo.distance);
                Debug.Log(playerHalfHeight);

                if (hitInfo.distance < playerHalfHeight + 1)
                {
                    music.Play();
                    rig2d.velocity = new Vector2(rig2d.velocity.x, JumpSpeed);

                }
            }

        }
        //按键长按
        if (Input.GetKey(KeyCode.Space))

        {
            flyPassTime += Time.deltaTime;

            if (flyPassTime <= flyCd)
            {
                //Debug.Log("按键长按"+jumpAddSpeed+Time.deltaTime);
                rig2d.velocity += new Vector2(0, jumpAddSpeed * Time.deltaTime);


            }
        }
        //按键松开
        if (Input.GetKeyUp(KeyCode.Space))
        //Debug.Log("按键松开");
        {
            flyPassTime = 0;
            //music.Stop();
        }
    }
}

