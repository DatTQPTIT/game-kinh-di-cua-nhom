using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameTimer : MonoBehaviour
{
    public float totalSurvivalTime = 360f; 
    private float elapsedTime = 0f;
    private bool isWin = false;

    public TextMeshProUGUI timeText;
    public string winSceneName = "WinScene";

    void Update()
    {
        if (isWin) return;

        elapsedTime += Time.deltaTime;

        UpdateClockDisplay();

        if (elapsedTime >= totalSurvivalTime)
        {
            WinGame();
        }
    }

    void UpdateClockDisplay()
    {
        int totalMinutes = Mathf.FloorToInt(elapsedTime);

        int hours = totalMinutes / 60;
        int mins = totalMinutes % 60;

        if (timeText != null)
        {
            timeText.text = string.Format("{0:D2}:{1:D2} AM", hours, mins);
        }
    }

    void WinGame()
    {
        isWin = true;
        SceneManager.LoadScene(winSceneName);
    }
}