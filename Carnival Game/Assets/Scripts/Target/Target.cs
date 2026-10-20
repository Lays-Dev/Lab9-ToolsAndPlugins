using UnityEngine;

public class Target : MonoBehaviour
{
    [Header("Target Settings")]
    public float moveSpeed = 2f;
    public Vector2 targetSize = new Vector2(1f, 1f);
    public int pointValue = 10;

    [Header("Movement")]
    public float moveDistance = 3f;

    private Vector3 startPosition;
    private int moveDirection = 1;

    // Apply's settings to a target.
    public void Configure(
        float speed,
        Vector2 size,
        int points,
        float distance)
    {
        moveSpeed = speed;
        targetSize = size;
        pointValue = points;
        moveDistance = distance;

        // Apply the chosen size.
        transform.localScale = new Vector3(
            size.x, size.y, 1f);
    }

    // Spawning position
    void OnEnable()
    {
        startPosition = transform.position;
        moveDirection = 1;
    }

    void Update()
    {
        // Move the target left and right.
        transform.position += Vector3.right
            * moveDirection
            * moveSpeed
            * Time.deltaTime;

        // Max movement distance causes new direction. 
        float distanceFromStart =
            transform.position.x - startPosition.x;

        if (distanceFromStart >= moveDistance)
        {
            moveDirection = -1;
        }
        else if (distanceFromStart <= -moveDistance)
        {
            moveDirection = 1;
        }
    }
}
