
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
 
//Add this script to the platform you want to move.
//左右移动的平台
public class cubemove : MonoBehaviour {
 
	//Platform movement speed.平台移动速度
	public float speed;
	public float movex;
 
	//This is the position where the platform will move.平台移动的位置
	//public Transform MovePosition;//创建一个空物体作为移动的位置
 
	private Vector3 StartPosition;
	private Vector3 EndPosition1;
	private Vector3 EndPosition2;
	private bool OnTheMove;
 
	// Use this for initialization
	void Start () {
		//Store the start and the end position. Platform will move between these two points.储存左右两端点位置
		StartPosition = this.transform.position;
		OnTheMove = false;
		//EndPosition = MovePosition.position;
		EndPosition1 = new Vector3(transform.position.x, transform.position.y+movex, transform.position.z);
		EndPosition2 = new Vector3(transform.position.x, transform.position.y, transform.position.z);
	}
	
	void FixedUpdate () {
	
		float step = speed * Time.deltaTime;
			if(OnTheMove)
				this.transform.position = Vector3.MoveTowards (this.transform.position, EndPosition1, step);
			else
				this.transform.position = Vector3.MoveTowards (this.transform.position, EndPosition2, step);
	}
	public void setSpeedon()
    {
		speed = 100;
		OnTheMove = true;
		Debug.Log("设置速度为100");
    }
	public void setSpeedup()
    {
		speed = 100;
		OnTheMove = false;
		Debug.Log("设置速度为0");
    }
}
