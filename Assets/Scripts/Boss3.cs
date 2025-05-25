using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Boss3 : MonoBehaviour
{
    public float BossMoveSpeed = 3f;
    public float MinY = -1.8f;
    public float MaxY = 3.5f;

    public float MinX = -11f;
    public float MaxX = -2.8639f;

    public float health = 2000f; 
    private float maxHealth = 2000f;
    public Image healthBar; 

    private float direction = 1f;

    public GameObject FishPrefab;
    public GameObject FishPrefab2;

    public float FishPower = 5f;
    public float FishSpawnInterval = 2f;

    public ParticleSystem FishDeath;
    private Rigidbody2D rb;

    void Start()
    {
        InvokeRepeating("FishEnemy", 1f, FishSpawnInterval);
        InvokeRepeating("FishEnemyTop", 10f, 10f);
        InvokeRepeating("FishEnemyCenter", 12f, 12f); 
        InvokeRepeating("BossMove", 15f, 15f);
      

        FishDeath.Stop();
        if (healthBar != null)
        {
            healthBar.fillAmount = 1f; 
        }
        rb = GetComponent<Rigidbody2D>();

    }

    void Update()
    {
        BossMovement();
     
    }

    public void FishEnemy()
    {
        if (FishPrefab != null)
        {
            GameObject newFish = Instantiate(FishPrefab, transform.position, Quaternion.identity);

            Rigidbody2D rb = newFish.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                rb.velocity = Vector2.left * FishPower;
                newFish.transform.localScale = new Vector3(3, 3, 3);
                newFish.transform.localRotation = Quaternion.Euler(0, 180, 0);
            }


        }
    }

    public void FishEnemyTop()
    {
        if (FishPrefab2 != null)
        {
            Vector2[] spawnPositions =
            {
            new Vector2(-7f, 3.6f),
            new Vector2(-7f, -2.80f)
        };

            foreach (Vector2 pos in spawnPositions)
            {
                GameObject newFish = Instantiate(FishPrefab2, pos, Quaternion.identity);
                Rigidbody2D rb = newFish.GetComponent<Rigidbody2D>();

                if (rb != null)
                {
                    rb.velocity = Vector2.left * FishPower;
                    newFish.transform.localScale = new Vector3(3, 3, 3);
                    newFish.transform.localRotation = Quaternion.Euler(0, 180, 0);
                }
            }
        }
    }
    public void FishEnemyCenter()
    {
        if (FishPrefab2 != null)
        {
            Vector2[] spawnPositions =
            {
             new Vector2(-7f,0.7550437f),
             new Vector2(-7f,-0.49f)
              
        };

            foreach (Vector2 pos in spawnPositions)
            {
                GameObject newFish = Instantiate(FishPrefab2, pos, Quaternion.identity);
                Rigidbody2D rb = newFish.GetComponent<Rigidbody2D>();

                if (rb != null)
                {
                    rb.velocity = Vector2.left * FishPower;
                    newFish.transform.localScale = new Vector3(3, 3, 3);
                    newFish.transform.localRotation = Quaternion.Euler(0, 180, 0);
                }
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            health -= 100f;
            Debug.Log("Boss Caný: " + health);

            if (healthBar != null)
            {
                float HealtBarScale = Mathf.Clamp01(health / maxHealth);
                healthBar.transform.localScale = new Vector3(HealtBarScale, healthBar.transform.localScale.y, healthBar.transform.localScale.z);
            }

            Destroy(collision.gameObject);

            if (health <= 0)
            {
                StartCoroutine(Die());
            }
        }
    }

    void BossMovement()
    {
        float newY = transform.position.y + (BossMoveSpeed * direction * Time.deltaTime);
        newY = Mathf.Clamp(newY, MinY, MaxY);
        transform.position = new Vector2(transform.position.x, newY);

        if (newY >= MaxY || newY <= MinY)
        {
            direction *= -1;
        }
    }

    public IEnumerator Die()
    {
        Debug.Log("Boss Öldü!");

        FishDeath.Play();

        yield return new WaitForSeconds(0.2f);

        DestroyEvent();

    }

    public void DestroyEvent()
    {
        Debug.Log("Boss yok edildi ve sahne deðiþtiriliyor...");
        Destroy(this.gameObject);
        SceneManager.LoadScene("LevelScene");
    }

    public void BossMove()
    {
        float newX = transform.position.x + direction * BossMoveSpeed * Time.deltaTime;

      
        if (newX >= MaxX || newX <= MinX)
        {
            direction *= -1;
        }

   
        Vector2 newPosition = new Vector2(newX, transform.position.y);
        rb.MovePosition(newPosition); 

    }

}
