using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFly : MonoBehaviour
{
    public float speed = 3;
    Rigidbody2D rigid;
    private int a=0;
   
    // Use this for initialization
    void Start()
    {        
        rigid = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Vector3 front = new Vector3(-1, 0, 0);
        if (transform.localScale.x < 0)
        {
            front = new Vector3(1, 0, 0);
        }

        a++;
        if (a > 200)
        {
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            a = 0;
        }
        rigid.velocity = new Vector2(front.x * speed, rigid.velocity.y);
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
