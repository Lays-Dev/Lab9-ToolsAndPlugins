using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    [Header("Bullet Settings")]
    public GameObject bulletPrefab;
    // How many bullets to keep in the pool. 
    public int poolSize = 20;

    // Creates an empty list that stores references to all the bullet GameObjects.
    private List<GameObject> bullets = new List<GameObject>();

    void Awake()
    {
        // Create the bullets at start.
        for (int i = 0; i < poolSize; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab);

            // Deactivate the bullets until needed.
            bullet.SetActive(false);

            // Store it in the bullet pool.
            bullets.Add(bullet);
        }
    }

    public GameObject GetBullet()
    {
        // Find an unused bullet to use.
        foreach (GameObject bullet in bullets)
        {
            if (!bullet.activeInHierarchy)
            {
                return bullet;
            }
        }

        // All bullets are currently in use.
        return null;
    }
}
