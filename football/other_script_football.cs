using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.Mathematics;

public class other_script_football : MonoBehaviour
{
    public static other_script_football Instance;
    AudioSource audioSource;
    float saniye;
    public int jamoa1,jamoa2;
    public float waitingTime;
    [SerializeField] TextMeshProUGUI saniye_txt,jamoa2_txt,jamoa1_txt;
    [SerializeField] AudioClip goal_voice,hit_voice;
    [SerializeField] Image[] joysticks_image;
    [SerializeField] GameObject Players,NPC;
    void Awake()
    {
        jamoa1=0;
        jamoa2=0;
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
        saniye=60;
        audioSource = GetComponent<AudioSource>();
    }
    void FixedUpdate()
    {
        if (game_scripts.Instance.stopTime==true) return;

        if (waitingTime<2f) waitingTime+=Time.deltaTime;

        saniye-=Time.deltaTime;
        if (saniye<=0)
        {
            game_scripts.Instance.RestartSceneOnClick(transform);
        }
        else 
        {
            int A = Mathf.FloorToInt(saniye);
            saniye_txt.text = A.ToString();
        }
        
    }
    public void voice()
    {
        audioSource.PlayOneShot(hit_voice);
    }
    public void ballVoiceAndRestart(int A)
    {
        waitingTime=0;
        audioSource.PlayOneShot(goal_voice);
        if (A==1)
        {
            jamoa1++;
            jamoa1_txt.text = jamoa1.ToString();
        }
        else if (A==2)
        {
            jamoa2++;
            jamoa2_txt.text = jamoa2.ToString();
        }

        if (jamoa1 > jamoa2)
        {
            game_scripts.hangi_oyuncu_kazandi=0303;
        }
        else if (jamoa2 > jamoa1)
        {
            game_scripts.hangi_oyuncu_kazandi=3030;
        }
        else game_scripts.hangi_oyuncu_kazandi=3333;


        if (jamoa1 >= 3 || jamoa2 >= 3)
        {
            game_scripts.Instance.RestartSceneOnClick(transform);
            return;
        }
        
        
        player1_football.Instance.transform.localPosition=new Vector3(-3,-2,100);
        player1_football.Instance.transform.rotation=new quaternion(0,0,0,0);

        player2_football.Instance.transform.localPosition=new Vector3(3,2,100);
        player2_football.Instance.transform.rotation=new quaternion(0,0,180,0);

        player3_football.Instance.transform.localPosition=new Vector3(-3,2,100);
        player3_football.Instance.transform.rotation=new quaternion(0,0,0,0);

        player4_football.Instance.transform.localPosition=new Vector3(3,-2,100);
        player4_football.Instance.transform.rotation=new quaternion(0,0,180,0);
    }
}
