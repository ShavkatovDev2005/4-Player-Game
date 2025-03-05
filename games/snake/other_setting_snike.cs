using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class other_setting_snike : MonoBehaviour
{
    public static other_setting_snike Instance;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip destroy;
    [SerializeField] AudioClip take;
    [SerializeField] Image[] joysticks_image;
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
    }
    void FixedUpdate()
    {
        if (game_scripts.Instance?.stopTime==true) return;

        if (game_scripts.oyuncuSayisi<=1)
        {
            game_scripts.Instance.RestartSceneOnClick(transform);
        }
    }
    public void destroy_audio()
    {
        audioSource.PlayOneShot(destroy);
    }
    public void take_audio()
    {
        audioSource.PlayOneShot(take);
    }
}
