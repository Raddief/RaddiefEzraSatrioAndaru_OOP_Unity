using UnityEngine;

public class EnemyHorizontal : Enemy
{
    [Header("Movement Settings")]
    [SerializeField] private float speed = 2f;
    private Vector2 direction;

    protected new void Start()
    {
        base.Start();

        // Randomly spawn from either left or right side
        float spawnX = Random.Range(0, 2) == 0 ? -10f : 10f;
        float spawnY = Random.Range(-5f, 5f);
        transform.position = new Vector3(spawnX, spawnY, 0f);
        transform.rotation = Quaternion.Euler(0, 0, 0); // Set initial rotation

        // Set direction based on spawn position
        if (spawnX < 0) 
        {
            direction = Vector2.right;
        }
        else 
        {
            direction = Vector2.left;
        }
    }

    private void Update()
    {
        Move();

        // Reverse direction when out of bounds
        if (transform.position.x < -10f || transform.position.x > 10f)
        {
            direction = -direction;
        }

        Vector3 rotationAngles = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(rotationAngles.x, rotationAngles.y, 0);
    }

    private void Move()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }
}
