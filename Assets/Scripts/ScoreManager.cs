using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    [SerializeField]
    private int scoreToAdd;
    [SerializeField]
    private int score;

    [Space]
    [SerializeField]
    private Text scoreText;

    [SerializeField]
    private Text gwScoreText;
    [SerializeField]
    private Text goScoreText;
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

    private void Start()
    {
        SetInitialScore();
    }

    private void SetInitialScore()
    {
        scoreText.text = $"Score: {0}";
    }

    public void UpdateScore(int i)
    {
        score += i;
        scoreText.text = $"Score: {score}";
        gwScoreText.text = $"Score: {score}";
        goScoreText.text = $"Score: {score}";
    }
}
