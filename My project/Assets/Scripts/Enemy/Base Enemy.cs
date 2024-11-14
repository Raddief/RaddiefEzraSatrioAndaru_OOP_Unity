using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Stats")]
    public int level = 1; // Default level for the enemy

    protected Rigidbody2D rb;
    protected SpriteRenderer spriteRenderer;

    private void Awake()
    {
        // Initialize the components
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Ensure the enemy is not affected by gravity
        rb.gravityScale = 0;

        // Check if the current scene is "Main"
        if (SceneManager.GetActiveScene().name != "Main")
        {
            gameObject.SetActive(false);
        }
    }

    protected void Start()
    {
        // Ensure the enemy faces the player at the start
        if (gameObject.activeSelf)
        {
            FacePlayer();
        }
    }

    private void FacePlayer()
    {
        // Assume there's a Player GameObject with the tag "Player"
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            // Rotate the enemy to face the player
            Vector3 direction = player.transform.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            rb.rotation = angle;
        }
    }
}
