using UnityEngine;
using UnityEngine.UI;

public class Market : MonoBehaviour
{
    public GameObject SpeedÝtemPrefab; // Speed item prefab'ý
    private EnvÝtems envitems; // Çevre öðeleri
    private GameObject[] env = new GameObject[3]; // Envanterdeki öðeler
    public Text CoinCount; // Coin UI Text
    public Player player; // Player referansý



    private void Start()
    {
        envitems = FindObjectOfType<EnvÝtems>();
        player = FindAnyObjectByType<Player>();
        UpdateCoinText(); // Oyunun baþýnda coin miktarýný güncelle
    }
    private void Update()
    {
        UpdateCoinText();
    }
    public void SpeedItemButton()
    {
     
        if (GameManager.instance.CoinCount >= 20)
        {
            for (int i = 0; i < env.Length; i++)
            {
                if (env[i] == null)
                {
                    env[0] = SpeedÝtemPrefab;
                    GameManager.instance.CoinCount -= 20;
                    player.UpdateCoinText();
                    Debug.Log("Speed item satýn alýndý!");
                    return;
                }
            }

            Debug.Log("Envanter dolu!");
        }
        else
        {
            Debug.Log("Yeterli paranýz yok!");
        }
    }
   


    public void MarketReturn()
    {

            if (env[0] == SpeedÝtemPrefab) // DÝREKT KONTROL
            {
                envitems.StartFastSwim(3);
                env[0] = null;
            }
     
    }

    public void UpdateCoinText()
    {
        if (CoinCount != null && GameManager.instance != null)
        {
            CoinCount.text = GameManager.instance.CoinCount.ToString();
        }
    }
}
