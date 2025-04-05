using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Timeline.TimelinePlaybackControls;

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

    public Animator GameOverAnim;
    public Animator ÝsdeadPlayer;
    public GameObject GameOverPanel;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        PlayerMovement();

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Fish") )
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
                StartCoroutine(diePlayer(3));
            }

            Debug.Log("Can Azaldý! Yeni Can: " + health);
        }
        else if (collision.CompareTag("Fish2"))
        {
            health -= 15f; // Caný 15 azalt

            float healthScale = Mathf.Clamp01(health / 100f); // 0 ile 1 arasýnda sýnýrla

            // Can barýnýn X ekseninde küçülmesini saðla
            if (healthBar != null)
            {
                healthBar.transform.localScale = new Vector3(healthScale, healthBar.transform.localScale.y, healthBar.transform.localScale.z);
            }
            if (health <= 0)
            {
                StartCoroutine(diePlayer(3));
            }

            Debug.Log("Can Azaldý! Yeni Can: " + health);
        }

    }
    IEnumerator diePlayer(int delay)
    {
        ÝsdeadPlayer.SetBool("Ýsdead", true);
        rb.velocity = Vector2.zero;
        rb.gravityScale = 0;
       
         
        GameOverAnim.gameObject.SetActive(true);
        GameOverAnim.Play("gameOverAnim");


        yield return new WaitForSeconds(delay);
        GameOverPanel.SetActive(true);
        Time.timeScale = 0;
    }
  

    void PlayerMovement()

    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                rb.gravityScale = 0;
                rb.velocity = Vector2.up * playerPower;
            }
            else
            {

                rb.gravityScale = 1;
            }
        }


       
    }
    
    private void BulletAttack()
    {
        GameObject bullet = Instantiate(Bullet, BulletPoint.position, Quaternion.identity);
        Rigidbody2D rbBullet = bullet.GetComponent<Rigidbody2D>();

        if (rbBullet != null)
        {
            rbBullet.velocity = Vector2.right * bulletPower;
        }

        Destroy(bullet, 4f);

    }

           
      

    public void FireButton()
    {
      
        BulletAttack();
    }
}
