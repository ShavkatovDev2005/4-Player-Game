using UnityEngine;
using UnityEngine.UI;


public class bombadan_kac_other_script : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    public static int oyuncu_sec;
    float oyun_suresi;
    [SerializeField] GameObject[] activePlayers;
    [SerializeField] Image[] joysticks_image;
    [SerializeField] GameObject Players,NPC;
    void Start()
    {
        activePlayers = new GameObject[menu.secilen_oyuncu_sayisi];

        for (int i = 0; i < 4; i++)
        {
            if (i < menu.secilen_oyuncu_sayisi)
            {
                Players.transform.GetChild(i).gameObject.SetActive(true);//playerleri ac
                activePlayers[i]=Players.transform.GetChild(i).gameObject;
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
                activePlayers[i]=NPC.transform.GetChild(i).gameObject;
            }
            else {
                NPC.transform.GetChild(i).gameObject.SetActive(false);//NPCleri kapat
            }
        }
        
        audioSource.Play();

        oyuncu_sec=Random.Range(0,activePlayers.Length);
        select_Player();
    }
    void LateUpdate()
    {
        if (game_scripts.Instance.stopTime==true) return;

        oyun_suresi+=Time.deltaTime;
        if (oyun_suresi>=20)
        {
            for (int i = 0; i < activePlayers.Length; i++)
            {
                if (activePlayers[i]!=null)
                {
                    if (activePlayers[i].tag== "tagger")
                    {
                        game_scripts.Instance.kaybeden_oyuncu(i);//losed player
                        activePlayers[i].SetActive(false);
                        activePlayers[i] = null;
                    }
                }
            }

            for (int i = 0; i < 20; i++)
            {
                oyuncu_sec=Random.Range(0,activePlayers.Length);

                if (activePlayers[oyuncu_sec]!=null) break;
            }

            select_Player();

            game_scripts.oyuncuSayisi--;
            if (game_scripts.oyuncuSayisi<=1)
            {
                restart_game();
            }
            else oyun_suresi=0;
        }
    }
    void select_Player()
    {
        switch (oyuncu_sec)
        {
            case 0 : 
                activePlayers[0].tag = "tagger";
                break;
            case 1 :
                activePlayers[1].tag = "tagger";
                break;
            case 2 :
                activePlayers[2].tag = "tagger";
                break;
            default :
                activePlayers[3].tag = "tagger";
                break;
        }
    }
    public void restart_game()
    {
        game_scripts.Instance.RestartSceneOnClick(transform);
    }
    public void play_go_voice_and_stoptime()
    {
        game_scripts.Instance.play_go_voice_and_stoptime();
    }
}
