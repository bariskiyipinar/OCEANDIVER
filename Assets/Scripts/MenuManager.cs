using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    void Update()
    {
        // Fare týklamasý (masaüstü ve WebGL için)
        if (Input.GetMouseButtonDown(0))
        {
            LoadGame();
        }

        // Dokunma (mobil cihazlar için)
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                LoadGame();
            }
        }
    }

    void LoadGame()
    {
        SceneManager.LoadScene("LevelScene");
    }
}
