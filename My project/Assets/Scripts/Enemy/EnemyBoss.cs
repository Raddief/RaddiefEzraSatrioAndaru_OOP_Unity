using UnityEngine;

public class EnemyBoss : Enemy
{
    [Header("Movement Settings")]
    [SerializeField] private float speed = 2f;
    private Vector2 direction;

    [Header("Weapon Settings")]
    [SerializeField] private Weapon weapon; // Assuming Weapon is a script managing shooting
    [SerializeField] private float shootInterval = 2f;
    private float shootTimer;

    protected new void Start()
    {
        base.Start();

        // Initialize the weapon component
        weapon = GetComponentInChildren<Weapon>();

        // Randomly spawn from either left or right side
        float spawnX = Random.Range(0, 2) == 0 ? -10f : 10f;
        float spawnY = Random.Range(-5f, 5f);
        transform.position = new Vector3(spawnX, spawnY, 0f);
        transform.rotation = Quaternion.Euler(0, 0, 0); // Set initial rotation
    }

    private void Update()
    {
        Move();

        // Reverse direction when out of bounds
        if (transform.position.x < -10f || transform.position.x > 10f)
        {
            direction = -direction;
        }

        Shoot();

        // Ensure rotation on the Z-axis is always 0
        Vector3 rotationAngles = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(rotationAngles.x, rotationAngles.y, 0);
    }

    private void Move()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void Shoot()
    {
        shootTimer += Time.deltaTime;

        if (shootTimer >= shootInterval)
        {
            weapon.Shoot(); // Call the Shoot method from the Weapon class
            shootTimer = 0f;
        }
    }
}
