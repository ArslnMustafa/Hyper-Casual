using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class LevelController : MonoBehaviour
{
    public static LevelController Current;
    public bool gameActive = false;
    public GameObject startMenu, gameMenu, gameOverMenu, finishMenu;
    public Text scoreText, finishScoreText, currentLevelText, nextLevelText;
    // Start is called before the first frame update
    public AudioSource gameMusicAudioSource;
    public AudioClip victoryAudioClip, gameOverAudioClip;
    public Slider levelProgressBar;
    public float maxDistance;
    public GameObject finishLine;
    int currentLevel;
    int score;
    void Start()
    {

        Current = this;
        currentLevel = PlayerPrefs.GetInt("currentLevel");
        if (SceneManager.GetActiveScene().name != "Level " + currentLevel)
        {
            SceneManager.LoadScene("Level " + currentLevel);
        }
        else
        {
            currentLevelText.text = (currentLevel + 1).ToString();
            nextLevelText.text = (currentLevel + 2).ToString();
        }
        gameMusicAudioSource = Camera.main.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerController player = PlayerController.Current;
        float distance = finishLine.transform.position.z - PlayerController.Current.transform.position.z;
        levelProgressBar.value = 1 - (distance / maxDistance);

    }

    public void StartLevel()
    {
        maxDistance = finishLine.transform.position.z - PlayerController.Current.transform.position.z;
        PlayerController.Current.ChangeSpeed(PlayerController.Current.runningSpeed);
        //_currentRunningSpeed = runningSpeed;
        startMenu.SetActive(false);
        gameMenu.SetActive(true);
        PlayerController.Current.animator.SetBool("running", true);
        gameActive = true;
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void LoadNextLevel()
    {
        SceneManager.LoadScene("Level " + (currentLevel + 1));
    }
    public void GameOver()
    {
        gameMusicAudioSource.Stop();
        gameMusicAudioSource.PlayOneShot(gameOverAudioClip);
        gameMenu.SetActive(false);
        gameOverMenu.SetActive(true);
        gameActive = false;

    }
    public void FinishGame()
    {
        gameMusicAudioSource.Stop();
        gameMusicAudioSource.PlayOneShot(victoryAudioClip);
        PlayerPrefs.SetInt("currentLevel", (currentLevel + 1));
        finishScoreText.text = score.ToString();
        gameMenu.SetActive(false);
        finishMenu.SetActive(true);
        gameActive = false;

    }
    public void ChangeScore(int incerement)
    {
        score += incerement;
        scoreText.text = score.ToString();


    }
}
