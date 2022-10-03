using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField]
    private GameObject gameOverScreen;

    [SerializeField]
    private GameObject gameWin;

    [SerializeField]
    private Text enemiesKilled;

    [SerializeField]
    private Text lives;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Update()
    {
        lives.text = $"Lives: {GameManager.Instance.livesLeft}";
    }
    public void UpdateEnemyKilledCount(int count)
    {
        enemiesKilled.text = $"Enemies Killed: {count}";
    }

    public void GameOver()
    {
        gameOverScreen.SetActive(true);

        Time.timeScale = 0;
    }

    public void GameWin()
    {
        gameWin.SetActive(true);

        Time.timeScale = 0;
    }
}
