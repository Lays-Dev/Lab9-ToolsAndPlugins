using UnityEngine;

// The TargetBuilder class creates targets with different settings.
// It uses the Builder Pattern to configure a target before creating it.
public class TargetBuilder
{
    // Target setting variables. They store the setting info per target.
    private float speed;
    private Vector2 size;
    private int points;
    private float distance;

    public TargetBuilder SetSpeed(float newSpeed)
    {
        speed = newSpeed;
        return this;
    }

    public TargetBuilder SetSize(Vector2 newSize)
    {
        size = newSize;
        return this;
    }

    // SCORE CODE. Sets the amount of points.
    public TargetBuilder SetPoints(int newPoints)
    {
        points = newPoints;
        return this;
    }

    public TargetBuilder SetMoveDistance(float newDistance)
    {
        distance = newDistance;
        return this;
    }

    // Builds the target. Prefab and position are assigned in the inspector.
    public Target Build(GameObject prefab, Vector3 position)
    {
        GameObject targetObject =
            Object.Instantiate(prefab, position, Quaternion.identity);

        Target target = targetObject.GetComponent<Target>();

        target.Configure(speed, size, points, distance);

        return target;
    }
}
