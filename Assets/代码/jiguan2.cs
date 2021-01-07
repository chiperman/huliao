using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jiguan2 : MonoBehaviour {
	private Animator animator;

	// Use this for initialization
	void Start () {
				//获取动画组件
		animator = transform.GetComponent<Animator>();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnCollisionEnter2D(Collision2D coll){
		//碰到机关
		if (coll.transform.tag == "Mario"){
			Debug.Log("碰到机关");
            animator.SetBool("upOrdown", !animator.GetBool("upOrdown"));
			//获取移动板组件
			GameObject Cube = GameObject.Find("movestone");
			cubemove em = Cube.GetComponent<cubemove>();
			//加速度与取消速度
			if (em != null)
            {
				if(animator.GetBool("upOrdown"))
					em.setSpeedon();
				else
					em.setSpeedup();
			}
        }
	}
}
