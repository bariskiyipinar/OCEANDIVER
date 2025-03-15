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
    public Animator ÝsdeadPlayer;
    public GameObject GameOverPanel;

    private bool isGameOver = false;
    public AudioSource BgSound;
    public Animator GameOverAnim;
    private AudioSource CoinSound;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        CoinText.text = "" + CoinCount;
        CoinSound = GetComponent<AudioSource>();
        
    }

    void Update()
    {
        if (!isGameOver)  
        {
            PlayerMovement();
        }
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

            
            if (healthBar != null)
            {
                healthBar.transform.localScale = new Vector3(healthScale, healthBar.transform.localScale.y, healthBar.transform.localScale.z);
            }

            if(health <= 0 && ÝsdeadPlayer != null)
            {
                StartCoroutine(diePlayer(3));
                BgSound.Stop();
            }
            else
            {
                ÝsdeadPlayer.SetBool("Ýsdead", false);
            }

            Debug.Log("Can Azaldý! Yeni Can: " + health);
        }



        if (collision.CompareTag("Coin"))
        {
            CoinCount++;
            CoinText.text = CoinCount.ToString();
            CoinSound.Play();
            Destroy(collision.gameObject);

        }

        if (collision.gameObject.CompareTag("Finish1"))
        {
            SceneManager.LoadScene("Boss1");
        }
        if (collision.gameObject.CompareTag("Finish2"))
        {
            SceneManager.LoadScene("Boss2");
        }
    }

    IEnumerator diePlayer(int delay)
    {
        ÝsdeadPlayer.SetBool("Ýsdead", true);

        speed = 0;
        rb.velocity = Vector2.zero;
        rb.gravityScale=0;

        GameOverAnim.Play("gameOverAnim");

        isGameOver = true;

        yield return new  WaitForSeconds(delay);
        GameOverPanel.SetActive(true);
        Time.timeScale = 0;
    }

    
}