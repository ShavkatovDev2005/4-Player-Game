using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class other_script_topOyunu : MonoBehaviour
{
    // 1. Delegate aniqlash
    public delegate void MyEventHandler();

    // 2. Event e'lon qilish
    public static event MyEventHandler myEvent;    
    public GameObject bulletPrefab;
    [SerializeField] GameObject[] kenar;
    private int kenar_secim;
    private float saniye;
    [SerializeField] Image[] buttons_image;
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
    }
    
    void Update()
    {
        if (game_scripts.Instance.stopTime==true) return;

        if (game_scripts.oyuncuSayisi<=1) restart_game();

        saniye+=Time.deltaTime;
        if (saniye>=3)
        {
            saniye=0f;
            FireBullet();
        }
    }
    public static void eventCagir()//bu even top bulletda chaqirilgan
    {
        if (myEvent != null)
        {
            myEvent(); // Event chaqirilyapti
        }
    }
    public void restart_game()//oynama sirasini azalt
    {
        game_scripts.Instance.RestartSceneOnClick(transform);
    }
    void FireBullet()
    {   
        kenar_secim=Random.Range(0,4);
        GameObject bullet = Instantiate(bulletPrefab, kenar[kenar_secim].transform);
        bullet.transform.localPosition =bullet.transform.localPosition + new Vector3(0, 12, 0);
        bullet.transform.rotation =kenar[kenar_secim].transform.rotation;
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        bulletRb.velocity = bullet.transform.up * 3f;  // Merminin hareket hızı
    }
}