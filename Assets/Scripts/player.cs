using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    public float playerPower = 5f;
    public int speed = 3;
    public GameObject healthBar;

    public Text CoinText;
    public int health = 100;
    public Animator ›sdeadPlayer;
    public GameObject GameOverPanel;

    private bool isGameOver = false;
    public AudioSource CharacterDamageSound;
    public Animator GameOverAnim;
    public AudioSource BgSound;
    private AudioSource CoinSound;

    private bool isTouching = false;
    public bool isFast = false;

    public static bool istouchfinish1, istouchfinish2, istouchfinish3, istouchfinish4;
    private Env›tems envitems;
    private SpriteRenderer playerRenderer;
    private bool ›scolor = false;
   

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        CoinSound = GetComponent<AudioSource>();
        GameOverAnim.gameObject.SetActive(false);
        envitems = FindObjectOfType<Env›tems>();
        playerRenderer=GetComponent<SpriteRenderer>();
        if (envitems == null)
        {
            Debug.LogWarning("Env›tems bulunamad˝, FastSwim Áal˝˛mayabilir.");
        }

        if (GameManager.instance != null)
        {
            UpdateCoinText();
        }
        else
        {
            Debug.LogWarning("GameManager sahnede bulunamad˝! Coinler kaydedilmeyebilir.");
        }

       

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
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began && !isTouching)
            {
                isTouching = true;
                rb.gravityScale = 0;
                rb.velocity = Vector2.up * playerPower;
            }
            else
            {
                isTouching = false;
                rb.gravityScale = 1;
            }
        }

        if (!isTouching || isFast)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Fish") && !isFast)
        {
            health -= 5;
            playerRenderer.color = Color.red;
            ›scolor = true;
            StartCoroutine(ResetColor(0.3f));
            
            CharacterDamageSound.Play();
            float healthScale = Mathf.Clamp01(health / 100f);

            if (healthBar != null)
            {
                healthBar.transform.localScale = new Vector3(healthScale, healthBar.transform.localScale.y, healthBar.transform.localScale.z);
            }
            if (health <= 0)
            {
                
                StartCoroutine(diePlayer(3));
                BgSound.Stop();
            }
            else
            {
                ›sdeadPlayer.SetBool("›sdead", false);
            }
        }

        else if (collision.CompareTag("Fish2") && !isFast)
        {
            health -= 7;
            playerRenderer.color = Color.red;
            ›scolor = true;
            StartCoroutine(ResetColor(0.3f));


            CharacterDamageSound.Play();

            float healthScale = Mathf.Clamp01(health / 100f);

            if (healthBar != null)
            {
                healthBar.transform.localScale = new Vector3(healthScale, healthBar.transform.localScale.y, healthBar.transform.localScale.z);
            }

            if (health <= 0)
            {

                StartCoroutine(diePlayer(3));
                BgSound.Stop();
            }
            else
            {
                ›sdeadPlayer.SetBool("›sdead", false);
            }
        }
        else if (collision.CompareTag("Fish3") && !isFast)
        {
            health -= 9;
            playerRenderer.color = Color.red;
            ›scolor = true;
            StartCoroutine(ResetColor(0.3f));
            CharacterDamageSound.Play();
            float healthScale = Mathf.Clamp01(health / 100f);

            if (healthBar != null)
            {
                healthBar.transform.localScale = new Vector3(healthScale, healthBar.transform.localScale.y, healthBar.transform.localScale.z);
            }

            if (health <= 0)
            {

                StartCoroutine(diePlayer(3));
                BgSound.Stop();
            }
            else
            {
                ›sdeadPlayer.SetBool("›sdead", false);
            }
        }

        else if (collision.CompareTag("Fish4") && !isFast)
        {
            health -= 10;
            playerRenderer.color = Color.red;
            ›scolor = true;
            StartCoroutine(ResetColor(0.3f));
            CharacterDamageSound.Play();
            float healthScale = Mathf.Clamp01(health / 100f);

            if (healthBar != null)
            {
                healthBar.transform.localScale = new Vector3(healthScale, healthBar.transform.localScale.y, healthBar.transform.localScale.z);
            }

            if (health <= 0)
            {

                StartCoroutine(diePlayer(3));
                BgSound.Stop();
            }
            else
            {
                ›sdeadPlayer.SetBool("›sdead", false);
            }
        }


        if (collision.CompareTag("Coin"))
        {
            if (GameManager.instance != null)
            {
                GameManager.instance.AddCoin(1);
                UpdateCoinText();
            }

            if (CoinSound != null)
            {
                CoinSound.Play();
            }

            Destroy(collision.gameObject);
        }

        if (collision.CompareTag("Finish1"))
        {
            istouchfinish1 = true;  
            SceneManager.LoadScene("Boss1");
        }

        if (collision.CompareTag("Finish2"))
        {
            istouchfinish2 = true;
            SceneManager.LoadScene("Boss2");
        }

        if (collision.CompareTag("Finish3"))
        {
            istouchfinish3 = true;
            SceneManager.LoadScene("Boss3");
        }
        if (collision.CompareTag("Finish4"))
        {
            istouchfinish4 = true;
            SceneManager.LoadScene("Boss4");
        }




    }
    IEnumerator diePlayer(int delay)
    {
        ›sdeadPlayer.SetBool("›sdead", true);
        speed = 0;
        rb.velocity = Vector2.zero;
        rb.gravityScale = 0;

       
        isGameOver = true;

        GameOverAnim.gameObject.SetActive(true);
        GameOverAnim.Play("gameOverAnim");
           
       

        if (GameManager.instance != null)
        {
            GameManager.instance.ResetCoins(); 
            UpdateCoinText(); 
        }

        yield return new WaitForSeconds(delay);
        GameOverPanel.SetActive(true);
        Time.timeScale = 0;
    }


    public void UpdateCoinText()
    {
        if (CoinText != null && GameManager.instance != null)
        {
            CoinText.text = GameManager.instance.CoinCount.ToString();
        }
    }

    private  IEnumerator ResetColor(float delay)
    {
        yield return new WaitForSeconds(delay);
        playerRenderer.color = Color.white;
        ›scolor = false;

    }
}
