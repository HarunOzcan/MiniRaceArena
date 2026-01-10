using UnityEngine;

public class FinishLineTrigger : MonoBehaviour
{
    public LapManager lapManager;
    public RaceProgress raceProgress;

    private float lastTriggerTime;
    public float cooldown = 2f;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (lapManager == null || raceProgress == null) return;

        if (Time.time - lastTriggerTime < cooldown) return;
        lastTriggerTime = Time.time;

        if (!raceProgress.CanCountLap)
        {
            Debug.Log("⛔ Checkpointler tamam değil, lap sayılmadı");
            return;
        }

        lapManager.RegisterLap();
        raceProgress.ResetForNextLap();
    }
}
