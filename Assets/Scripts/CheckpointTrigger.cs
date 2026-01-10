using UnityEngine;

public class CheckpointTrigger : MonoBehaviour
{
    public RaceProgress raceProgress;
    [Min(1)] public int checkpointIndex = 1;

    [Header("Anti-Spam")]
    public float minSecondsBetweenHits = 1f;
    private float lastHitTime = -999f;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (raceProgress == null) return;

        if (Time.time - lastHitTime < minSecondsBetweenHits) return;
        lastHitTime = Time.time;

        raceProgress.HitCheckpoint(checkpointIndex);
    }
}
