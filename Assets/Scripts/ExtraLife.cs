using UnityEngine;

public class ExtraLife : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "Slider")
        {
            FindObjectOfType<GameManager>().ExtraLife();
            this.gameObject.SetActive(false);
        }
    }
}
