using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEditor;
using UnityEngine.UI;


public class other_setting_tank : MonoBehaviour
{
    public static other_setting_tank Instance;
    public static int arena;//chosen arena
    public Sprite[] _tanklar_sprite; // Tankların sprite dizisi
    public GameObject bulletPrefab;
    [SerializeField] GameObject[] _arena;// arena
    [SerializeField] AudioClip bullet;//bullet voice
    [SerializeField] AudioClip bullet_touched;//bullet voice
    [SerializeField] Image[] buttons_image;
    private AudioSource audioSource;
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
        
        if (arena >= 0 && arena < _arena.Length)//will be elected arena
        {
            for (int i = 0; i < _arena.Length; i++)
            {
                _arena[i].SetActive(false);
            }
            _arena[arena].SetActive(true);
        }
        
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        if (game_scripts.Instance.stopTime==true) return;

        if (game_scripts.oyuncuSayisi<=1)//oyunu tekrarlama
        {
            game_scripts.Instance.RestartSceneOnClick(transform);
        }
    }
    public void FireBullet(Transform tank)//tanklara mermiler buradan verilir
    {
        bullet_voice();
        Vector3 firePosition = tank.transform.position + tank.transform.up * 0.6f; // Karakterin önüne doğru kaydırma
        GameObject bullet = Instantiate(bulletPrefab, firePosition, tank.transform.rotation);
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        bulletRb.velocity = tank.transform.transform.up * 7f;  // Merminin hareket hızı
    }
    public void bullet_voice()//tank ses cal
    {
        audioSource.PlayOneShot(bullet);
    }
    public void bullet_touched_voice()//tank ses cal
    {
        audioSource.PlayOneShot(bullet_touched);
    }
}