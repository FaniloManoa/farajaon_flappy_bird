using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
    public Text scoreText;
    private int score = -1;
    public float scoreInterval = 2f;
    private Coroutine scoreCoroutine;

    void Start()
    {
        UpdateScoreText();
        scoreCoroutine = StartCoroutine(IncrementScoreRoutine());
    }

    public void IncreaseScore(int amount)
    {
        score += amount;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score.ToString();
        }
    }

    private IEnumerator IncrementScoreRoutine()
    {
        while (true)
        {
            IncreaseScore(1);
            yield return new WaitForSeconds(scoreInterval);
        }
    }

    public void GameOver()
    {
        if (scoreCoroutine != null)
        {
            StopCoroutine(scoreCoroutine);
        }
    }
}
