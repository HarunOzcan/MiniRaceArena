using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RacePositionManager : MonoBehaviour
{
    public static RacePositionManager Instance;

    public List<RaceProgress> racers = new();
    public Transform[] checkpointTransforms;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        foreach (var r in racers)
        {
            int nextIndex = Mathf.Clamp(
                r.currentCheckpoint,
                0,
                checkpointTransforms.Length - 1
            );

            r.distanceToNextCheckpoint =
                Vector3.Distance(r.transform.position, checkpointTransforms[nextIndex].position);
        }

        racers = racers
            .OrderByDescending(r => r.currentLap)
            .ThenByDescending(r => r.currentCheckpoint)
            .ThenBy(r => r.distanceToNextCheckpoint)
            .ToList();
    }

    public int GetPosition(RaceProgress rp)
    {
        return racers.IndexOf(rp) + 1;
    }
}
