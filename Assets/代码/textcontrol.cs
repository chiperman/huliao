using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class textcontrol : MonoBehaviour {
    public Transform player;//角色
	// Use this for initialization
	void Start () {
        player = GameObject.Find("Mario").GetComponent<Transform>();//获取角色
	}
	
	// Update is called once per frame
	void Update () {
		if(player.position.x>963 && player.position.x<2844){
			if(player.position.y+334>800)
				transform.position = new Vector3(player.position.x-740, player.position.y+546, transform.position.z);
			else
				transform.position = new Vector3(player.position.x-740, 1012, transform.position.z);
		}
		else{
			if(player.position.y+334>800)
				if(player.position.x<=963)
					transform.position = new Vector3(223, player.position.y+546, transform.position.z);
				else
					transform.position = new Vector3(2844-740, player.position.y+546, transform.position.z);
			else
				if(player.position.x<=963)
					transform.position = new Vector3(223, 1012, transform.position.z);
				else
					transform.position = new Vector3(2844-740, 1012, transform.position.z);
		}
	}
}
