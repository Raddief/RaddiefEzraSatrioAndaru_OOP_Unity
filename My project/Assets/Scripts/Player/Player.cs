using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.LowLevel;

public class Player : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public Animator animator;
    public static Player Instance {get; private set;}
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this); 
        }
        else
        {
            Instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        animator = GameObject.Find("EngineEffect").GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        playerMovement.Move();
    }

    void LateUpdate()
    {
        animator.SetBool("IsMoving", playerMovement.IsMoving());
    }
}
