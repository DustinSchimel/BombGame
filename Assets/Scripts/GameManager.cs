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
    private int bombInFieldCount;
    private int pinkInCageCount;
    private int blackInCageCount;
    private ArrayList bombsInField;
    private ArrayList blackBombsInCage;
    private ArrayList pinkBombsInCage;
    [SerializeField] private int cageMax = 40;
    [SerializeField] public Animator leftExitTop;
    [SerializeField] public Animator leftExitBottom;
    [SerializeField] public Animator rightExitTop;
    [SerializeField] public Animator rightExitBottom;

    void Start()
    {
        bombsInField = new ArrayList();
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
            foreach(GameObject bomb in bombsInField)
            {
                Destroy(bomb);
            }
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
            foreach(GameObject bomb in bombsInField)
            {
                Destroy(bomb);
            }
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
            foreach(GameObject bomb in bombsInField)
            {
                Destroy(bomb);
            }
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
        pinkInCageCount++;
        pinkBombsInCage.Add(pinkBomb);
        bombsInField.Remove(pinkBomb);
        bombInFieldCount--;

        if (pinkInCageCount == cageMax)
        {
            PinkCageOrganize();
        }
    }

    public void IncrementBlackInCage(GameObject blackBomb)
    {
        blackInCageCount++;
        blackBombsInCage.Add(blackBomb);
        bombsInField.Remove(blackBomb);
        bombInFieldCount--;

        if (blackInCageCount == cageMax)
        {
            BlackCageOrganize();
        }
    }

    public void IncrementBombInField(GameObject bomb)
    {
        bombInFieldCount++;
        bombsInField.Add(bomb);
    }

    public void BlackCageOrganize()
    {
        rightExitTop.SetBool("closing", false);
        rightExitBottom.SetBool("closing", false);
        rightExitTop.SetBool("opening", true);
        rightExitBottom.SetBool("opening", true);

        foreach(GameObject bomb in blackBombsInCage)
        {
            IncrementScore();
            Destroy(bomb);
        }

        blackInCageCount = 0;
        blackBombsInCage.Clear();
    }

    public void PinkCageOrganize()
    {
        leftExitTop.SetBool("closing", false);
        leftExitBottom.SetBool("closing", false);
        leftExitTop.SetBool("opening", true);
        leftExitBottom.SetBool("opening", true);

        foreach(GameObject bomb in pinkBombsInCage)
        {
            IncrementScore();
            Destroy(bomb);
        }

        pinkInCageCount = 0;
        pinkBombsInCage.Clear();
    }
}