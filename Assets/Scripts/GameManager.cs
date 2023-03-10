using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int score;
    public TMP_Text textScore;
    public GameObject canvas;
    public GameObject gameOverScreen;
    public TMP_Text gameoverScoreText;
    public TMP_Text gameoverHighScoreText;

    void Start()
    {

    }

    public void IncrementScore()
    {
        score++;
        textScore.text = score.ToString();
    }

    public void GameOver()
    {
        int highscore = PlayerPrefs.GetInt("Highscore", 0);

        if (score > highscore)
        {
            PlayerPrefs.SetInt("Highscore", score);
            highscore = score;
        }

        Time.timeScale = 0;
        textScore.enabled = false;
        gameOverScreen.SetActive(true);
        gameoverScoreText.text = score.ToString();
        gameoverHighScoreText.text = highscore.ToString();
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
