using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class monster_other_script : MonoBehaviour
{
    public static monster_other_script Instance;
    public TextMeshProUGUI saniye;
    [SerializeField] GameObject finish;
    [SerializeField] Image[] joysticks_image;
    float saniye_;
    [SerializeField] GameObject Players,NPC;
    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            if (i < menu.secilen_oyuncu_sayisi)
            {
                Players.transform.GetChild(i).gameObject.SetActive(true);//playerleri ac
                joysticks_image[i].color = game_scripts.Instance.player_materials[i].color;//button renklerini degistir
            }
            else
            {
                Players.transform.GetChild(i).gameObject.SetActive(false);//playerleri kapat
                joysticks_image[i].transform.parent.gameObject.SetActive(false);//bot var olan joystickleri kapat
            }

            if (i >= menu.secilen_oyuncu_sayisi && menu.NPC_olsun) 
            {
                NPC.transform.GetChild(i).gameObject.SetActive(true);//NPCleri ac
            }
            else {
                NPC.transform.GetChild(i).gameObject.SetActive(false);//NPCleri kapat
            }
        }
        saniye_=60;
    }
    void FixedUpdate()
    {
        if (game_scripts.Instance.stopTime==true) return;

        saniye_-=Time.deltaTime;

        if (saniye_<=3f) finish.transform.position+=new Vector3(-0.06f,0,0);//finish cizgisiyi yaklastirma

        if (saniye_>=0) saniye.text=Mathf.FloorToInt(saniye_).ToString();//saniyeyi yazdirma

        if (game_scripts.oyuncuSayisi<=1) restart_game();//oyunu bitirme
    }
    public void restart_game()//oynama sirasini azalt
    {
        game_scripts.Instance.RestartSceneOnClick(transform);
    }
}
