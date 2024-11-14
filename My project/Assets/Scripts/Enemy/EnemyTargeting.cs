using UnityEngine;

public class EnemyTargeting : Enemy
{
    [Header("Movement Settings")]
    [SerializeField] private float speed = 2f;
    private Transform playerTransform;

    protected new void Start()
    {
        base.Start();

        // Find the player object
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
    }

    private void Update()
    {
        if (playerTransform != null)
        {
            MoveTowardsPlayer();
            RotateTowardsPlayer();
        }
    }

    private void MoveTowardsPlayer()
    {
        // Calculate the direction towards the player
        Vector2 direction = (playerTransform.position - transform.position).normalized;

        // Move the enemy towards the player
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }

    private void RotateTowardsPlayer()
    {
        // Calculate the direction towards the player
        Vector2 direction = (playerTransform.position - transform.position).normalized;

        // Calculate the angle between the enemy and the player
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Update the rotation to face the player
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the enemy collides with the player
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject); // Destroy the enemy
        }
    }
}
