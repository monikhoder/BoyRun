using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Text scoreText;
    [SerializeField] private Text highScoreText;
    [SerializeField] private GameObject startScreen;
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private GameObject gameUI;

    private int currentScore = 0;
    private int highScore = 0;

    void Start()
    {
        //Load High Score
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        UpdateScoreUI();

        //show start screen at the beginning
        ShowStartScreen();
    }

    // game control

    public void ShowStartScreen()
    {
        Time.timeScale = 0;
        startScreen.SetActive(true);
        pauseScreen.SetActive(false);
        gameUI.SetActive(false);
    }

    public void StartGame()
    {
        Time.timeScale = 1;
        startScreen.SetActive(false);
        gameUI.SetActive(true);
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        pauseScreen.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        pauseScreen.SetActive(false);
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void BackToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }

    public void RefreshCoin()
    {
        currentScore++;

        // check for high score
        if (currentScore > highScore)
        {
            highScore = currentScore;
            // save high score
            PlayerPrefs.SetInt("HighScore", highScore);
        }

        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null) scoreText.text = "SCORE: " + currentScore;
        if (highScoreText != null) highScoreText.text = "HIGH SCORE: " + highScore;
    }
}