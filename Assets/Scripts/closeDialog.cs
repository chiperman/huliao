using UnityEngine;

public class closeDialog : MonoBehaviour
{
    public GameObject enterDialog;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            enterDialog.SetActive(false);
        }

    }
}
