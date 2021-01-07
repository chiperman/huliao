using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cubeaction : MonoBehaviour
{

    //创造金币模板
    public GameObject coinModle;
    private Animator animator;
    // Use this for initialization
    private Image cube;
    //空金币砖块
    public Sprite noCube;
    //分数组件
    public Text score;
    public Text lifeNum;


    void Start()
    {
        //获取砖块
        cube = transform.GetComponentInChildren<Image>();
        score = GameObject.Find("Text").GetComponent<Text>();//获取分数
        lifeNum = GameObject.Find("lifeNumber").GetComponent<Text>();
        animator = transform.GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    //砖块上升函数
    public void UpDown()
    {
        //Debug.Log(cube.sprite==noCube);
        if (cube.sprite != noCube)
        {
            animator.SetTrigger("upDown");
            //加3分
            MarioMovo.num = MarioMovo.num + 500;
            if (MarioMovo.num > 1000)
            {
                MarioMovo.num -= 1000;
                MarioMovo.Life += 1;
                lifeNum.text = MarioMovo.Life.ToString();
            }
            score.text = "Score：" + MarioMovo.num;
        }
    }
    //硬币生成函数
    public void CreatMoney()
    {
        GameObject coin = Instantiate(coinModle);
        coin.transform.SetParent(transform);
        coin.transform.position = transform.position + new Vector3(0, 100, 0);
        Destroy(coin, 1f);
        cube.sprite = noCube;
        Debug.Log(cube.sprite == noCube);
    }
}
