using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooter : MonoBehaviour
{
    [Header("References")]
    public Transform firePoint;
    public BulletPool bulletPool;

    [Header("Movement")]
    public float moveSpeed = 5f;
    public float horizontalLimit = 7f;

    [Header("Shooting")]
    public float bulletSpeed = 10f;

    private float moveInput;

    void Update()
    {
        // Read left and right movement.
        moveInput = 0f;

        if (Keyboard.current != null)
        {
            if (Keyboard.current.aKey.isPressed ||
                Keyboard.current.leftArrowKey.isPressed)
            {
                moveInput = -1f;
            }

            if (Keyboard.current.dKey.isPressed ||
                Keyboard.current.rightArrowKey.isPressed)
            {
                moveInput = 1f;
            }

            // Shoot when Space is pressed.
            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                Shoot();
            }
        }
    }

    void FixedUpdate()
    {
        // Move the player horizontally.
        Vector3 position = transform.position;

        position.x += moveInput * moveSpeed * Time.fixedDeltaTime;

        // Keep the player inside the shooting area.
        position.x = Mathf.Clamp(
            position.x, -horizontalLimit, horizontalLimit);

        transform.position = position;
    }

    void Shoot()
    {
        // Get an unused bullet from the pool.
        GameObject bullet = bulletPool.GetBullet();

        if (bullet == null)
            return;

        // Place the bullet at the FirePoint.
        bullet.transform.position = firePoint.position;
        bullet.transform.rotation = Quaternion.identity;

        bullet.SetActive(true);

        // Shoot straight up.
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        rb.linearVelocity = Vector2.up * bulletSpeed;
    }
}
