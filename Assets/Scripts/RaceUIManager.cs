using UnityEngine;
using UnityEngine.SceneManagement;

public class RaceUIManager : MonoBehaviour
{
    [Header("UI")]
    public GameObject finishPanel;

    void Awake()
    {
        // Güvenlik: zaman durmuşsa aç
        Time.timeScale = 1f;
    }

    void Start()
    {
        if (finishPanel == null)
        {
            Debug.LogError("❌ FinishPanel atanmadı!");
            return;
        }

        // Başta kapalı olacak
        finishPanel.SetActive(false);

        Debug.Log("✅ RaceUIManager çalışıyor, FinishPanel kapalı.");
    }

    // 🔥 Yarış bitince çağrılacak
    public void ShowFinish()
    {
        if (finishPanel == null)
        {
            Debug.LogError("❌ FinishPanel NULL, açamıyorum!");
            return;
        }

        Debug.Log("🏁 FINISH PANEL AÇILDI");
        finishPanel.SetActive(true);

        // UI için gerekli
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // 🔁 Restart butonu burayı çağırır
    public void RestartRace()
    {
        Debug.Log("🔄 RESTART BASILDI");

        Time.timeScale = 1f;

        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }

    public void QuitGame()
    {
        Debug.Log("❌ QUIT");
        Application.Quit();
    }
}
