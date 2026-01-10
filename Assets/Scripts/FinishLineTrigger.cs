using UnityEngine;

public class FinishLineTrigger : MonoBehaviour
{
    public LapManager lapManager;

    [Header("Anti-Spam")]
    public float minSecondsBetweenCounts = 2f;
    private float lastCountTime = -999f;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (lapManager == null) return;

        // Ayný anda iki collider tetiklerse diye basit cooldown
        if (Time.time - lastCountTime < minSecondsBetweenCounts) return;
        lastCountTime = Time.time;

        lapManager.RegisterLap();
    }
}
