using UnityEngine;
using UnityEngine.UI;

public class Market : MonoBehaviour
{
    public GameObject SpeedÝtemPrefab;
    private EnvÝtems envitems;
    private GameObject[] env = new GameObject[3];
    public Text CoinCount;
    public Player player;
    private void Start()
    {
        envitems = FindObjectOfType<EnvÝtems>();
        player=FindAnyObjectByType<Player>();
        UpdateCoinText(); // Oyunun baþýnda coin miktarýný güncelle
    }

    public void SpeedItemButton()
    {
        if (GameManager.instance.CoinCount >= 20) // DÜZELTME: 20 veya daha fazlaysa satýn alma yapýlmalý
        {
            if (env[0] == null) // Eðer envanterde boþ yer varsa ekle
            {
                env[0] = SpeedÝtemPrefab;
                GameManager.instance.ResetCoins();
                player.UpdateCoinText();// Coin UI güncellensin
                UpdateCoinText();
            }
            else
            {
                Debug.Log("Envanter dolu!"); // Hata ayýklama için
            }
        }
        else
        {
            Debug.Log("Yeterli paranýz yok!"); // Kullanýcýya bilgilendirme
        }
    }

    public void MarketReturn()
    {
        if (env[0] == SpeedÝtemPrefab) // DÝREKT KONTROL
        {
            envitems.StartFastSwim(3);
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
