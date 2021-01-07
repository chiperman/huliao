using UnityEngine;

public class GameOver : MonoBehaviour
{
    public GameObject enterDialog;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            enterDialog.SetActive(true);
        }

    }
}
