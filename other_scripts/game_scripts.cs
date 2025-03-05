using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEditor;
using UnityEngine.EventSystems;
using System;
using System.Linq;

public class game_scripts : MonoBehaviour
{
    public static game_scripts Instance;
    [SerializeField] GameObject winner_menu;
    public GameObject[] modlar;
    public static int kacKezOynariz;//fazla oynandigi zaman burada secilir
    public static int oyuncuSayisi;//bu sayi oynayacak
    public static List<int> game_numbers = new List<int>();//secilen modlar
    public static int hangi_oyuncu_kazandi;//hangi oyuncu kazandigini hesaplar  1p=3000, 2p=0300, 3p=0030, 4p=0003  ||   3333
    public static bool fazla_oyun;
    public bool stopTime=false;//menu acildiginde harseyi durduracak
    int kaybetme_sayisi;//onemli
    public Material[] player_materials;
    AudioSource audioSource;
    static bool ads_izlensinmi=true;
    public bool oyun_bitti=false;
    void Awake()
    {
        Instance=this;
        audioSource = GetComponent<AudioSource>();
        for (int i = 0; i < modlar.Length; i++)
        {
            modlar[i].SetActive(false);
        }
    }
    void Start()
    {
        kaybetme_sayisi=0;
        oyuncuSayisi = menu.secilen_oyuncu_sayisi_botlarla;
        if (menu.secilen_oyuncu_sayisi_botlarla==1) hangi_oyuncu_kazandi=0003;
        else if (menu.secilen_oyuncu_sayisi_botlarla==2) hangi_oyuncu_kazandi=0033;
        else if (menu.secilen_oyuncu_sayisi_botlarla==3) hangi_oyuncu_kazandi=0333;
        else if (menu.secilen_oyuncu_sayisi_botlarla==4) hangi_oyuncu_kazandi=3333;

        winner_menu.SetActive(false);

        modlar[TakeAndRemoveFirstItem()].SetActive(true);
        adsScript.Ads.LoadInterstitialAd();//reklamni yuklash
    }
    public void home()
    {
        SceneManager.LoadScene(1);
    }
    public void restart_button()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
    int TakeAndRemoveFirstItem()
    {
        if (game_numbers.Count == 1)
        {
            return game_numbers[0];
        }
        else if (game_numbers.Count > 0)
        {
            int item = game_numbers[0]; // 0. indeksteki öğeyi al
            game_numbers.RemoveAt(0);    // O öğeyi listeden kaldır
            return item;            // Alınan öğeyi döndür
        }
        return 0; // Liste boşsa 0 döndür
    }

    public void kaybeden_oyuncu(int A)//A==player id. yutqazgan o'yinchini belgilaydi. 3 yutgan,  0,1,2, yutqazgan
    {
        // Asl qiymatni olish va stringga aylantirish
        string currentValue = hangi_oyuncu_kazandi.ToString("D4");

        // Yangi raqamni stringda almashtirish
        char[] charArray = currentValue.ToCharArray();
        charArray[3 - A] = kaybetme_sayisi.ToString()[0];
        kaybetme_sayisi++;

        // Yangi qiymatni stringdan int ga aylantirish va ro'yxatga almashtirish
        hangi_oyuncu_kazandi= int.Parse(new string(charArray));
    }
    public void kazanan_oyuncu(int A)//yutgan o'yinchini belgilab qolgnlarini yutqazgan deb belgilaydi
    {
        // Barcha qiymatlarni "4" qilish
        char[] charArray = new char[] { '0', '0', '0', '0' };
        charArray[3 - A] = '3'; // yutgan o‘yinchining qiymati "3"

        // Yangi qiymatni stringdan int ga aylantirish va ro'yxatga almashtirish
        hangi_oyuncu_kazandi = int.Parse(new string(charArray));
    }
    public void  RestartSceneOnClick(Transform asd)//o'yin necha marta o'ynalishini boshqaradi
    {
        asd.GetChild(0).GetChild(0).GetChild(1).gameObject.SetActive(false);//pause button
        Instance?.audioSource.Play();

        for (int i = 0; i < 4; i++)//hangi oyuncular kazandini gorsterir.   4==oyuncu sayisi
        {
            // Natijani o'yinchiga tegishli bo'lagini ajratib olish
            int playerScore = (hangi_oyuncu_kazandi / (int)Math.Pow(10, i)) % 10;
            
            if (playerScore == 3 && menu.secilen_oyuncu_sayisi_botlarla > i)
            {
                asd.transform.GetChild(0).GetChild(0).GetChild(i+1).gameObject.SetActive(true);//kazanan oyuncu
                asd.transform.GetChild(0).GetChild(0).GetChild(i+1).GetChild(0).GetChild(4).GetComponent<TextMeshProUGUI>().text=PlayerPrefs.GetString("user_name"+(i+1),"Player"+(i+1));
            }
        }

        if (fazla_oyun)
        {
            StartCoroutine(winnerMenu(asd));
        }
        else//fazla oyun oynanmadigi zaman
        {
            asd.GetChild(0).GetChild(0).GetComponent<Animator>().Play("open");
            oyun_bitti=true;
            menu.kazanilan_para++;
            menu.parayi_ver = true;//menuya gectikten sonra parayi alabilir
            
            if (ads_izlensinmi) adsScript.Ads?.ShowInterstitialAd();//reklamni yuklash
            ads_izlensinmi=!ads_izlensinmi;
        }
        stopTime=true;
    }
    IEnumerator winnerMenu(Transform asd)
    {
        yield return new WaitForSeconds(3);
        asd.gameObject.SetActive(false);
        winner_menu.SetActive(true);
        winner_menu_script.Instance.oyuculari_gorster(hangi_oyuncu_kazandi);
        kacKezOynariz--;//her oyunde puani bir azalt
    }
}