using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    public Text scoreText;

    // True after we sign up so we do not sign up twice
    private bool subscribed;

    void OnEnable()
    {
        // Sign up when this object turns on
        Subscribe();
    }

    void OnDisable()
    {
        // Nothing to undo if we never signed up
        if (!subscribed || ScoreSubject.Instance == null)
            return;

        // Stop listening so this object is not called after it turns off
        ScoreSubject.Instance.OnScoreUpdated -= UpdateScore;
        subscribed = false;
    }

    void Subscribe()
    {
        // Skip if we are already listening or the subject is not in the scene yet
        if (subscribed || ScoreSubject.Instance == null)
            return;

        // When the score changes call UpdateScore
        ScoreSubject.Instance.OnScoreUpdated += UpdateScore;
        subscribed = true;
    }

    void UpdateScore(int score)
    {
        scoreText.text = "Score: " + score;
    }
}
