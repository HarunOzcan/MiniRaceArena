using UnityEngine;

public class RaceProgress : MonoBehaviour
{
    [Header("Checkpoint Settings")]
    public int totalCheckpoints = 3;

    private int nextCheckpointIndex = 1;

    public bool CanCountLap => nextCheckpointIndex > totalCheckpoints;

    public void HitCheckpoint(int checkpointIndex)
    {
        if (checkpointIndex != nextCheckpointIndex) return;

        Debug.Log($"✅ CP{checkpointIndex} geçildi");
        nextCheckpointIndex++;
    }

    public void ResetForNextLap()
    {
        nextCheckpointIndex = 1;
        Debug.Log("🔄 Checkpointler sıfırlandı");
    }
}
