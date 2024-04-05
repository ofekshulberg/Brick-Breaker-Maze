using UnityEngine;

public class LoseBall : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Ball")
        {
            FindObjectOfType<GameManager>().Fall();
            col.gameObject.SetActive(false);
        }
    }
}
