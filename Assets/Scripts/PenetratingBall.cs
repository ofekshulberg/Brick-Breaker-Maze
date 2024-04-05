using UnityEngine;

public class PenetratingBall : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "Slider")
        {

            this.gameObject.SetActive(false);
        }
        else if (col.gameObject.tag == "Wall")
        {
            this.gameObject.SetActive(false);
        }
    }
}
