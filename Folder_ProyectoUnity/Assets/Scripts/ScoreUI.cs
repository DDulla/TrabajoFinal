using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class ScoreUI : MonoBehaviour
{
    public TMP_Text scoreText; 

    void Update()
    {
        scoreText.text = "Score: " + GameManager.Instance.score;
    }
}

