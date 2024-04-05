using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    public BallStartScript ball { get; private set; }

    public Brick[] bricks { get; private set; }

    public int level = 1;

    public int score = 0;

    public int lives = 3;

    public TextMeshProUGUI scoreText;

    public GameObject[] livesImage;

    private int extraLife = 0;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        SceneManager.sceneLoaded += OnLevelLoaded;
    }

    private void Start()
    {
        Cursor.visible = false;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void Update()
    {
        if (extraLife + 10000 <= this.score && lives <= 10)
        {
            this.lives++;
            livesImage[this.lives - 1].gameObject.SetActive(true);
            extraLife += 10000;
        }
        else
        {
            return;
        }
    }

    public void ExtraLife()
    {
        if (lives <= 10)
        {
            this.lives++;
            livesImage[this.lives - 1].gameObject.SetActive(true);
        }
        else
        {
            return;
        }
    }

    private void NewGame()
    {
        this.score = 0;
        this.lives = 3;
        ResetLivesImages();

        SceneManager.LoadScene("Level1");
    }

    void ResetLivesImages()
    {
        // Activate the first 3 life images
        for (int i = 0; i < Mathf.Min(3, livesImage.Length); i++)
        {
            livesImage[i].SetActive(true);
        }

        // Deactivate the rest of the life images
        for (int i = 3; i < livesImage.Length; i++)
        {
            livesImage[i].SetActive(false);
        }
    }

    private void LoadLevel(int level)
    {
        this.level = level;

        if (level > 20)
        {
            SceneManager.LoadScene("WinScreen");
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    private void OnLevelLoaded(Scene scene, LoadSceneMode mode)
    {
        this.ball = FindObjectOfType<BallStartScript>();
        this.bricks = FindObjectsOfType<Brick>();
    }

    private void ResetLevel()
    {
        this.ball.ResetBall();
    }

    private void GameOver()
    {
        //SceneManager.LoadScene("GameOver");

        NewGame();
    }

    public void Fall()
    {
        this.lives--;
        livesImage[this.lives].gameObject.SetActive(false);
        ball.ballActive = false;
        ball.GetComponent<SpriteRenderer>().enabled = true;

        if (this.lives > 0)
        {
            ResetLevel();
        }
        else
        {
            GameOver();
        }
    }

    public void Hit(Brick brick)
    {
        this.score += brick.points;

        scoreText.text = score.ToString("0000000");

        if (Cleared())
        {
            LoadLevel(this.level + 1);
        }
    }

    public void BonusPoints()
    {
        if (FindObjectOfType<NextLevel>().bonusPoints == true)
        {
            this.score += 500;

            int brickCount = 0;

            for (int i = 0; i < this.bricks.Length; i++)
            {
                if (this.bricks[i].gameObject.activeInHierarchy && !this.bricks[i].unbreakable)
                {
                    brickCount++;
                }
                FindObjectOfType<NextLevel>().bonusPoints = false;
            }
            this.score += brickCount * 30;
        }                

        scoreText.text = score.ToString("0000000");
    }

    private bool Cleared()
    {
        for (int i = 0; i < this.bricks.Length; i++)
        {
            if (this.bricks[i].gameObject.activeInHierarchy && !this.bricks[i].unbreakable)
            {
                return false;
            }
        }

        return true;
    }
}
