using UnityEngine;

public class LaserGun : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "Slider")
        {
            FindObjectOfType<SliderScript>().ActivateLaserGun();
            this.gameObject.SetActive(false);
        }
        else if (col.gameObject.tag == "Wall")
        {
            this.gameObject.SetActive(false);
        }
    }
}
