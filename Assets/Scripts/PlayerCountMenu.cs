using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCountMenu : MonoBehaviour
{
    [SerializeField] string raceSceneName = "RaceScene";

    public void Select2Players()
    {
        SetPlayersAndStart(2);
    }

    public void Select3Players()
    {
        SetPlayersAndStart(3);
    }

    public void Select4Players()
    {
        SetPlayersAndStart(4);
    }

    void SetPlayersAndStart(int count)
    {
        if (GameManager.Instance == null)
        {
            Debug.LogError("GameManager yok! PlayerCountScene'de GameManager objesi var mý?");
            return;
        }

        GameManager.Instance.playerCount = count;
        SceneManager.LoadScene(raceSceneName);
    }
}
