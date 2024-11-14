using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{
    [Header("Bullet Stats")]
    public float bulletSpeed = 20f;
    public int damage = 10;
    private Rigidbody2D rb;
    private IObjectPool<Bullet> pool;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        // Make the bullet persist across scenes
        DontDestroyOnLoad(gameObject);
    }

    public void SetPool(IObjectPool<Bullet> bulletPool)
    {
        pool = bulletPool;
    }

    public void SetVelocity(Vector2 velocity)
    {
        rb.velocity = velocity;
        Debug.Log($"Bullet velocity set to: {velocity}");
    }

    private void OnEnable()
    {
        rb.velocity = transform.up * bulletSpeed;
        Debug.Log($"Bullet enabled with velocity: {rb.velocity}");
        Invoke(nameof(ReturnToPool), 2f); // Bullet returns to pool after 2 seconds if it doesn't hit anything
    }

    private void OnDisable()
    {
        CancelInvoke(); // Cancel Invoke if Bullet is disabled early
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check for specific collision types, e.g., enemies or walls
        if (other.CompareTag("Enemy"))
        {
            ReturnToPool(); // Return bullet to the pool
        }
    }

    private void OnBecameInvisible()
    {
        // Return bullet to the pool if it goes off-screen
        ReturnToPool();
    }

    private void ReturnToPool()
    {
        if (pool != null)
        {
            pool.Release(this); // Return bullet to pool
        }
        else
        {
            Destroy(gameObject); // If no pool exists, destroy the bullet
        }
    }
}
