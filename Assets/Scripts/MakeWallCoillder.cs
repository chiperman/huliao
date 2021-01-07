using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MakeWallCoillder : MonoBehaviour {

    private RectTransform[] allRectTrans;

	// Use this for initialization
	void Awake () {
        allRectTrans = transform.GetComponentsInChildren<RectTransform>();
        //foreach(RectTransform xx in allRectTrans)
        //{
        //    BoxCollider2D boxC2d=xx.gameObject.AddComponent<BoxCollider2D>();
        //    boxC2d.size =new Vector2(xx.rect.width, xx.rect.height);

        //}
        for(int i=1;i< allRectTrans.Length;i++)
        {
            BoxCollider2D boxC2d = allRectTrans[i].gameObject.AddComponent<BoxCollider2D>();
            boxC2d.size = new Vector2(allRectTrans[i].rect.width, allRectTrans[i].rect.height);

        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
