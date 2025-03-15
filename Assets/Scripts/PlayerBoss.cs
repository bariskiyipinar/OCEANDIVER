using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerBoss : MonoBehaviour
{
    private Rigidbody2D rb;
    private float playerPower = 5f;

    //Health(Can)
    public float health = 100f;
    public Image healthBar;

    //Bullet(Mermi)
    public GameObject Bullet;
    private float bulletPower = 10f;
    public Transform BulletPoint;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(AutoFire());
    }


    void Update()
    {
        PlayerMovement();
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Fish"))
        {
            health -= 10f; // Caný 10 azalt

            float healthScale = Mathf.Clamp01(health / 100f); // 0 ile 1 arasýnda sýnýrla

            // Can barýnýn X ekseninde küçülmesini saðla
            if (healthBar != null)
            {
                healthBar.transform.localScale = new Vector3(healthScale, healthBar.transform.localScale.y, healthBar.transform.localScale.z);
            }
            if (health <= 0)
            {
                Time.timeScale = 0;
            }

            Debug.Log("Can Azaldý! Yeni Can: " + health);


        }

    }



    void PlayerMovement()

    {
        if (Input.GetKey(KeyCode.Space))
        {
            rb.gravityScale = 0;
            rb.velocity = Vector2.up * playerPower;
        }
        else
        {
            rb.gravityScale = 1;
        }
    }


    IEnumerator AutoFire()
    {
        while (true) 
        {
            yield return new WaitForSeconds(2f); 

         
            GameObject bullet = Instantiate(Bullet, BulletPoint.position, Quaternion.identity);
            Rigidbody2D rbBullet = bullet.GetComponent<Rigidbody2D>();

            if (rbBullet != null)
            {
                rbBullet.velocity = Vector2.right * bulletPower;
            }

            Destroy(bullet, 4f); 
        }
    }
}
