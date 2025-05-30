using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBoss : MonoBehaviour
{
    private Rigidbody2D rb;
    private float playerPower = 6f; 

    public float health = 100f;
    public Image healthBar;

    public GameObject Bullet;
    private float bulletPower = 10f;
    public Transform BulletPoint;

    public Animator GameOverAnim;
    public Animator ÝsdeadPlayer;
    public GameObject GameOverPanel;

    public AudioSource Damage;
    private GameObject Backgroundsound;
    private bool canShoot = true;

    private bool isJumping = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Backgroundsound = GameObject.FindGameObjectWithTag("Sound");
    }

    void Update()
    {
        PlayerMovement();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Fish"))
        {
            TakeDamage(10f);
        }
        else if (collision.CompareTag("Fish2"))
        {
            TakeDamage(15f);
        }
    }

    void TakeDamage(float damage)
    {
        health -= damage;
        if (Damage != null) Damage.Play();

        float healthScale = Mathf.Clamp01(health / 100f);
        if (healthBar != null)
        {
            healthBar.transform.localScale = new Vector3(healthScale, healthBar.transform.localScale.y, healthBar.transform.localScale.z);
        }

        if (health <= 0)
        {
            if (Backgroundsound != null)
                Backgroundsound.GetComponent<AudioSource>().Stop();

            StartCoroutine(diePlayer(3));
        }

        Debug.Log("Can Azaldý! Yeni Can: " + health);
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
        if (!isJumping)
        {
            bool jumpInput = false;

            // Mobil dokunmatik kontrolü
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    jumpInput = true;
                }
            }

            // PC ve WebGL mouse týklama kontrolü
            if (Input.GetMouseButtonDown(0))
            {
                jumpInput = true;
            }

            if (jumpInput)
            {
                rb.gravityScale = 1; // Yerçekimi açýk kalsýn
                rb.velocity = new Vector2(rb.velocity.x, 0); // Dikey hýzý sýfýrla
                rb.AddForce(Vector2.up * playerPower, ForceMode2D.Impulse); // Yumuþak zýplama kuvveti
                isJumping = true;
            }
        }
        else
        {
            if (rb.velocity.y <= 0)
            {
                isJumping = false;
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
        if (canShoot)
        {
            BulletAttack();
            canShoot = false;
            Invoke("ResetcanShoot", 2f);
        }
    }

    private void ResetcanShoot()
    {
        canShoot = true;
    }
}
