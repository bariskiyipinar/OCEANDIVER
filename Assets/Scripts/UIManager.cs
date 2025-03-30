using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public GameObject settings;
    public GameObject Continuebutton;
    public GameObject restart;
    public GameObject Mainmenu;

   

    public Button level2;

  
   
    private void Update()
    {
       if( Player.istouchfinish1 ==true)
        {
            level2.interactable = true;
        }
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
}
