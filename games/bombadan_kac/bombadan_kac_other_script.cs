using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


public class bombadan_kac_other_script : MonoBehaviour
{
    public static bombadan_kac_other_script bombadan_Kac_Other_Script;
    [SerializeField] AudioSource audioSource;
    public static int oyuncu_sec;
    float oyun_suresi;
    public List<GameObject> activeList = new List<GameObject>();
    [SerializeField] Image[] joysticks_image;
    [SerializeField] GameObject Players,NPC;
    void Awake()
    {
        bombadan_Kac_Other_Script = this;
    }
    void Start()
    {
        activeList.Clear();

        for (int i = 0; i < 4; i++)
        {
            if (i < menu.secilen_oyuncu_sayisi)
            {
                Players.transform.GetChild(i).gameObject.SetActive(true);//playerleri ac
                activeList.Add(Players.transform.GetChild(i).gameObject);
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
                activeList.Add(NPC.transform.GetChild(i).gameObject);
            }
            else {
                NPC.transform.GetChild(i).gameObject.SetActive(false);//NPCleri kapat
            }
        }
        
        audioSource.Play();

        select_Player();
    }
    void LateUpdate()
    {
        if (game_scripts.Instance.stopTime==true) return;

        oyun_suresi+=Time.deltaTime;
        if (oyun_suresi>=20)
        {
            for (int i = 0; i < activeList.Count; i++)//release losed player
            {
                if (activeList[i].gameObject.activeSelf && activeList[i].gameObject.layer == LayerMask.NameToLayer("tagger"))
                {
                    activeList[i].gameObject.layer = LayerMask.NameToLayer(activeList[i].gameObject.tag); // tag==layername
                    game_scripts.Instance.kaybeden_oyuncu(i);
                    activeList[i].gameObject.SetActive(false);
                }
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
        for (int i = 0; i < 50; i++)
        {
            oyuncu_sec = Random.Range(0,activeList.Count);

            if (activeList[oyuncu_sec].activeSelf) break;
        }

        switch (oyuncu_sec)
        {
            case 0 : 
                activeList[0].gameObject.layer = LayerMask.NameToLayer("tagger");
                break;
            case 1 :
                activeList[1].gameObject.layer = LayerMask.NameToLayer("tagger");
                break;
            case 2 :
                activeList[2].gameObject.layer = LayerMask.NameToLayer("tagger");
                break;
            default :
                activeList[3].gameObject.layer = LayerMask.NameToLayer("tagger");
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
