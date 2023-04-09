using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState { 
    walk,
    attack,
    interact
}

public class PlayerMovement : MonoBehaviour
{
    public PlayerState currentState;

    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Animator anim;
    private Vector3 change;

    void Start()
    {
        currentState = PlayerState.walk;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        anim.SetFloat("moveX", 0);
        anim.SetFloat("moveY", -1);
    }

    void Update()
    {
        change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");


        if (Input.GetButtonDown("Fire1") && currentState != PlayerState.attack)
        {
            StartCoroutine(AttackCo());
        } 
        else if (currentState == PlayerState.walk)
        {
            UpdateAnimAndMove();
        }

    }
    
    private IEnumerator AttackCo()
    {
        anim.SetBool("attacking", true);
        currentState = PlayerState.attack;
        yield return null;
        anim.SetBool("attacking", false);
        yield return new WaitForSeconds(.33f);
        currentState = PlayerState.walk;
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
        change.Normalize();
        rb.MovePosition(transform.position + change * moveSpeed * Time.fixedDeltaTime);
    } 
}
