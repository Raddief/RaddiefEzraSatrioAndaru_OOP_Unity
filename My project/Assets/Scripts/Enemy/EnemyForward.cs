using UnityEngine;

public class EnemyForward : Enemy
{
    [Header("Movement Settings")]
    [SerializeField] private float speed = 2f;
    private Vector2 direction = Vector2.down;

    protected new void Start()
    {
        base.Start();

        // Spawn from the top of the screen
        float spawnX = Random.Range(-5f, 5f);
        float spawnY = 10f;
        transform.position = new Vector3(spawnX, spawnY, 0f); // Ensure z is 0
        transform.rotation = Quaternion.Euler(0, 0, 0); // Set initial rotation
    }

    private void Update()
    {
        Move();

        // Deactivate when out of bounds
        if (transform.position.y < -10f)
        {
            gameObject.SetActive(false); // Deactivate the enemy
        }

        // Ensure rotation on the Z-axis is always 0
        Vector3 rotationAngles = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(rotationAngles.x, rotationAngles.y, 0);
    }

    private void Move()
    {
        // Move the enemy and ensure z is always 0
        Vector3 newPosition = transform.position + (Vector3)(direction * speed * Time.deltaTime);
        newPosition.z = 0f; // Ensure z is 0
        transform.position = newPosition;
    }
}
