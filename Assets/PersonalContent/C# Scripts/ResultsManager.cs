using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ResultsManager : MonoBehaviour
{
    //Create variables for UI
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;

    [SerializeField] private string menuSceneName = "Menu";
    [SerializeField] private string gameSceneName = "Game";

    [SerializeField] private GameObject firstSelectedButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Get last score in player prefs
        int lastScore = PlayerPrefs.GetInt("LastScore", 0);

        //Save bew record if is greater than the previous one
        int currentHighScore = PlayerPrefs.GetInt("HighScore", 0);
        if (lastScore > currentHighScore)
        {
            PlayerPrefs.SetInt("HighScore", lastScore);
            PlayerPrefs.Save();
            currentHighScore = lastScore;
        }

        //Show datas on UI
        if (scoreText != null)
        {
            scoreText.text = $"SCORE: {lastScore}";
        }

        if (highScoreText  != null)
        {
            highScoreText.text = $"HIGH SCORE: {currentHighScore}";
        }

        //For controller set first selected button
        if (firstSelectedButton != null)
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(firstSelectedButton);

        }
    }

    //Go to menu funtion
    public void GoToMenu()
    {
        SceneManager.LoadScene(menuSceneName);
    }

    //Restart game function
    public void RestartGame()
    {
        SceneManager.LoadScene(gameSceneName);
    }

   
}
