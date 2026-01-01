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
     [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private Text lifeText;
    [SerializeField] private Text finalScoreText;
    [SerializeField] private Text hightScoreText;

    private int currentScore = 0;
    private int highScore = 0;
    public static bool isRestarting = false;

    void Start()
    {
        //Load High Score
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        UpdateScoreUI();

        //show start screen at the beginning
        if (isRestarting)
        {
            StartGame();
            isRestarting = false;
        }
        else
        {
            ShowStartScreen();
        }


        AudioManager.Instance.PlayMusic("bgm");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseScreen.activeSelf)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
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
        gameOverScreen.SetActive(false);
        gameUI.SetActive(true);
        AudioManager.Instance.PlaySound("UIPopup");
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        pauseScreen.SetActive(true);
        AudioManager.Instance.PlaySound("UIPopup");
    }

    public void ResumeGame()
    {

        Time.timeScale = 1;
        pauseScreen.SetActive(false);
        AudioManager.Instance.PlaySound("UIPopup");
    }

    public void RestartGame()
    {
        isRestarting = true;
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void GameOver()
    {
        Time.timeScale = 0;
        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(true);
            finalScoreText.text = "SCORE: " + currentScore;
            hightScoreText.text = "HIGHT SCORE :" + highScore;
            AudioManager.Instance.StopMusic();

            if(currentScore >= highScore)
            {
                AudioManager.Instance.PlaySound("hightScore");
            }
            else
            {
                AudioManager.Instance.PlaySound("game_over");
            }
        }
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
    public void UpdateLifeUI(int currentLife)
    {
        if (lifeText != null)
            lifeText.text = lifeEmoji(currentLife);
    }

    private string lifeEmoji(int life)
    {
       string hearts = "";
        for (int i = 0; i < life; i++)
        {
            hearts += "❤";
        }
        return hearts;
    }






}