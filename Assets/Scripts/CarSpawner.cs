using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    public GameObject[] carPrefabs;
    public Transform[] spawnPoints;

    void Start()
    {
        int playerCount = GameManager.Instance.playerCount;

        for (int i = 0; i < playerCount; i++)
        {
            GameObject car = Instantiate(carPrefabs[i], spawnPoints[i].position, spawnPoints[i].rotation);

            // Player ID ver
            CarController controller = car.GetComponent<CarController>();
            if (controller != null)
            {
                controller.playerID = i;
            }

            // PLAYER 1 KAMERASI
            if (i == 0)
            {
                CameraFollow cam = Camera.main.GetComponent<CameraFollow>();
                if (cam != null)
                {
                    cam.target = car.transform;
                }
            }
        }
    }
}
