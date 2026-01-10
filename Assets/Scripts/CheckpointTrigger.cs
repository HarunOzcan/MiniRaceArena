using UnityEngine;

public class CheckpointTrigger : MonoBehaviour
{
    public RaceProgress raceProgress;
    [Min(1)] public int checkpointIndex = 1;

    private float lastHitTime;
    public float cooldown = 1f;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (raceProgress == null) return;

        if (Time.time - lastHitTime < cooldown) return;
        lastHitTime = Time.time;

        raceProgress.HitCheckpoint(checkpointIndex);
    }
}
