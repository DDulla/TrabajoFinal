using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; 

public class GameOverPanel : MonoBehaviour
{
    public GameObject gameOverPanel;
    public TMP_Text scoreText; 

    public void ShowGameOverPanel()
    {
        gameOverPanel.SetActive(true);
        scoreText.text = "Puntuación Final: " + GameManager.Instance.score; 
    }

    public void RetryLevel()
    {
        GameManager.Instance.ResetScore(); 
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToMenu()
    {
        GameManager.Instance.ResetScore(); 
        SceneManager.LoadScene("Menu");
    }
}
