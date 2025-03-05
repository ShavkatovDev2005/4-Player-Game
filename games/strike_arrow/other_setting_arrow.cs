using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.PlayerLoop;

public class other_setting_arrow : MonoBehaviour
{
    public static other_setting_arrow Instance;
    AudioSource audioSource;
    public GameObject bulletPrefab; // Mermi prefab'ı.
    public GameObject random_mermi;
    public GameObject random_can;
    [SerializeField] AudioClip bullet;//mermi ses
    float can_ver;
    private List<GameObject> mermiler = new List<GameObject>();  // Oluşturulan mermileri tutacak liste
    private float random_x, random_y;
    private float random_x_can, random_y_can;
    private int mermiHesap = 0;  // Mermi sayacı
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
        audioSource = GetComponent<AudioSource>();
    }
    void FixedUpdate()
    {
        if (game_scripts.Instance.stopTime==true) return;

        can_ver+=Time.deltaTime;
        if (can_ver>=7)
        {
            random_x_can = Random.Range(5f, -5f);
            random_y_can = Random.Range(43f, 36f);
            Instantiate(random_can, new Vector3(random_x_can, random_y_can, 90f), Quaternion.identity);
            can_ver=0;
        }
        
        // Mermi sayısı 4'ten küçükse yeni bir mermi oluştur
        if (mermiHesap < 4)
        {
            random_x = Random.Range(5f, -5f);
            random_y = Random.Range(43f, 36f);
            
            // Yeni bir mermi oluştur
            GameObject yeniMermi = Instantiate(random_mermi, new Vector3(random_x, random_y, 90f), Quaternion.identity);
            
            // Yeni mermiyi listeye ekle
            mermiler.Add(yeniMermi);
            
            // Mermi sayacını artır
            mermiHesap++;
        }

        // Listeyi kontrol et, silinmiş objeleri temizle
        for (int i = mermiler.Count - 1; i >= 0; i--)
        {
            if (mermiler[i] == null)  // Objeler yok oldugunda
            {
                mermiler.RemoveAt(i);  // Listeden çıkar
                mermiHesap--;  // Mermi sayısını azalt
            }
        }
    }

    public void FireBullet(Transform player)//mermiler buradan verilir
    {
        bullet_voice();
        Vector3 firePosition = player.position - player.up * 0.4f + player.right * 0.5f; // Karakterin önüne doğru kaydırma
        GameObject bullet = Instantiate(bulletPrefab, firePosition, player.transform.rotation);
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        bulletRb.velocity = player.transform.right * 10f;  // Merminin hareket hızı
    }
    void bullet_voice()//tank ses cal
    {
        audioSource.PlayOneShot(bullet);
    }
    public void restart_game()//oynama sirasini azalt
    {
        game_scripts.Instance.RestartSceneOnClick(transform);
    }
}
