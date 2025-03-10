using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyFish : MonoBehaviour
{


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Fish"))
        {
            Destroy(collision.gameObject);
        }

        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Bullet(Mermi Yok Etmek)

        if (collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);
        }
    }





}
