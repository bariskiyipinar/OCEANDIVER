using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Camera : MonoBehaviour
{
    public Transform Character;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    public float followThreshold = 5f;

    public Slider progressSlider;
    public Transform levelEndPoint;
    public Image fillImage;
  

    void Update()
    {

        float distance = Vector3.Distance(transform.position, Character.position);


        if (distance > followThreshold)
        {

            Vector3 targetPosition = new Vector3(Character.position.x, transform.position.y, transform.position.z) + offset;
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);
        }

        UpdateSliderProgress();
    }
    void UpdateSliderProgress()
    {
        // Karakterin x pozisyonu ile bitiþ noktasýnýn x pozisyonu arasýndaki oraný hesapla
        // Bu oran, 0 ile 1 arasýnda bir deðer olacak, böylece slider ve resim ilerleyecek.
        float progress = Mathf.InverseLerp(0, levelEndPoint.position.x, Character.position.x);

      
        progressSlider.value = progress;

     
        if (fillImage != null)
        {
            fillImage.fillAmount = progress; 
            
        }
       
    }


    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;

    }

}
