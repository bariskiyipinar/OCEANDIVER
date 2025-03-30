using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int CoinCount = 0;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Sahne deðiþse bile GameManager yok olmaz
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (!PlayerPrefs.HasKey("Coin"))
        {
            PlayerPrefs.SetInt("Coin", 0);
            PlayerPrefs.Save();
        }

        CoinCount = PlayerPrefs.GetInt("Coin", 0);
  
    }

    public void AddCoin(int amount)
    {
        CoinCount += amount;
        PlayerPrefs.SetInt("Coin", CoinCount);
        PlayerPrefs.Save();
    }

    public void ResetCoins()
    {
        CoinCount = 0;
        PlayerPrefs.SetInt("Coin", 0);
        PlayerPrefs.Save();
    }

    public void LoadLevel1()
    {
     
        SceneManager.LoadScene("Level1");
    }
    public void quitGame()
    {
       
        Application.Quit();
       
        
    }
}
