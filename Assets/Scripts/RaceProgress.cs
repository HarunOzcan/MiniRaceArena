using UnityEngine;

public class RaceProgress : MonoBehaviour
{
    [Header("Checkpoint Order")]
    public int totalCheckpoints = 3;

    private int nextCheckpointIndex = 1; // 1'den başlatıyoruz: CP1 bekleniyor

    public bool CanCountLap => nextCheckpointIndex > totalCheckpoints;

    public void HitCheckpoint(int checkpointIndex)
    {
        // Sıradaki checkpoint doğru mu?
        if (checkpointIndex != nextCheckpointIndex) return;

        nextCheckpointIndex++;
        Debug.Log($"✅ Checkpoint OK: CP{checkpointIndex}");

        if (CanCountLap)
            Debug.Log("🏁 All checkpoints passed. FinishLine can count lap!");
    }

    public void ResetForNextLap()
    {
        nextCheckpointIndex = 1;
        Debug.Log("🔄 Checkpoints reset for next lap (CP1 bekleniyor)");
    }
}
