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
        
            if (SceneManager.GetActiveScene().name == "LevelScene")
            {
                level2.onClick.AddListener(ActiveLevels);
                level3.onClick.AddListener(ActiveLevels);
                level4.onClick.AddListener(ActiveLevels);
            }

            market = FindAnyObjectByType<Market>();
       

    }
  public void ActiveLevels()
{
    if (level2 != null && Player.istouchfinish1)
        level2.interactable = true;

    if (level3 != null && Player.istouchfinish2)
        level3.interactable = true;

    if (level4 != null && Player.istouchfinish3)
        level4.interactable = true;
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
