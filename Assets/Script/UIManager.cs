using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static int GoldCount = 0;

    [SerializeField] private Text goldCountLabel = null;
    [SerializeField] private Text labelWinLose;
    [SerializeField] private GameObject winloseScreen;

    private void OnEnable()
    {
        GoldCount = 0;
        goldCountLabel.text = "GOLD : " + GoldCount;
    }
    public void ShowWinLoseScreen(String message)
    {
        labelWinLose.text = message;
        winloseScreen.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("DemoAIAgent");
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void RefreshGoldHitCount()
    {
        GoldCount += 1;
        goldCountLabel.text = "GOLD : " + GoldCount;
    }


}
