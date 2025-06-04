using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class coin_other_script : MonoBehaviour
{
    public static coin_other_script  Instance;
    [SerializeField] TextMeshProUGUI saniye_txt;
    [SerializeField] Image[] buttons_image;
    public int count1,count2,count3,count4;
    float oyun_suresi;
    int intValue;
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
        oyun_suresi=60;
    }
    void FixedUpdate()
    {
        if (game_scripts.Instance.stopTime==true) return;

        
        if (oyun_suresi<=0)
        {
            // Maksimal qiymatni topish
            int maxCount = Mathf.Max(count1, count2, count3, count4);

            // Maksimal qiymatga ega o'yinchilarni aniqlash
            if (count1 == maxCount) game_scripts.Instance.kazanan_oyuncu(0);
            else if (count2 == maxCount) game_scripts.Instance.kazanan_oyuncu(1);
            else if (count3 == maxCount) game_scripts.Instance.kazanan_oyuncu(2);
            else if (count4 == maxCount) game_scripts.Instance.kazanan_oyuncu(3);
        

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
    public void play_go_voice_and_stoptime()
    {
        game_scripts.Instance.play_go_voice_and_stoptime();
    }
}