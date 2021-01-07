using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class CameraController : MonoBehaviour {
    public Transform player;//角色
	// Use this for initialization
	void Start () {
        player = GameObject.Find("Mario").GetComponent<Transform>();//获取角色
	}
	
	// Update is called once per frame
	void Update () {
		if(player.position.x>963 && player.position.x<2844){
			if(player.position.y+334>800)
				transform.position = new Vector3(player.position.x, player.position.y+76, transform.position.z);
			else
				transform.position = new Vector3(player.position.x, 542, transform.position.z);
		}
		else{
			if(player.position.y+334>800)
				if(player.position.x<=963)
					transform.position = new Vector3(963, player.position.y+76, transform.position.z);
				else
					transform.position = new Vector3(2844, player.position.y+76, transform.position.z);
			else
				if(player.position.x<=963)
					transform.position = new Vector3(963, 542, transform.position.z);
				else
					transform.position = new Vector3(2844, 542, transform.position.z);
		}
	}
}
