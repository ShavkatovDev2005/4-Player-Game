using UnityEngine;
using UnityEngine.UI;

public class duel_other_setting : MonoBehaviour
{
    public static duel_other_setting Instance;
    AudioSource audioSource;
    public GameObject bulletPrefab; // Mermi prefab'ı. 
    [SerializeField] AudioClip bullet;//mermi ses
    [SerializeField] AudioClip bullet_full;//mermi ses
    [SerializeField] Image[] buttons_image;
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
        audioSource = GetComponent<AudioSource>();
    }
    public  void FireBullet(Transform player, bool A)//mermiler buradan verilir
    {
        if (game_scripts.Instance.stopTime==true) return;

        bullet_voice();
        GameObject bullet = Instantiate(bulletPrefab);
        bullet.GetComponent<duel_bullet>().duel_Other_Setting = this;
        bullet.transform.position= player.transform.GetChild(A ? 0:1).position;
        bullet.transform.rotation = player.transform.localRotation;
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        bulletRb.linearVelocity = bullet.transform.right * 10;  // bullet move speed
    }
    void bullet_voice()//tank ses cal
    {
        audioSource.PlayOneShot(bullet);
    }
    public void bullet_full_voice()//tank ses cal
    {
        audioSource.PlayOneShot(bullet_full);
    }
    
    public void restart_game()//oynama sirasini azalt
    {
        game_scripts.Instance.RestartSceneOnClick(transform);
    }
    public void play_go_voice_and_stoptime()
    {
        game_scripts.Instance.play_go_voice_and_stoptime();
    }
}
