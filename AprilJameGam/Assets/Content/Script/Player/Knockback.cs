using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    public float thurst;
    public float knockTime;
    public float damage;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("breakable") && this.gameObject.CompareTag("Player"))
        {
            col.GetComponent<BreakObject>().Break();
        }

        if (col.gameObject.CompareTag("Enemy") || col.gameObject.CompareTag("Player"))
        {
            Rigidbody2D hit = col.GetComponent<Rigidbody2D>();
            if (hit != null)
            {
                Vector2 difference = hit.transform.position - transform.position;
                difference = difference.normalized * thurst;
                hit.AddForce(difference, ForceMode2D.Impulse);

                if (col.gameObject.CompareTag("Enemy") && col.isTrigger)
                {
                    hit.GetComponent<Enemy>().currentState = EnemyState.stagger;
                    col.GetComponent<Enemy>().Knock(hit, knockTime, damage);
                }
                if (col.CompareTag("Player"))
                {
                    if (col.GetComponent<PlayerMovement>().currentState != PlayerState.stagger)
                    {
                        hit.GetComponent<PlayerMovement>().currentState = PlayerState.stagger;
                        col.GetComponent<PlayerMovement>().Knock(knockTime, damage);
                    }
                }
            }
        }
    }
}
