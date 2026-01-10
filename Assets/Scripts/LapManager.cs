using UnityEngine;
using UnityEngine.Events;

public class LapManager : MonoBehaviour
{
    [Header("Race Settings")]
    [Min(1)] public int totalLaps = 3;

    [Header("State (Read Only)")]
    [SerializeField] private int currentLap = 0;
    [SerializeField] private bool raceFinished = false;

    [Header("Optional Events")]
    public UnityEvent onRaceStart;
    public UnityEvent onLapCompleted;
    public UnityEvent onRaceFinished;

    public int CurrentLap => currentLap;
    public int TotalLaps => totalLaps;
    public bool RaceFinished => raceFinished;

    void Start()
    {
        // Yarış başlatılmış gibi kabul ediyoruz (istersen sonra countdown ekleriz)
        currentLap = 0;
        raceFinished = false;
        onRaceStart?.Invoke();
    }

    public void RegisterLap()
    {
        if (raceFinished) return;

        currentLap++;

        onLapCompleted?.Invoke();
        Debug.Log($"🏁 LAP: {currentLap}/{totalLaps}");

        if (currentLap >= totalLaps)
        {
            FinishRace();
        }
    }

    private void FinishRace()
    {
        raceFinished = true;
        Debug.Log("✅ RACE FINISHED!");
        onRaceFinished?.Invoke();

        // Basit bitiriş: zamanı durdur
        Time.timeScale = 0f;

        // Cursor serbest (istersen)
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
