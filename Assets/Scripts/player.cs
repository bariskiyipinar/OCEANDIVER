using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    public float playerPower = 5f;
    public int speed = 3;
    public GameObject healthBar;

    public Text CoinText;
    public int health = 100;
    public Animator ÝsdeadPlayer;
    public GameObject GameOverPanel;

    private bool isGameOver = false;
    public AudioSource CharacterDamageSound;
    public Animator GameOverAnim;
    public AudioSource BgSound;
    private AudioSource CoinSound;

    private bool isTouching = false;
    public bool isFast = false;

    public static bool istouchfinish1, istouchfinish2, istouchfinish3, istouchfinish4;
    private SpriteRenderer playerRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        CoinSound = GetComponent<AudioSource>();
        GameOverAnim.gameObject.SetActive(false);
        playerRenderer = GetComponent<SpriteRenderer>();

        if (GameManager.instance != null)
        {
            UpdateCoinText();
        }

        Vector3 viewPos = new Vector3(0.2f, 0.5f, 0f);
        transform.position = UnityEngine.Camera.main.ViewportToWorldPoint(viewPos);
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
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
        bool inputStarted = false;
        bool inputEnded = false;

        // Mouse input (PC/WebGL)
        if (Input.GetMouseButtonDown(0))
        {
            inputStarted = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            inputEnded = true;
        }

        // Touch input (Mobil)
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                inputStarted = true;
            }
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                inputEnded = true;
            }
        }

        if (inputStarted && !isTouching)
        {
            isTouching = true;
        
            rb.AddForce(Vector2.up * playerPower, ForceMode2D.Impulse);
        }

        if (inputEnded)
        {
            isTouching = false;
        }


        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.CompareTag("Fish") ||
            collision.CompareTag("Fish2") ||
            collision.CompareTag("Fish3") ||
            collision.CompareTag("Fish4")) && !isFast)
        {
            int damage = 0;
            switch (collision.tag)
            {
                case "Fish": damage = 5; break;
                case "Fish2": damage = 7; break;
                case "Fish3": damage = 9; break;
                case "Fish4": damage = 10; break;
            }

            TakeDamage(damage);
        }
        else if (collision.CompareTag("Coin"))
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
        else if (collision.CompareTag("Finish1"))
        {
            istouchfinish1 = true;
            SceneManager.LoadScene("Boss1");
        }
        else if (collision.CompareTag("Finish2"))
        {
            istouchfinish2 = true;
            SceneManager.LoadScene("Boss2");
        }
        else if (collision.CompareTag("Finish3"))
        {
            istouchfinish3 = true;
            SceneManager.LoadScene("Boss3");
        }
        else if (collision.CompareTag("Finish4"))
        {
            istouchfinish4 = true;
            SceneManager.LoadScene("Boss4");
        }
    }

    void TakeDamage(int damage)
    {
        health -= damage;
        playerRenderer.color = Color.red;
        StartCoroutine(ResetColor(0.3f));
        if (CharacterDamageSound != null)
            CharacterDamageSound.Play();

        float healthScale = Mathf.Clamp01(health / 100f);
        if (healthBar != null)
        {
            healthBar.transform.localScale = new Vector3(healthScale, healthBar.transform.localScale.y, healthBar.transform.localScale.z);
        }

        if (health <= 0)
        {
            StartCoroutine(diePlayer(3));
            if (BgSound != null)
                BgSound.Stop();
        }
        else
        {
            ÝsdeadPlayer.SetBool("Ýsdead", false);
        }
    }

    IEnumerator diePlayer(int delay)
    {
        ÝsdeadPlayer.SetBool("Ýsdead", true);
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

        float timer = 0f;
        while (timer < delay)
        {
            timer += Time.unscaledDeltaTime;
            yield return null;
        }

        GameOverPanel.SetActive(true);
    }

    IEnumerator ResetColor(float delay)
    {
        yield return new WaitForSeconds(delay);
        playerRenderer.color = Color.white;
    }

    public void UpdateCoinText()
    {
        if (CoinText != null && GameManager.instance != null)
        {
            CoinText.text = GameManager.instance.CoinCount.ToString();
        }
    }
}
