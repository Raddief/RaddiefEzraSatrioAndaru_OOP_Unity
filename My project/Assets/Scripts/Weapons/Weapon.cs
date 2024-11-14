using UnityEngine;
using UnityEngine.Pool;

public class Weapon : MonoBehaviour
{
    [Header("Weapon Stats")]
    [SerializeField] private float shootIntervalInSeconds = 3f;

    [Header("Bullets")]
    public Bullet bulletPrefab;
    [SerializeField] private Transform bulletSpawnPoint;

    [Header("Bullet Pool")]
    private IObjectPool<Bullet> bulletPool;

    private readonly bool collectionCheck = false;
    private readonly int defaultCapacity = 30;
    private readonly int maxSize = 100;
    private float timer;

    public Transform parentTransform;

    private void Awake()
    {
        // Create an Object Pool for the Bullet
        bulletPool = new ObjectPool<Bullet>(
            CreateBullet,
            OnTakeBulletFromPool,
            OnReturnBulletToPool,
            OnDestroyBullet,
            collectionCheck,
            defaultCapacity,
            maxSize
        );
        
        // Make sure the weapon itself persists across scenes
        DontDestroyOnLoad(gameObject);
    }

    private Bullet CreateBullet()
    {
        Bullet newBullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        newBullet.SetPool(bulletPool);
        DontDestroyOnLoad(newBullet.gameObject); // Ensure each bullet also persists across scenes
        return newBullet;
    }

    private void OnTakeBulletFromPool(Bullet bullet)
    {
        bullet.transform.position = bulletSpawnPoint.position;
        bullet.transform.rotation = bulletSpawnPoint.rotation;
        bullet.gameObject.SetActive(true);
    }

    private void OnReturnBulletToPool(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
    }

    private void OnDestroyBullet(Bullet bullet)
    {
        Destroy(bullet.gameObject);
    }

    private void Update()
    {
        // Manage the timer for bullet shooting
        timer += Time.deltaTime;

        if (timer >= shootIntervalInSeconds)
        {
            Shoot();
            timer = 0f;
        }
    }

    public void Shoot()
    {
        Bullet bulletInstance = bulletPool.Get(); // Retrieve Bullet from pool
        bulletInstance.SetVelocity(bulletSpawnPoint.up * bulletInstance.bulletSpeed); // Set bullet velocity
    }
}
