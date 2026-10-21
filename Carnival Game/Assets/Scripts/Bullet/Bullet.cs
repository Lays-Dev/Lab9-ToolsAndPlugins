using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 3f;

    private float timer;

    void OnEnable()
    {
        // Reset the timer whenever the bullet is reused.
        timer = 0f;
    }

    void Update()
    {
        // Move up.
        transform.Translate(Vector2.up * speed * Time.deltaTime);

        timer += Time.deltaTime;

        // Return the bullet to the pool.
        if (timer >= lifetime)
        {
            gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Deactivate the bullet when it hits a target.
        if (other.CompareTag("Target"))
        {
            // Get the target that was hit so we can read its points
            Target target = other.GetComponent<Target>();

            // Tell the score subject to add this target's point value
            if (target != null && ScoreSubject.Instance != null)
                ScoreSubject.Instance.AddPoints(target.pointValue);

            gameObject.SetActive(false);
            other.gameObject.SetActive(false);
        }
    }
}
