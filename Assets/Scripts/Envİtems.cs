using System.Collections;
using UnityEngine;

public class Envİtems : MonoBehaviour
{
    public Player player;
    public ParticleSystem speedEffect;
    public GameObject Bullet;
    public Transform BulletPoint;
    private float bulletPower = 10f;
    private bool canShoot = false;

    private void Start()
    {
        player = FindObjectOfType<Player>();
    }

    public IEnumerator FastSwim(int fastdelay)
    {
        player.isFast = true; 
        speedEffect.gameObject.SetActive(true);
        speedEffect.Play();
        int originalSpeed = player.speed;
        player.speed = 15; 
        player.playerPower = 10f;

        yield return new WaitForSeconds(fastdelay); 

        
        player.speed = originalSpeed;
        player.playerPower = 5f;
        player.isFast = false; 
        speedEffect.gameObject.SetActive(false);
    }

    public void StartFastSwim(int fastdelay)
    {
        StartCoroutine(FastSwim(fastdelay));
    }

    public int HealthItem()
    {
        player.health = Mathf.Min(player.health + 20, 100);

        float healthScale = Mathf.Clamp01(player.health / 100f);

        if (player.healthBar != null)
        {
           player. healthBar.transform.localScale = new Vector3(healthScale, player.healthBar.transform.localScale.y, player.healthBar.transform.localScale.z);
        }

        return player.health;
    }

    public void StartShooting(float duration)
    {
        StartCoroutine(ShootingDuration(duration));
    }

    private IEnumerator ShootingDuration(float duration)
    {
        canShoot = true;
        float timer = 0f;
        float fireRate = 0.5f; 

        while (timer < duration)
        {
            TryShoot();
            yield return new WaitForSeconds(fireRate);
            timer += fireRate;
        }

        canShoot = false;
    }


    public void TryShoot()
    {
        if (!canShoot) return;

        GameObject bullet = Instantiate(Bullet, BulletPoint.position, Quaternion.identity);
        Rigidbody2D rbBullet = bullet.GetComponent<Rigidbody2D>();
        if (rbBullet != null)
        {
            rbBullet.velocity = Vector2.right * bulletPower;
        }
        Destroy(bullet, 2f);
    }
}
