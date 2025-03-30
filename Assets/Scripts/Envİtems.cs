using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Envİtems : MonoBehaviour
{
   public  Player player;
    private void Start()
    {
        player = FindObjectOfType<Player>();
    }
    public IEnumerator FastSwim(int fastdelay)
    {
        player.isFast = true; // Hızlı yüzme başlasın

        int originalSpeed = player.speed;
        player.speed = 15; // 15 olarak hızlanma değerini belirledik
        player.playerPower = 10f;
        // Hızlanma süresi boyunca bekle
        yield return new WaitForSeconds(fastdelay);

        // Hızlandıktan sonra hız geri döner
        player.speed = originalSpeed;
        player .playerPower = 5f;
        player.isFast = false; // Hızlı yüzme bitti
    }
}
