using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Pool;

public class EnemyBoss : Enemy 
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Bullet bulletPrefab; // Reference to the bullet prefab
    [SerializeField] private Transform bulletSpawnPoint; // Spawn point for bullets
    [SerializeField] private float shootInterval = 3f; // Time between shots

    private Vector2 dir;
    private float shootTimer;
    private IObjectPool<Bullet> objectPool;

    private void Awake() 
    {
        PickRandomPositions();
        SetupBulletPool();
    }

    private void SetupBulletPool()
    {
        objectPool = new ObjectPool<Bullet>(
            createFunc: CreateBullet,
            actionOnGet: (bullet) => bullet.gameObject.SetActive(true),
            actionOnRelease: (bullet) => bullet.gameObject.SetActive(false),
            actionOnDestroy: (bullet) => Destroy(bullet.gameObject),
            collectionCheck: false,
            defaultCapacity: 10,
            maxSize: 20
        );
    }

    private Bullet CreateBullet()
    {
        Bullet bullet = Instantiate(bulletPrefab);
        bullet.objectPool = objectPool;
        return bullet;
    }

    private void Update() 
    {
        // Move horizontally
        transform.Translate(moveSpeed * Time.deltaTime * dir);

        Vector3 ePos = Camera.main.WorldToViewportPoint(new(transform.position.x, transform.position.y, transform.position.z));

        // Change direction if reaching screen bounds
        if (ePos.x < -0.05f && dir == Vector2.right) 
        {
            PickRandomPositions();
        }
        if (ePos.x > 1.05f && dir == Vector2.left) 
        {
            PickRandomPositions();
        }

        // Shooting logic
        shootTimer += Time.deltaTime;
        if (shootTimer >= shootInterval)
        {
            Shoot();
            shootTimer = 0;
        }
    }

    private void Shoot()
    {
        if (bulletPrefab == null || bulletSpawnPoint == null) return;

        Bullet bullet = objectPool.Get();
        bullet.transform.SetPositionAndRotation(bulletSpawnPoint.position, Quaternion.Euler(0, 0, 180)); // Shoot downwards
    }

    private void PickRandomPositions() 
    {
        Vector2 randPos;

        if (Random.Range(-1, 1) >= 0) 
        {
            dir = Vector2.right;
        }
        else 
        {
            dir = Vector2.left;
        }

        if (dir == Vector2.right) 
        {
            randPos = new(1.1f, Random.Range(0.1f, 0.95f));
        }
        else 
        {
            randPos = new(-0.01f, Random.Range(0.1f, 0.95f));
        }

        transform.position = Camera.main.ViewportToWorldPoint(randPos) + new Vector3(0, 0, 10);
    }
}