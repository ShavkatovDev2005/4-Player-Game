using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;


public class bombadan_kac_other_script : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    public static int oyuncu_sec;
    float oyun_suresi;
    [SerializeField] GameObject[] karakter;
    [SerializeField] Image[] joysticks_image;
    int a;
    List<int> players = new List<int>{1,2,3,4};
    int Z;
    [SerializeField] GameObject Players,NPC;
    void Awake()
    {
        oyuncu_sec=Random.Range(1,5);
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
        audioSource.Play();
    }
    void LateUpdate()
    {
        if (game_scripts.Instance.stopTime==true) return;

        oyun_suresi+=Time.deltaTime;
        if (oyun_suresi>=20)
        {
            if (bomba_karakter1.bomba_bende && karakter[0].gameObject!=null)
            {
                Destroy(karakter[0]);
                game_scripts.Instance.kaybeden_oyuncu(0);
                Z = 1;
            }
            else if (bomba_karakter2.bomba_bende && karakter[1].gameObject!=null)
            {
                Destroy(karakter[1]);
                game_scripts.Instance.kaybeden_oyuncu(1);
                Z = 2;
            }
            else if (bomba_karakter3.bomba_bende && karakter[2].gameObject!=null)
            {
                Destroy(karakter[2]);
                game_scripts.Instance.kaybeden_oyuncu(2);
                Z = 3;
            }
            else if (bomba_karakter4.bomba_bende && karakter[3].gameObject!=null)
            {
                Destroy(karakter[3]);
                game_scripts.Instance.kaybeden_oyuncu(3);
                Z = 4;
            }
            for (int i = 0; i < players.Count; i++)//secilen playeri silme
            {
                if (players[i]==Z)
                    players.RemoveAt(i);
            }
            

            oyun_suresi = 0;
            game_scripts.oyuncuSayisi--;
            if (game_scripts.oyuncuSayisi<=1)
            {
                restart_game();
            }

            int randomIndex = Random.Range(0, players.Count);
            karakter[players[randomIndex]-1].transform.GetChild(0).gameObject.SetActive(true);

            if (players[randomIndex]==1) bomba_karakter1.bomba_bende=true;
            else if (players[randomIndex]==2) bomba_karakter2.bomba_bende=true;
            else if (players[randomIndex]==3) bomba_karakter3.bomba_bende=true;
            else if (players[randomIndex]==4) bomba_karakter4.bomba_bende=true;
        }
    }
    public void restart_game()//oynama sirasini azalt
    {
        audioSource.Stop();
        game_scripts.Instance.RestartSceneOnClick(transform);
    }
}
