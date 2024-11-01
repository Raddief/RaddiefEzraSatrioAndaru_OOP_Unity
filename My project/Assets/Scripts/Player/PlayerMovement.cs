using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Vector2 maxSpeed;
    [SerializeField] Vector2 timeToFullSpeed;
    [SerializeField] Vector2 timeToStop;
    [SerializeField] Vector2 stopClamp;

    Vector2 moveDirection;
    Vector2 moveVelocity;
    Vector2 moveFriction;
    Vector2 stopFriction;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        moveVelocity = 2 * maxSpeed / timeToFullSpeed;
        moveFriction = -2 * maxSpeed / (timeToFullSpeed * timeToFullSpeed);
        stopFriction = -2 * maxSpeed / (timeToStop * timeToStop);
    }

    public void Move()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        moveDirection = new Vector2(inputX, inputY).normalized;

        if (moveDirection.magnitude > 0)
        {
            rb.velocity += moveDirection * moveVelocity * Time.deltaTime;
        
        }
        else
        {
            if (Mathf.Abs(rb.velocity.x) < stopClamp.x || Mathf.Abs(rb.velocity.y) < stopClamp.y)
            {
                rb.velocity = Vector2.zero;
            }
            else 
            {
                Vector2 friction = GetFriction();
                rb.velocity = friction * Time.deltaTime;
            }
        }

        rb.velocity = new Vector2(
            Mathf.Clamp(rb.velocity.x, -maxSpeed.x, maxSpeed.x),
            Mathf.Clamp(rb.velocity.y, -maxSpeed.y, maxSpeed.y)
        );

        if (rb.velocity.magnitude > maxSpeed.magnitude)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed.magnitude;
        }
    }

    public Vector2 GetFriction()
    {
        return rb.velocity != Vector2.zero ?  moveFriction : stopFriction ;
    }

    public void MoveBound()
    {
    
    }

    public bool IsMoving()
    {
        return moveDirection != Vector2.zero;
    }
}