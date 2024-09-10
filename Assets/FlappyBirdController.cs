using UnityEngine;

public class FlappyBirdController : MonoBehaviour
{
    public ScoreManager scoreManager;
    public float jumpForce = 5f;
    private Rigidbody2D rb;
    private bool isGameOver = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!isGameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                Jump();
            }
        }
    }

    void Jump()
    {
        rb.velocity = Vector2.zero;
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        GameOver();
        scoreManager.GameOver();
    }

    void GameOver()
    {
        isGameOver = true;
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;

        WallSpawner[] spawners = FindObjectsOfType<WallSpawner>();
        foreach (WallSpawner spawner in spawners)
        {
            spawner.StopSpawning();
        }
    }
}
