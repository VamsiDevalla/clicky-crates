using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    private float spawnRate = 3.0f;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI gameOverTxt;
    public Button restartBtn;
    public Slider volumeRocker;
    public GameObject titleScreen;
    public AudioSource gameAudio;
    public GameObject pauseScreen;
    private bool paused;
    private int score = 0;
    public bool isGameActive = false;
    private int lives = 4;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ChangePaused();
        }
    }


    IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            GameObject randomTarget = targets[Random.Range(0, targets.Count)];
            Instantiate(randomTarget);
        }
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    public void DecreaseLives()
    {
        if (isGameActive)
        {
            lives -= 1;
            livesText.text = "Lives: " + lives;
            if (lives <= 0)
            {
                GameOver();
            }
        }
    }

    void GameOver()
    {
        isGameActive = false;
        gameOverTxt.gameObject.SetActive(true);
        restartBtn.gameObject.SetActive(true);
    }

    public void ChangeVolume()
    {
        gameAudio.volume = volumeRocker.value;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame(int difficulty)
    {
        titleScreen.SetActive(false);
        scoreText.gameObject.SetActive(true);
        livesText.gameObject.SetActive(true);
        spawnRate /= difficulty;
        isGameActive = true;
        StartCoroutine(SpawnTarget());
        UpdateScore(0);
    }

    private void ChangePaused()
    {
        if (!paused)
        {
            paused = true;
            pauseScreen.SetActive(true);
            Time.timeScale = 0;
        } else
        {
            paused = false;
            pauseScreen.SetActive(false);
            Time.timeScale = 1;
        }
    }
}
