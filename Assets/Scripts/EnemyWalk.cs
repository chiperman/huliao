using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWalk : MonoBehaviour {
    public float speed = 3;
    Rigidbody2D rigid;

	// Use this for initialization
	void Start () {
        rigid = GetComponent<Rigidbody2D>();

	}

    private void FixedUpdate() {
        Vector3 front = new Vector3(-1, 0, 0);

        if (transform.localScale.x < 0)
        {
            front = new Vector3(1, 0, 0);
        }

        Vector3 beg = transform.position + new Vector3(0, -0.1f, 0);
        Vector3 down = new Vector3(0, -1, 0);

        // 碰到物体转身
        if (Physics2D.Raycast(beg, front, 0.8f, LayerMask.GetMask("Default"))) {

            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        }
        // 碰到悬崖转身
        if (!Physics2D.Raycast(beg, front+down, 1.5f, LayerMask.GetMask("Default")))
        {
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        }

        rigid.velocity = new Vector2(front.x * speed, rigid.velocity.y);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
