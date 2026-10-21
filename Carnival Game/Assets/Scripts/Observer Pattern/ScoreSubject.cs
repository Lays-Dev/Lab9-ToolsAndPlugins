using UnityEngine;

public delegate void ScoreUpdated(int score);

// Runs before ScoreDisplay so subject exists first
[DefaultExecutionOrder(-100)]
public class ScoreSubject : MonoBehaviour
{
    // Lets a bullet find this subject
    public static ScoreSubject Instance { get; private set; }

    // Listeners sign up here and get called when the score changes
    public event ScoreUpdated OnScoreUpdated;

    private int score;

    void Awake()
    {
        Instance = this;
    }

    public void AddPoints(int points)
    {
        score += points;

        // Tell listener the new total
        OnScoreUpdated?.Invoke(score);
    }
}
