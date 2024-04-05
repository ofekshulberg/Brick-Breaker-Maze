using UnityEngine;

public class Brick : MonoBehaviour
{
    public SpriteRenderer spriteRenderer { get; private set; }

    public Sprite[] states;

    public int health { get; private set; }

    public int points = 10;

    public bool unbreakable;

    private int notUnbreakable = 1;

    public GameObject brickBreak;

    private void Awake()
    {
        this.spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        ResetBrick();
    }

    public void ResetBrick()
    {
        this.gameObject.SetActive(true);

        if (!this.unbreakable)
        {
            this.health = this.states.Length;
            this.spriteRenderer.sprite = this.states[this.health - 1];
        }
    }

    private void Hit()
    {
        if (this.unbreakable)
        {
            this.notUnbreakable++;
            if (this.notUnbreakable >= 50)
            {
                unbreakable = false;
            }
            return;
        }

        this.health--;

        if (this.health <= 0)
        {
            Instantiate(this.brickBreak, transform.position, Quaternion.identity);
            this.gameObject.SetActive(false);
        }
        else
        {
            this.spriteRenderer.sprite = this.states[this.health - 1];
        }

        FindObjectOfType<GameManager>().Hit(this);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ball" || col.gameObject.tag == "Bullet")
        {
            Hit();
            col.rigidbody.velocity *= 1.015f;
        }
    }
}
