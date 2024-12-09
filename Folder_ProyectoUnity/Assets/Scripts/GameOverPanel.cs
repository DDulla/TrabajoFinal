using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverPanel : MonoBehaviour
{
    public static GameOverPanel Instance { get; private set; }
    public GameObject gameOverPanel;
    public TMP_Text scoreText;
    public TMP_Text highScoresText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        gameOverPanel.SetActive(false);
    }

    public void ShowGameOverPanel()
    {
        gameOverPanel.SetActive(true);
        int finalScore = GameManager.Instance.score;
        scoreText.text = "Final Score: " + finalScore;

        if (HighScoreManager.Instance != null)
        {
            HighScoreManager.Instance.AddScore(finalScore);
            UpdateHighScoresText();
        }
    }

    private void UpdateHighScoresText()
    {
        if (HighScoreManager.Instance != null)
        {
            highScoresText.text = "Top Scores:\n";
            var highScores = HighScoreManager.Instance.GetHighScores();
            for (int i = 0; i < highScores.Count; i++)
            {
                highScoresText.text += (i + 1) + ": " + highScores[i] + "\n";
            }
        }
    }

    public void RetryLevel()
    {
        gameOverPanel.SetActive(false); 
        GameManager.Instance.ResetScore();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
        Time.timeScale = 1;
    }

    public void GoToMenu()
    {
        gameOverPanel.SetActive(false); 
        GameManager.Instance.ResetScore();
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1;
    }
}
