using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int score;
    [SerializeField] private GameObject canvas;
    [SerializeField] private TMP_Text scoreNumber;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private TMP_Text gameoverScoreNumber;
    [SerializeField] private TMP_Text gameoverHighScoreNumber;

    public void IncrementScore()
    {
        score++;
        scoreNumber.text = score.ToString();
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
        scoreNumber.enabled = false;
        gameOverScreen.SetActive(true);
        gameoverScoreNumber.text = score.ToString();
        gameoverHighScoreNumber.text = highscore.ToString();
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
