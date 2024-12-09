using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public int score { get; private set; }
    public int speed { get; private set; }
    public int lives { get; private set; }
    public TMP_Text trophiesText; // Referencia al texto en la pantalla de puntuación
    private SimpleCircularLinkedList<PowerUp.PowerUpType> powerUpList;
    private SimpleLinkedListForThrophies<string> obtainedTrophies; // Lista de IDs de trofeos obtenidos
    private int trophiesCount = 0;
    private int totalTrophies = 4;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializePowerUps();
            InitializeTrophiesList();
            ResetLives();
            LoadTrophies();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializePowerUps()
    {
        powerUpList = new SimpleCircularLinkedList<PowerUp.PowerUpType>();
        powerUpList.Add(PowerUp.PowerUpType.Score);
        powerUpList.Add(PowerUp.PowerUpType.Speed);
        powerUpList.Add(PowerUp.PowerUpType.Life);
    }

    private void InitializeTrophiesList()
    {
        obtainedTrophies = new SimpleLinkedListForThrophies<string>();
    }

    public void ApplyPowerUp(PowerUp.PowerUpType type)
    {
        switch (type)
        {
            case PowerUp.PowerUpType.Score:
                AddScore(10);
                break;
            case PowerUp.PowerUpType.Speed:
                break;
            case PowerUp.PowerUpType.Life:
                lives += 1;
                break;
        }
    }

    public void CollectPowerUp()
    {
        PowerUp.PowerUpType nextPowerUp = powerUpList.GetNext();
        PlayerCarController player = FindObjectOfType<PlayerCarController>();
        if (player != null)
        {
            player.ApplyPowerUp(nextPowerUp);
        }
    }

    public void CollectTrophy(Trophy trophy)
    {
        if (!IsTrophyObtained(trophy.trophyID))
        {
            obtainedTrophies.Add(trophy.trophyID);
            trophiesCount++;
            PlayerPrefs.SetInt("TrophiesCount", trophiesCount);
            PlayerPrefs.SetString("Trophy_" + trophy.trophyID, "obtained");
            UpdateTrophiesText();
        }
    }

    private bool IsTrophyObtained(string trophyID)
    {
        return PlayerPrefs.GetString("Trophy_" + trophyID, "not obtained") == "obtained";
    }

    private void UpdateTrophiesText()
    {
        if (trophiesText != null)
        {
            trophiesText.text = "Trofeos: " + trophiesCount + "/" + totalTrophies;
        }
    }

    private void LoadTrophies()
    {
        trophiesCount = PlayerPrefs.GetInt("TrophiesCount", 0);
        for (int i = 0; i < totalTrophies; i++)
        {
            string trophyID = "Trophy_" + i;
            if (IsTrophyObtained(trophyID))
            {
                obtainedTrophies.Add(trophyID);
            }
        }
        UpdateTrophiesText();
    }

    public void AddScore(int points)
    {
        score += points;
    }

    public void ResetScore()
    {
        score = 0;
    }

    public void ResetSpeed()
    {
        speed = 0;
    }

    public void ResetLives()
    {
        lives = 1;
    }
}
