using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TimerMangement : MonoBehaviour
{
    //Create variables
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private float gameDuration = 30f;

    [SerializeField] private string resultsSceneName = "Risultati";

    private float timeRemaining;
    private bool isTimerRunning = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Set time remaining to game duration at the start
        timeRemaining = gameDuration;
        isTimerRunning = true;
        UpdateTimerDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isTimerRunning) return;

        if (timeRemaining > 0)
        {
            //Subtract deltatime to time remaining each frame so that the timer doesnt depend on fps
            timeRemaining -= Time.deltaTime;
            UpdateTimerDisplay();
        }
        else
        {
            timeRemaining = 0;
            isTimerRunning = false;
            UpdateTimerDisplay();
            OnTimerFinished();
        }
    }

    private void UpdateTimerDisplay()
    {
        if (timerText != null)
        {
            //Set ui text
            int seconds = Mathf.CeilToInt(timeRemaining);
            timerText.text = $"Time: {seconds}s";
        }
    }

    private void OnTimerFinished()
    {
        //When timer ends save high score and change scene
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (ScoreManager.Instance != null )
        {
            ScoreManager.Instance.SaveGameData();
        }

        SceneManager.LoadScene(resultsSceneName);
    }
}
