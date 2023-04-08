using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    private Rigidbody2D rb;
    private Animator anim;

    private Vector3 change;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        change = Vector3.zero;

        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");
        UpdateAnimAndMove();

    }
    
        void UpdateAnimAndMove()
        {
            if (change != Vector3.zero)
            {
                MoveCharacter();
                anim.SetFloat("moveX", change.x);
                anim.SetFloat("moveY", change.y);
                anim.SetBool("moving", true);
            }
            else
            {
                anim.SetBool("moving", false);
            }
        }

    void MoveCharacter()
    {
        rb.MovePosition(transform.position + change * moveSpeed * Time.fixedDeltaTime);
    } 
}
