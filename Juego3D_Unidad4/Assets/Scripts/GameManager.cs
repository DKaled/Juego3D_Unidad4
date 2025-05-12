using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int bestScore;
    public int currentScore;
    public int currentLevel = 0;

    public static GameManager singleton;

    void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
        }
        else if (singleton != this)
        {
            Destroy(gameObject);
        }

        bestScore = PlayerPrefs.GetInt("BestScore");
    }

    public void NextLevel()
    {
        Debug.Log("Next Level");
        if (currentLevel >= FindFirstObjectByType<HelixController>().allStages.Count - 1)
        {
            Debug.Log("Game Over");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
            return;
        }
        currentLevel++;
        FindFirstObjectByType<HelixController>().LoadStage(currentLevel);
        FindFirstObjectByType<BallControler>().ResetBall();
        currentScore = 0;
    }

    public void RestartLevel()
    {
        Debug.Log("Restart Level");
        singleton.currentScore = 0;
        FindFirstObjectByType<BallControler>().ResetBall();
        FindFirstObjectByType<HelixController>().LoadStage(currentLevel);

    }

    public void AddScore(int scoreToAdd)
    {
        currentScore += scoreToAdd;
        if (currentScore > bestScore)
        {
            bestScore = currentScore;
            PlayerPrefs.SetInt("BestScore", bestScore);
        }
    }
}
