using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class menu : MonoBehaviour
{
    public GameObject[] modlar;
    public AudioSource audioSource; 
    public string my_telegram = "https://t.me/devshavkatov";
    public string my_instagram = "https://www.instagram.com/sirojiddin_88_30";
    public static int kazanilan_para;
    public static bool parayi_ver;
    public static bool NPC_olsun;
    public static int secilen_oyuncu_sayisi;
    public static int secilen_oyuncu_sayisi_botlarla;

    int alinacak_para;

    [Header("coin")]
    [SerializeField] GameObject kazanilan_para_object;
    [SerializeField] TextMeshProUGUI kazanilan_para_txt;
    [SerializeField] TextMeshProUGUI coin;
    [SerializeField] AudioClip click;
    [SerializeField] Animator coin_animator;
    [SerializeField] AudioClip coin_audio;

    [SerializeField] GameObject coklu_menu,tekli_menu;

    [SerializeField] GameObject settings;
    [SerializeField] GameObject menu_camera;
    int selected_game;
    void Awake()
    {
        for (int i = 0; i < modlar.Length; i++)//gereksiz sahneleri kapat
        {
            modlar[i].SetActive(false);
        }
        modlar[0].SetActive(true);
    }
    void Start()
    {
        coin.text = market.coin.ToString();
        settings.SetActive(false);

        if (Application.internetReachability == NetworkReachability.NotReachable && PlayerPrefs.GetInt("adsRemove",0)==0)//check internet connection
        {
            menu_camera.transform.GetChild(2).gameObject.SetActive(true);
        }
        else{
            menu_camera.transform.GetChild(2).gameObject.SetActive(false);
        }

        if (parayi_ver)
        {
            for (int i = 1; i <= kazanilan_para; i++)
            {
                alinacak_para += 100;
            }
            kazanilan_para_txt.text = "You have " + alinacak_para + " coin".ToString();
            kazanilan_para_object.SetActive(true);
            parayi_ver=false;
        }
        audioSource.Play();//muzik cal
    }
    public void verilen_parayi_al()
    {
        coin_animator.Play("coin");
        audioSource.PlayOneShot(coin_audio);
        StartCoroutine(animatoru_baslat());
    }
    IEnumerator animatoru_baslat()
    {
        // Hozirgi animatsiya davomiyligini olish
        AnimatorStateInfo stateInfo = coin_animator.GetCurrentAnimatorStateInfo(0);
        float animationTime = stateInfo.length; // Animatsiyaning uzunligi

        yield return new WaitForSeconds(animationTime+1); // Animatsiya tugashini kutish

        market.Instance.altinEkle(alinacak_para);
        coin.text = market.coin.ToString();
        kazanilan_para_object.SetActive(false);
        alinacak_para=0;
    }
    public void oyunu_sec(bool coklu)
    {
        if (coklu)
        {
            coklu_menu.SetActive(true);
            tekli_menu.SetActive(false);
            game_scripts.fazla_oyun=true;
        }
        else
        {
            coklu_menu.SetActive(false);
            tekli_menu.SetActive(true);
            game_scripts.fazla_oyun=false;
        }
    }

    public void play_game_4_player(int player_sayisi)
    {
        secilen_oyuncu_sayisi = player_sayisi;

        if (player_sayisi==1)
        {
            take_NPC(true);//take NPC
        }
        else if (player_sayisi==4)
        {
            take_NPC(false);//don't take NPC
        }
        else
        {
            modlara_gecis(5);//ask about NPC
        }
    }
    public void take_NPC(bool NPC)
    {
        if (NPC) secilen_oyuncu_sayisi_botlarla=4;//eger NPC oynamasi istenirse playercount==4
        else secilen_oyuncu_sayisi_botlarla = secilen_oyuncu_sayisi;

        NPC_olsun = NPC;

        if (!game_scripts.fazla_oyun)
        {
            kazanilan_para = 0;
            SceneManager.LoadScene(2);
        }
        else{
            modlara_gecis(4);
        }
    }

    public void modlara_gecis(int _mod)//mod(canvas) secme islemi
    {
        for (int i = 0; i < modlar.Length; i++)
        {
            if (modlar[i].activeSelf)  // Eğer mod aktifse
            {
                modlar[i].SetActive(false);//aktif modu kapat
                modlar[_mod].SetActive(true);//istenen modu ac
                break;  // Bulunduğunda döngüden çık
            }
        }
    }
    public void telegram()
    {
        Application.OpenURL(my_telegram);
    }
    public void instagram()
    {
        Application.OpenURL(my_instagram);
    }
    
    public void oyunSecim1(int oyun)
    {
        game_scripts.kacKezOynariz=0;
        game_scripts.game_numbers.Clear();
        game_scripts.game_numbers.Add(oyun);
        modlara_gecis(1);
    }
    public void play_tank(int arena)
    {
        other_setting_tank.arena=arena;//tank arena sec
        oyunSecim1(0);
    }

#region multi game setting region
    public void select_count(int count)
    {
        game_scripts.game_numbers.Clear();//listi bosalt
        selected_game=0;
        game_scripts.kacKezOynariz=count-1;
        kazanilan_para = count;
        modlara_gecis(3);
    }
    public void select_game(int count)
    {
        selected_game++;
        game_scripts.game_numbers.Add(count);//oyunleri liste ekle
        if (selected_game>game_scripts.kacKezOynariz)
        {
            winner_menu_script.players_count = new int[4];
            SceneManager.LoadScene(2);
        }
    }
    public void random_button()//oyunu random olarak secer
    {
        for (int i = 0; i < game_scripts.kacKezOynariz; i++)
        {
            int a = Random.Range(0,12);//burada mod eklendigi zaman sayi arttirilacak
            game_scripts.game_numbers.Add(a);
        }
        winner_menu_script.players_count = new int[4];
        SceneManager.LoadScene(2);
    }
    public void setting_button()
    {
        settings.SetActive(!settings.activeInHierarchy);
    }
#endregion

    public void Button_Audio()
    {
        audioSource.PlayOneShot(click);
    }

    public void CheckInternet()
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
           menu_camera.transform.GetChild(2).gameObject.SetActive(false);
        }
    }
}