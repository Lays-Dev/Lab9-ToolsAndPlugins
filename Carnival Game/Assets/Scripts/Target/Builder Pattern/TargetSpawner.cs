using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    [Header("Target Prefab")]
    public GameObject targetPrefab;

    [Header("Spawn Positions")]
    public Transform[] spawnPoints;

    void Start()
    {
        SpawnTargets();
    }

    void SpawnTargets()
    {
        // Spawn points are in the inspector, empty GameObjects.
        // No spawn points = No targets.
        // Spawn points can't be to close to the edge of the screen.
        if (targetPrefab == null || spawnPoints.Length < 3)
        {
            Debug.LogWarning(
                "Assign a target prefab and at least 3 spawn points.");
            return;
        }

        // SCORE CODE. Points are assigned to each target type.

        // Big slow target.
        new TargetBuilder()
            .SetSpeed(1.5f)
            .SetSize(new Vector2(1.5f, 1.5f))
            .SetPoints(10)
            .SetMoveDistance(2f)
            .Build(targetPrefab, spawnPoints[0].position);

        // Normal target.
        new TargetBuilder()
            .SetSpeed(3f)
            .SetSize(new Vector2(1f, 1f))
            .SetPoints(20)
            .SetMoveDistance(3f)
            .Build(targetPrefab, spawnPoints[1].position);

        // Small fast target.
        new TargetBuilder()
            .SetSpeed(4.5f)
            .SetSize(new Vector2(0.6f, 0.6f))
            .SetPoints(30)
            .SetMoveDistance(4f)
            .Build(targetPrefab, spawnPoints[2].position);
    }
}
