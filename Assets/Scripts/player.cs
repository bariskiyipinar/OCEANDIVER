using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private float playerPower = 5f;
    public int speed = 3;
    public GameObject healthBar; // Can barý objesi
    public Text CoinText;
    private int CoinCount = 0;
    public  float health = 100f; // Can deðeri baþlangýçta 100 olsun

    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        CoinText.text = "" + CoinCount;
    }

    void Update()
    {
        PlayerMovement();
    }

    void PlayerMovement()
    {

        transform.Translate(Vector2.right * speed * Time.deltaTime);

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

            Debug.Log("Can Azaldý! Yeni Can: " + health);
        }



        if (collision.CompareTag("Coin"))
        {
            CoinCount++;
            CoinText.text = CoinCount.ToString();
            Destroy(collision.gameObject);

        }

        if (collision.gameObject.CompareTag("Finish1"))
        {
            SceneManager.LoadScene("Boss1");
        }
    }
}