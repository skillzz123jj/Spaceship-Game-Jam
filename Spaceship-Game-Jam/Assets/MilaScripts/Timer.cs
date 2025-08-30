using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public float timeRemaining = 60f;
    public bool timerIsRunning = false;
    public TextMeshProUGUI timeText;
    public int scene;

    public Color normalColor = Color.white;
    public Color warningColor = Color.red;
    public float warningThreshold = 11f; 

    void Start()
    {
        timerIsRunning = true;
        timeText.color = normalColor;
    }

    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                timeRemaining = 0;
                timerIsRunning = false;
                SceneManager.LoadScene(scene);

            }
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1; 
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        if (timeToDisplay <= warningThreshold)
            timeText.color = warningColor;
        else
            timeText.color = normalColor;
    }
}