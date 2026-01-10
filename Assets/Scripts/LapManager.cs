using UnityEngine;

public class LapManager : MonoBehaviour
{
    [Header("Race Settings")]
    [Min(1)] public int totalLaps = 3;

    [Header("State (Read Only)")]
    [SerializeField] private int currentLap = 0;
    [SerializeField] private bool raceFinished = false;

    [Header("UI References")]
    public RaceUIManager uiManager;
    public RaceHUD hud;

    public int CurrentLap => currentLap;
    public bool RaceFinished => raceFinished;

    void Start()
    {
        currentLap = 0;
        raceFinished = false;
        Time.timeScale = 1f;

        // HUD ilk değer
        if (hud != null)
            hud.UpdateLap();
    }

    // FinishLineTrigger burayı çağırır
    public void RegisterLap()
    {
        if (raceFinished) return;

        currentLap++;
        Debug.Log($"🏁 LAP: {currentLap}/{totalLaps}");

        // HUD güncelle
        if (hud != null)
            hud.UpdateLap();

        if (currentLap >= totalLaps)
        {
            FinishRace();
        }
    }

    private void FinishRace()
    {
        raceFinished = true;
        Debug.Log("✅ RACE FINISHED");

        // HUD timer durdur
        if (hud != null)
            hud.StopTimer();

        // Finish panel aç
        if (uiManager != null)
            uiManager.ShowFinish();

        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
