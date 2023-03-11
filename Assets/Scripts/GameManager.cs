using UnityEngine;
using System.Collections;
using System.Collections.Generic;
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
    private int pinkInCage;
    private int blackInCage;
    private ArrayList blackBombsInCage;
    private ArrayList pinkBombsInCage;
    [SerializeField] private int cageMax = 40;

    void Start()
    {
        blackBombsInCage = new ArrayList();
        pinkBombsInCage = new ArrayList();
    }

    public void IncrementScore()
    {
        score++;
        scoreNumber.text = score.ToString();
    }

    public void GameOver(int cause)
    {
        if (cause == 0) // timeout
        {
            foreach(GameObject bomb in blackBombsInCage)
            {
                IncrementScore();
                Destroy(bomb);
            }
            foreach(GameObject bomb in pinkBombsInCage)
            {
                IncrementScore();
                Destroy(bomb);
            }
        }
        else if (cause == 1)    // black in pink cage
        {
            foreach(GameObject bomb in pinkBombsInCage)
            {
                Destroy(bomb);
            }
            foreach(GameObject bomb in blackBombsInCage)
            {
                IncrementScore();
                Destroy(bomb);
            }
        }
        else if (cause == 2)    // pink in black cage
        {
            foreach(GameObject bomb in blackBombsInCage)
            {
                Destroy(bomb);
            }
            foreach(GameObject bomb in pinkBombsInCage)
            {
                IncrementScore();
                Destroy(bomb);
            }
        }

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

    public void IncrementPinkInCage(GameObject pinkBomb)
    {
        pinkInCage++;
        pinkBombsInCage.Add(pinkBomb);

        if (pinkInCage == cageMax)
        {
            foreach(GameObject bomb in pinkBombsInCage)
            {
                IncrementScore();
                Destroy(bomb);
            }

            pinkInCage = 0;
            pinkBombsInCage.Clear();
        }
    }

    public void IncrementBlackInCage(GameObject blackBomb)
    {
        blackInCage++;
        blackBombsInCage.Add(blackBomb);

        if (blackInCage == cageMax)
        {
            foreach(GameObject bomb in blackBombsInCage)
            {
                IncrementScore();
                Destroy(bomb);
            }

            blackInCage = 0;
            blackBombsInCage.Clear();
        }
    }
}