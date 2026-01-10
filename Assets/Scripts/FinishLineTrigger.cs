using UnityEngine;

public class FinishLineTrigger : MonoBehaviour
{
    public LapManager lapManager;
    public RaceProgress raceProgress;

    [Header("Anti-Spam")]
    public float minSecondsBetweenCounts = 2f;
    private float lastCountTime = -999f;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (lapManager == null || raceProgress == null) return;

        if (Time.time - lastCountTime < minSecondsBetweenCounts) return;
        lastCountTime = Time.time;

        // ✅ Cheat engeli: CP'ler tamam mı?
        if (!raceProgress.CanCountLap)
        {
            Debug.Log("⛔ FinishLine geçti ama checkpointler tamam değil. Lap sayılmadı.");
            return;
        }

        lapManager.RegisterLap();
        raceProgress.ResetForNextLap(); // yeni tur için CP1'e dön
    }
}
