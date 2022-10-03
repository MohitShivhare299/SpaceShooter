using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private int totalEnemyKilled;

    public int livesLeft = 3;

    public static Vector3 screenBounds;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }

    }

    // Start is called before the first frame update
    void OnEnable()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }

    public void OnEnemyKilled()
    {
        totalEnemyKilled++;

        UIManager.Instance.UpdateEnemyKilledCount(totalEnemyKilled);
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void OnPlayerHit()
    {
        livesLeft--;

        if (livesLeft <= 0)
            UIManager.Instance.GameOver();
        else
            PlayerController.Instance.RespawnPlayer();
    }
}
