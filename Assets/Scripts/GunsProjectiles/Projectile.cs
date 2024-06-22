using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision != null)
        {
            if(collision.gameObject.GetComponent<EnemyHealth>() != null)
            {
                collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(5);
                Destroy(gameObject);
            }
        }

        if(gameObject != null)
        {
            Destroy(gameObject,2);
        }
    }
}
