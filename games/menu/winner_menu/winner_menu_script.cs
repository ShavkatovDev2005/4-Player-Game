using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using System;
using System.Linq;
using TMPro;
using UnityEngine.Windows;


public class winner_menu_script : MonoBehaviour
{
    [SerializeField] GameObject button_object;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip[] win;
    [SerializeField] AudioClip[] lose;
    [SerializeField] TMP_Text[] players_count_txt;
    [SerializeField] TMP_Text[] players_text;
    public static int[] players_count = new int[4];
    static bool ads_izlensinmi;
    public int oyuncular_sayisi;
    void Awake()
    {
        button_object.SetActive(false);
        for (int i = 0; i < 4; i++)
        {
            players_text[i].text = PlayerPrefs.GetString("user_name"+(i+1),"Player"+(i+1));//username dogurlama       --PlayerPrefs.GetString("user_name1","Player1");
            players_text[i].color = game_scripts.Instance.player_materials[i].color;//username color degistirme
        }
    }
    void Start()
    {
        if (ads_izlensinmi) adsScript.Ads?.ShowInterstitialAd();//reklamni yuklash
        ads_izlensinmi=!ads_izlensinmi;

        StartCoroutine(sayilari_ver(oyuncular_sayisi));
    }

    IEnumerator sayilari_ver(int oyuncular_sayisi)
    {
        int[] players = oyuncular_sayisi.ToString("D4").Select(c => c - '0').ToArray();
        int[] index = {3,2,1,0};
        Array.Sort(players, index);  // Ballar va indekslarni tartiblaymiz
        Array.Reverse(players); 
        Array.Reverse(index);
        
        for (int i = 0; i < 4; i++)
        {
            players_count_txt[index[i]].text=players_count[index[i]].ToString();
        }

        for (int i = 0; i < 4; i++)
        {
            if (menu.secilen_oyuncu_sayisi_botlarla > index[i])
            {
                int I = index[i];
                transform.GetChild(0).GetChild(index[i]).gameObject.SetActive(true);//playerleri setactive true yapma
                Animator animator = transform.GetChild(0).GetChild(index[i]).GetComponent<Animator>();
                if (players[i]==0)
                {
                    int A = UnityEngine.Random.Range(1,4);//animation count
                    animator.Play("lose_animation"+A);

                    A = UnityEngine.Random.Range(0,lose.Length);//voice
                    audioSource.PlayOneShot(lose[A]);
                }
                else
                {
                    int A = UnityEngine.Random.Range(1,5);//animation count
                    animator.Play("win_animation"+A);

                    A = UnityEngine.Random.Range(0,win.Length);//voice
                    audioSource.PlayOneShot(win[A]);
                }
                players_count[I]+=players[i];
                players_count_txt[I].text=players_count[I].ToString();
                yield return new WaitForSeconds(3);//3saniye bekle
            }
        }
        if (game_scripts.kacKezOynariz<0) 
        {
            button_object.SetActive(true);
            players_count = new int[4];
        }
        else
        {
            if (game_scripts.kacKezOynariz<=0) menu.parayi_ver = true;
            
            Destroy(gameObject);
            game_scripts.Instance.load();//continiu game
        }
    }
    public void button()
    {
        Destroy(gameObject);
        game_scripts.Instance.home_button();
    }
}
