using System.Collections;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] float speed = 0.2f;
    [SerializeField] float rotateSpeed = 5f;
    Vector2 newPosition;

    void Start()
    {
        ChangePosition();
    }

    void Update()
    {
        //gerak ke posisi
        transform.position = Vector2.MoveTowards(transform.position, newPosition, speed * Time.deltaTime);
        
        //roatasi portal
        transform.Rotate(Vector3.forward, rotateSpeed * Time.deltaTime);

        // cek kalau portal dekat dengan newPosition dan ubah jika perlu
        if (Vector2.Distance(transform.position, newPosition) < 0.5f)
        {
            ChangePosition();
        }

        // cek kalau player punya weapon
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            Weapon weapon = player.GetComponentInChildren<Weapon>();
            bool hasWeapon = weapon != null;
            
            GetComponent<SpriteRenderer>().enabled = hasWeapon;
            GetComponent<Collider2D>().enabled = hasWeapon;
        }
    }

    void ChangePosition()
    {
        // Generate posisi random dalam range tersebut
        newPosition = new Vector2(Random.Range(-5f, 5f), Random.Range(-5f, 5f));
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Cek kalau player colide dengan portal (asteroid)
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player collided with the portal. Starting scene transition.");

            // mentrigger scene ke main
            GameManager.Instance.LevelManager.LoadScene("Main");
        }
    }
}
