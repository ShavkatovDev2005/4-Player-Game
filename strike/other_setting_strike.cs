using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Unity.VisualScripting;


public class other_setting_strike : MonoBehaviour
{
    public static other_setting_strike Instance;
    public GameObject bulletPrefab; // Mermi prefab'ı
    [SerializeField] Image[] buttons_image;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip audioClip; 
    [SerializeField] AudioClip kaybetme_sound;
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
    }
    void FixedUpdate()
    {
        if (game_scripts.Instance?.stopTime==true) return;

        if (game_scripts.oyuncuSayisi<=1)
        {
            game_scripts.Instance.RestartSceneOnClick(transform);
        }
    }
    public void FireBullet(Transform target)//mermi buradan alinir
    {   
        audioSource.PlayOneShot(audioClip);
        Vector3 firePosition = target.transform.position + target.transform.up * 1.5f; // Karakterin önüne doğru kaydırma
        GameObject bullet = Instantiate(bulletPrefab, firePosition, target.transform.rotation);
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        bulletRb.velocity = target.transform.transform.up * 4f;  // Merminin hareket hızı
    }
    public void kaybetme()
    {
        audioSource.PlayOneShot(kaybetme_sound);
    }
}
