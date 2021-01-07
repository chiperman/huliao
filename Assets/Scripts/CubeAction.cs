using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CubeAction : MonoBehaviour
{
    public GameObject prop;
    public Sprite changeSprite;
    Animator animator;
    Image myImage;
    float proDis;

    // Use this for initialization
    void Start()
    {
        animator = transform.GetComponentInChildren<Animator>();
        myImage = animator.transform.GetComponent<Image>();
        float thisHalfHeight = transform.GetComponent<RectTransform>().sizeDelta.y / 2;
        float propHalfHeight = prop.GetComponent<RectTransform>().sizeDelta.y / 2;
        proDis = thisHalfHeight + propHalfHeight;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpDown()
    {
        animator.SetTrigger("upDown");
    }

    public void CreateProp()
    {
        GameObject myProp = Instantiate(prop);
        myProp.transform.SetParent(transform);
        myProp.transform.position = animator.transform.position + new Vector3(0, proDis, 0);

    }
    public void ChangeSprite()
    {
        myImage.sprite = changeSprite;

    }
    public void CancelAnimable()
    {
        Destroy(this);
    }

}
