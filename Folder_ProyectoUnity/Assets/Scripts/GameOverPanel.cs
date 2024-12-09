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
            DontDestroyOnLoad(gameObject); // No destruir al cargar nueva escena
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

        // Actualizar la lista de mejores puntuaciones y el texto
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
        gameOverPanel.SetActive(false); // Desactivar el panel antes de recargar la escena
        GameManager.Instance.ResetScore();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Recargar la escena actual
        Time.timeScale = 1;
    }

    public void GoToMenu()
    {
        gameOverPanel.SetActive(false); // Desactivar el panel antes de recargar la escena
        GameManager.Instance.ResetScore();
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1;
    }
}
