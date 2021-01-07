using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerAction : MonoBehaviour
{
    public float moveSpeed;
    public float JumpSpeed;
    public float JumpSpeedAdd = 20;
    public float addJumpForceTime = 0.3f;

    private Animator animator;
    private float jumpTime = 0;


    private float playerHalfWidth;
    private float playerHalfHeight;
    private Rigidbody2D rig2d;
    // Use this for initialization
    void Start()
    {
        animator = transform.GetComponent<Animator>();
        rig2d = transform.GetComponent<Rigidbody2D>();
        playerHalfHeight = transform.GetComponent<RectTransform>().sizeDelta.y / 2;
        playerHalfWidth = transform.GetComponent<RectTransform>().sizeDelta.x / 2;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        Debug.Log("撞到了");
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position - new Vector3(playerHalfWidth, 0), Vector3.up,  playerHalfHeight+1, 1 << LayerMask.NameToLayer("plane"));
        //if (hitInfo.collider == null)
        //{
            RaycastHit2D hitInfo2 = Physics2D.Raycast(transform.position + new Vector3(playerHalfWidth, 0), Vector3.up,  playerHalfHeight+1, 1 << LayerMask.NameToLayer("plane"));

        //}
        if (hitInfo.collider != null)
        {
            Debug.Log("射到了");
            CubeAction cubeAction = hitInfo.collider.transform.parent.GetComponent<CubeAction>();
            if (cubeAction != null)
            {
                cubeAction.UpDown();
            }

        }

        if (hitInfo2.collider != null)
        {
            Debug.Log("射到了");
            CubeAction cubeAction = hitInfo2.collider.transform.parent.GetComponent<CubeAction>();
            if (cubeAction != null)
            {
                cubeAction.UpDown();
            }

        }


    }

    void Move()
    {
        float h = Input.GetAxis("Horizontal");
        animator.SetBool("isRunRight", h > 0 ? true : false);

        if (h == 0)
        {

            animator.SetBool("isRun", false);
        }
        else
        {
            animator.SetBool("isRun", true);
            rig2d.velocity = new Vector2(h * moveSpeed, rig2d.velocity.y);

        }
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {

			Debug.Log("跳跃");
             RaycastHit2D hitInfo = Physics2D.Raycast(transform.position - new Vector3(playerHalfWidth, 0), Vector3.down, 2 * playerHalfHeight, 1 << LayerMask.NameToLayer("plane"));
            if (hitInfo.collider == null)
            {
				Debug.Log("跳跃1.....");
                hitInfo = Physics2D.Raycast(transform.position + new Vector3(playerHalfWidth, 0), Vector3.down, 2 * playerHalfHeight, 1 << LayerMask.NameToLayer("plane"));

            }
            if (hitInfo.collider != null)
            {	
					Debug.Log("跳跃2.....");

                if (hitInfo.distance < playerHalfHeight + 1)
                {
                    rig2d.velocity = new Vector2(rig2d.velocity.x, JumpSpeed);
					
                }
            }

        }

        if (Input.GetKey(KeyCode.Space))
        {
            jumpTime += Time.deltaTime;

            if (jumpTime <= addJumpForceTime)
            {

                rig2d.velocity += new Vector2(0, JumpSpeedAdd * Time.deltaTime);

            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            jumpTime = 0;
        }
    }
}
