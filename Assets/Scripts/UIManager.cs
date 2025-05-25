using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public GameObject settings;

    public Button level2;
    public Button level3;
    public Button level4;
    public GameObject MarketMenu;
    private Market market;

    private void Start()
    {
        market=FindAnyObjectByType<Market>();
        level2.onClick.AddListener(ActiveLevels);
        level3.onClick.AddListener(ActiveLevels);
        level4.onClick.AddListener(ActiveLevels);
    }
    public void ActiveLevels()
    {
        if (Player.istouchfinish1 == true)
        {
            level2.interactable = true;
        }
        if (Player.istouchfinish2 == true)
        {
            level3.interactable = true;
        }
        if(Player.istouchfinish3 == true)
        {
            level4.interactable = true;
        }

    }
    private void Update()
    {
        ActiveLevels();
    }


    public void Level1()
    {
        Debug.Log("Butona týklandý");
        SceneManager.LoadScene("Level1");
    }
    public void Level2()
    {
        Debug.Log("Butona týklandý");
        SceneManager.LoadScene("Level2");
    }
    
    public void Level3()
    {
        Debug.Log("Butona týklandý");
        SceneManager.LoadScene("Level3");
    }
    public void Level4()
    {
        Debug.Log("Butona týklandý");
        SceneManager.LoadScene("Level4");
    }


    public void Settings()
    {
        settings.SetActive(true);
        Time.timeScale = 0;
    }


    public void ContinueButton()
    {
        settings.SetActive(false);
        Time.timeScale = 1;
    }


    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }


    public void MainMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
        
    }

    public void Market()
    {
        MarketMenu.SetActive(true);
        settings.SetActive(false);
    }

    public void MarketReturn()
    {
       
        MarketMenu.SetActive(false);
        Time.timeScale = 1;

        market.MarketReturn();
    }

   
  
}
