using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Range(2, 4)]
    public int playerCount = 2;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
