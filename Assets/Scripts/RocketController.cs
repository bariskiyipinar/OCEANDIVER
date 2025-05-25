using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketController : MonoBehaviour
{
    public float RocketSpeed = 5f;
    private bool Ismoving = false;
    public Player player;

    void Update()
    {
        if (Ismoving)
        {
            transform.Translate(RocketSpeed*Vector2.left*Time.deltaTime,0);
        }
    }

    public void StartMoving()
    {
        Ismoving = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.CompareTag("Player"))
        {
            player.CharacterDamageSound.Play();
            player.health -= 20;
            player.BgSound.Play();
            float healthScale = Mathf.Clamp01(player.health / 100f);

            if (player.healthBar != null)
            {
                player.healthBar.transform.localScale = new Vector3(healthScale, player.healthBar.transform.localScale.y, player.healthBar.transform.localScale.z);
            }
        }
    }
}
