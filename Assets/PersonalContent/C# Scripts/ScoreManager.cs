using System;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    //To make this accessible from results scene
    public static ScoreManager Instance { get; private set; }

    //Public variable free to get but not to set
    public int GemsCollected { get; private set; } = 0;

    //Event to notify the UI when gems count changes
    public static event Action<int> OnGemCollected;

    private void Awake()
    {
        //Fill instance variable
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //Collect gem function
    public void AddGems(int amount)
    {
        GemsCollected += amount;

        //Call event
        OnGemCollected?.Invoke(GemsCollected);
    }

    public void SaveGameData()
    {
        //Save current score
        PlayerPrefs.SetInt("LastScore",  GemsCollected);

        //Check and update highscore
        int currentHighScore = PlayerPrefs.GetInt("HighScore", 0);
        if (GemsCollected > currentHighScore)
        {
            PlayerPrefs.SetInt("HighScore", GemsCollected);
        }
    }

    public static int GetHighScore()
    {
        return PlayerPrefs.GetInt("HighScore", 0);
    }
}
