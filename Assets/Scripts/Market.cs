using UnityEngine;
using UnityEngine.UI;

public class Market : MonoBehaviour
{
    public GameObject Speed›temPrefab; 
    public GameObject Health›temPrefab;
    private Env›tems envitems; 
    private GameObject[] env = new GameObject[3]; 
    public Text CoinCount; 
    public Player player; 
    public Text Health;

   



    private void Start()
    {
        envitems = FindObjectOfType<Env›tems>();
        player = FindAnyObjectByType<Player>();
        UpdateCoinText();


    }
    private void Update()
    {
        UpdateCoinText();
        UpdateHealth();
    }
    public void SpeedItemButton()
    {
     
        if (GameManager.instance.CoinCount >= 20)
        {
            for (int i = 0; i < env.Length; i++)
            {
                if (env[i] == null)
                {
                    env[0] = Speed›temPrefab;
                    GameManager.instance.CoinCount -= 20;
                    player.UpdateCoinText();
                    Debug.Log("Speed item sat˝n al˝nd˝!");
                    return;
                }
            }

            Debug.Log("Envanter dolu!");
        }
        else
        {
            Debug.Log("Yeterli paran˝z yok!");
        }
    }
    public void HealthItemButton()
    {
        if(player.health < 100) {
        if (GameManager.instance.CoinCount >= 25)
        {
              
            for (int i = 0; i < env.Length; i++)
            {
                if (env[i] == null)
                {
                    env[1] = Health›temPrefab;
                    GameManager.instance.CoinCount -= 25;
                    player.UpdateCoinText();
                    Debug.Log("Health item sat˝n al˝nd˝!");
                    return;
                }
            }

            Debug.Log("Envanter dolu!");
        }
        else
        {
            Debug.Log("Yeterli paran˝z yok!");
        }
        }
    }

    public void BulletItemButton()
    {
       
            if (GameManager.instance.CoinCount >= 30)
            {

                for (int i = 0; i < env.Length; i++)
                {
                    if (env[i] == null)
                    {
                        env[2] = envitems.Bullet;
                        GameManager.instance.CoinCount -= 30;
                        player.UpdateCoinText();
                        Debug.Log("Health item sat˝n al˝nd˝!");
                        return;
                    }
                }

                Debug.Log("Envanter dolu!");
            }
            else
            {
                Debug.Log("Yeterli paran˝z yok!");
            }
        
    }


    public void MarketReturn()
    {

            if (env[0] == Speed›temPrefab) 
            {
                envitems.StartFastSwim(3);
                env[0] = null;
            }
            if (env[1] == Health›temPrefab)
            {
                envitems.HealthItem(); 
                env[1] = null;
            }
            if (env[2] == envitems.Bullet)
            {
                envitems.StartShooting(10f);
                env[2] = null;
            }

    }

    public void UpdateCoinText()
    {
        if (CoinCount != null && GameManager.instance != null)
        {
            CoinCount.text = GameManager.instance.CoinCount.ToString();
        }
    }

    public void UpdateHealth()
    {
        if ((Health !=null && player.health !=null))
        {
            Health.text=player.health.ToString();
        }
    }
}
