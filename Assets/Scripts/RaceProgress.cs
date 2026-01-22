using UnityEngine;

public class RaceProgress : MonoBehaviour
{
    [Header("Checkpoint Settings")]
    public int totalCheckpoints = 3;

    [Header("Race State")]
    public int currentLap = 0;
    public int currentCheckpoint = 0;
    public float distanceToNextCheckpoint;

    private int nextCheckpointIndex = 1;

    public bool CanCountLap => nextCheckpointIndex > totalCheckpoints;

    public void HitCheckpoint(int checkpointIndex)
    {
        if (checkpointIndex != nextCheckpointIndex) return;

        Debug.Log($"✅ CP{checkpointIndex} geçildi");

        currentCheckpoint = checkpointIndex;
        nextCheckpointIndex++;
    }

    public void ResetForNextLap()
    {
        nextCheckpointIndex = 1;
        currentCheckpoint = 0;
        currentLap++;

        Debug.Log("🔄 Yeni tura geçildi");
    }
}
