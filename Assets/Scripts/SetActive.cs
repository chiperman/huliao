using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActive : MonoBehaviour
{
    public GameObject xx;
    public bool OnEnableActive = true;
    //public float invokeTime = 3;




    // Use this for initialization
    void Start()
    {
        if (xx == null)
        {

            xx = gameObject;

        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetByActiveself()
    {
        if (xx.activeSelf == true)
        {
            xx.SetActive(false);
        }
        else
        {
            xx.SetActive(true);
        }
    }

    public void CloseGo()
    {

        xx.SetActive(false);
    }
    public void DestoryGo()
    {

        Destroy(xx);
    }
    public void OpenGo()
    {

        xx.SetActive(true);
    }
    public void InvokeCloseGo(float time)
    {
        Invoke("CloseGo", time);
    }
    public void InvokeOpenGo(float time)
    {

        Invoke("OpenGo", time);
    }
    public void InvokeDestoryGo(float time)
    {
        Invoke("DestoryGo", time);
    }


    public void ResetActive()
    {
        xx.SetActive(OnEnableActive);
    }
}
