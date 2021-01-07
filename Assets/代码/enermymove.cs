
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
 
//Add this script to the platform you want to move.
//左右移动的平台
public class enermymove : MonoBehaviour {
 
	//Platform movement speed.平台移动速度
	public float speed;
	public float movex;
 
	//This is the position where the platform will move.平台移动的位置
	//public Transform MovePosition;//创建一个空物体作为移动的位置
 
	private Vector3 StartPosition;
	private Vector3 EndPosition;
	private bool OnTheMove;
 
	// Use this for initialization
	void Start () {
		//Store the start and the end position. Platform will move between these two points.储存左右两端点位置
		StartPosition = this.transform.position;
		//EndPosition = MovePosition.position;
		EndPosition = new Vector3(transform.position.x-movex, transform.position.y, transform.position.z);
	}
	
	void FixedUpdate () {
	
		float step = speed * Time.deltaTime;
 
		if (OnTheMove == false) {
			this.transform.position = Vector3.MoveTowards (this.transform.position, EndPosition, step);
		}else{
			this.transform.position = Vector3.MoveTowards (this.transform.position, StartPosition, step);
		}
 
		//When the platform reaches end. Start to go into other direction.
		if (this.transform.position.x == EndPosition.x && this.transform.position.y == EndPosition.y && OnTheMove == false) {
			OnTheMove = true;
		}else if (this.transform.position.x == StartPosition.x && this.transform.position.y == StartPosition.y && OnTheMove == true) {
			OnTheMove = false;
		}
	}
	public void setSpeedon()
    {
		speed = 100;
		Debug.Log("设置速度为100");
    }
	public void setSpeedup()
    {
		speed = 0;
		Debug.Log("设置速度为0");
    }
}
