using UnityEngine;
using UnityEngine.UI;

public class other_setting_knife : MonoBehaviour
{
    public static other_setting_knife Instance;
    public GameObject[] knife;
    [SerializeField] AudioClip touch;
    [SerializeField] AudioClip successful;
    [SerializeField] AudioClip unsuccessful;
    [SerializeField] Image[] buttons_image;
    private AudioSource audioSource;
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
    void FixedUpdate()
    {
        if (game_scripts.Instance?.stopTime==true) return;

        if (game_scripts.oyuncuSayisi<=1)
        {
            game_scripts.Instance.RestartSceneOnClick(transform);//restart the game
        }
    }
#region audio player
    public void play_audioclipTouch()
    {
        audioSource.PlayOneShot(touch);
    }
    public void play_audioclipSuccsessful()
    {
        audioSource.PlayOneShot(successful);
    }
    public void play_audioclipUnsuccessful()
    {
        audioSource.PlayOneShot(unsuccessful);
    }
    public void play_go_voice_and_stoptime()
    {
        game_scripts.Instance.play_go_voice_and_stoptime();
    }
#endregion
}
