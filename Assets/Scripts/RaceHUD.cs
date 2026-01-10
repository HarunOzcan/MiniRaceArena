using UnityEngine;
using TMPro;

public class RaceHUD : MonoBehaviour
{
    [Header("UI")]
    public TMP_Text lapText;
    public TMP_Text timeText;

    [Header("Race")]
    public LapManager lapManager;

    private float raceTime;
    private bool running = true;

    void Update()
    {
        if (!running) return;

        raceTime += Time.deltaTime;
        timeText.text = FormatTime(raceTime);
    }

    public void UpdateLap()
    {
        lapText.text = $"Lap {lapManager.CurrentLap} / {lapManager.totalLaps}";
    }

    public void StopTimer()
    {
        running = false;
    }

    string FormatTime(float t)
    {
        int min = (int)(t / 60);
        float sec = t % 60;
        return $"{min:00}:{sec:00.00}";
    }
}
