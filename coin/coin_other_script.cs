using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using System;
using UnityEngine.UI;

public class coin_other_script : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI saniye_txt;
    [SerializeField] Image[] buttons_image;
    float oyun_suresi;
    int intValue;
    [SerializeField] GameObject Players,NPC;
    void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            if (i < menu.secilen_oyuncu_sayisi)
            {
                Players.transform.GetChild(i).gameObject.SetActive(true);//playerleri ac
                buttons_image[i].color = game_scripts.Instance.player_materials[i].color;//button renklerini degistir
            }
            else
            {
                Players.transform.GetChild(i).gameObject.SetActive(false);//playerleri kapat
                buttons_image[i].gameObject.SetActive(false);//bot var olan joystickleri kapat
            }

            if (i >= menu.secilen_oyuncu_sayisi && menu.NPC_olsun) 
            {
                NPC.transform.GetChild(i).gameObject.SetActive(true);//NPCleri ac
            }
            else {
                NPC.transform.GetChild(i).gameObject.SetActive(false);//NPCleri kapat
            }
        }
        oyun_suresi=60;
    }
    void FixedUpdate()
    {
        if (game_scripts.Instance.stopTime==true) return;

        
        if (oyun_suresi<=0)
        {
            // Har bir player's count qiymatini olish
            int player1Count = player1_coin.Instance.count;
            int player2Count = player2_coin.Instance.count;
            int player3Count = player3_coin.Instance.count;
            int player4Count = player4_coin.Instance.count;

            // Maksimal qiymatni topish
            int maxCount = Mathf.Max(player1Count, player2Count, player3Count, player4Count);

            // Maksimal qiymatga ega o'yinchilarni aniqlash
            if (player1Count == maxCount) game_scripts.Instance.kazanan_oyuncu(0);
            if (player2Count == maxCount) game_scripts.Instance.kazanan_oyuncu(1);
            if (player3Count == maxCount) game_scripts.Instance.kazanan_oyuncu(2);
            if (player4Count == maxCount) game_scripts.Instance.kazanan_oyuncu(3);
        

            game_scripts.Instance.RestartSceneOnClick(transform);//restart the game
            oyun_suresi=60;
        }
        else 
        {
            oyun_suresi-= Time.deltaTime;
            intValue = Mathf.FloorToInt(oyun_suresi); 
            saniye_txt.text = intValue.ToString();
        }
    }
}
