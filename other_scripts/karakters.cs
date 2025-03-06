using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class karakters : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip click;
    [SerializeField] AudioClip buying_audio,not_buying_audio;
    [SerializeField] Material player_color;
    [SerializeField] Material[] players_material;
    

	public TextMeshProUGUI obj_text;
	public TMP_InputField username;

    string tank_string;//satin alindini belirtecek   0 ile 1 le
    string knife_string;//satin alindini belirtecek   0 ile 1 le
    string color_string;//satin alindini belirtecek   0 ile 1 le
    int player;
    [SerializeField] GameObject[] playerSelectButton;
    void Awake()
    {
        player=1;
        username.text = PlayerPrefs.GetString("user_name"+player.ToString(),"Player"+player.ToString());
        tank_string= PlayerPrefs.GetString("tank_string","111100000000000");//15 tank
        knife_string= PlayerPrefs.GetString("knife_string","11110000000");//11 knife
        color_string= PlayerPrefs.GetString("color_string","111100000000");//12 color

        load_color();

        playerSelectButton[0].transform.GetChild(1).gameObject.SetActive(true);//tanlangan player buttoni
    }
#region tank
    public void tank_select(int secilen)
    {
        //               0    1   2   3    4      5      6      7      8      9      10     11     12     13     14     15     16     17
        int[] price = {  0 ,  0 , 0 , 0 , 2000 , 2000 , 2000 , 2000 , 2000 , 5000 , 5000 , 5000 , 5000 , 5000 , 5000 , 5000 , 5000 , 5000};  // Fiyatlar
        if (secilen < 0 || secilen >= price.Length) return;  // Geçerli secilen değerini kontrol et

        if (market.coin >= price[secilen] || tank_string[secilen]=='1')
        {
            // Seçilen tankı güncelle
            if (player == 1) PlayerPrefs.SetInt("tank1",secilen);
            else if (player == 2) PlayerPrefs.SetInt("tank2",secilen);
            else if (player == 3) PlayerPrefs.SetInt("tank3",secilen);
            else if (player == 4) PlayerPrefs.SetInt("tank4",secilen);
            tank_selected();

            if (tank_string[secilen]=='1')
            {
                audioSource.PlayOneShot(click);
                PlayerPrefs.Save(); // Değişiklikleri kaydet
                return;//eger tank onceden satin alindiyse return
            }

            market.Instance.altinCikar(price[secilen]);  // Coin çıkarma
            transform.GetChild(3).GetChild(secilen).GetChild(0).gameObject.SetActive(false);//para yazisini kapat
            tank_string = tank_string.Substring(0, secilen) + '1' + tank_string.Substring(secilen+1);//tank satin alindigini belirtecek
            PlayerPrefs.SetString("tank_string",tank_string); // karakter değeri her değiştiğinde PlayerPrefs'e kaydedilir
            PlayerPrefs.Save();
            audioSource.PlayOneShot(buying_audio);
        }
        else 
        {
            audioSource.PlayOneShot(not_buying_audio);
            market.shop.SetActive(true);  // Yetersiz coin
            return;
        }
    }
    public void load_tank()
    {
        for (int i = 0; i < tank_string.Length; i++)
        {
            if (tank_string[i]=='1')
            {
                transform.GetChild(3).GetChild(i).GetChild(0).gameObject.SetActive(false);// satin alinan tank parasi kapanacak
            }
        }
        transform.GetChild(2).gameObject.SetActive(false);//knife menu
        transform.GetChild(1).gameObject.SetActive(false);//color menu
        transform.GetChild(3).gameObject.SetActive(true);//tank menu
        tank_selected();
    }
    void tank_selected()//tankadaki ptichkani to'g'irlaydi
    {
        for (int i = 0; i < tank_string.Length; i++)//tankdaki hamma ptichkalarini o'chiradi
        {
            transform.GetChild(3).GetChild(i).transform.GetChild(1).gameObject.SetActive(false);
        }
        for (int i = 1;i <= 5; i++)//keraklisini topadi
        {
            if (player==i)//keraklisini yoqadi
            {
                transform.GetChild(3).GetChild(PlayerPrefs.GetInt("tank"+i.ToString(),player-1)).transform.GetChild(1).gameObject.SetActive(true);//secilen tank
            }
        }
    }
#endregion
#region knife
    public void knife_select(int secilen)
    {
        //               0    1   2   3      4      5      6      7      8      9     10
        int[] price = {  0 ,  0 , 0 , 0 , 2000 , 2000 , 2000 , 5000 , 5000 , 5000 , 5000};  // Fiyatlar
        if (secilen < 0 || secilen >= price.Length) return;  // Geçerli secilen değerini kontrol et

        if (market.coin >= price[secilen] || knife_string[secilen]=='1')
        {
            // Seçilen tankı güncelle
            if (player == 1) PlayerPrefs.SetInt("knife1",secilen);
            else if (player == 2) PlayerPrefs.SetInt("knife2",secilen);
            else if (player == 3) PlayerPrefs.SetInt("knife3",secilen);
            else if (player == 4) PlayerPrefs.SetInt("knife4",secilen);
            knife_selected();

            if (knife_string[secilen]=='1')
            {
                audioSource.PlayOneShot(click);
                PlayerPrefs.Save(); // Değişiklikleri kaydet
                return;//eger tank onceden satin alindiyse return
            }

            market.Instance.altinCikar(price[secilen]);  // Coin çıkarma
            transform.GetChild(2).GetChild(secilen).GetChild(0).gameObject.SetActive(false);//para yazisini kapat
            knife_string = knife_string.Substring(0, secilen) + '1' + knife_string.Substring(secilen+1);//knife satin alindigini belirtecek
            PlayerPrefs.SetString("knife_string",knife_string); // karakter değeri her değiştiğinde PlayerPrefs'e kaydedilir
            PlayerPrefs.Save();
            audioSource.PlayOneShot(buying_audio);
        }
        else
        {
            audioSource.PlayOneShot(not_buying_audio);
            market.shop.SetActive(true);  // Yetersiz coin
            return;
        }

    }
    public void load_knife()
    {
        for (int i = 0; i < knife_string.Length; i++)
        {
            if (knife_string[i]=='1')
            {
                transform.GetChild(2).GetChild(i).GetChild(0).gameObject.SetActive(false);// satin alinan kinfe parasi kapanacak
            }
        }
        transform.GetChild(3).gameObject.SetActive(false);//tank menu
        transform.GetChild(1).gameObject.SetActive(false);//color menu
        transform.GetChild(2).gameObject.SetActive(true);//knife menu
        knife_selected();
    }
    void knife_selected()//tankadaki ptichkani to'g'irlaydi
    {
        for (int i = 0; i < knife_string.Length; i++)//hamma ptichkalani o'chiradi
        {
            transform.GetChild(2).GetChild(i).transform.GetChild(1).gameObject.SetActive(false);
        }
        for (int i = 1;i <= 5; i++)//keraklisini topadi
        {
            if (player==i)//keraklisini yoqadi
            {
                transform.GetChild(2).GetChild(PlayerPrefs.GetInt("knife"+i.ToString(),player-1)).transform.GetChild(1).gameObject.SetActive(true);
            }
        }
    }
#endregion  
# region color
    public void color_select(int secilen)
    {
        //               0    1   2   3    4      5       6       7       8       9      10      11
        int[] price = {  0 ,  0 , 0 , 0 , 5000 , 5000 , 10000 , 10000 , 15000 , 15000 , 15000 , 15000};  // Fiyatlar
        if (secilen < 0 || secilen >= price.Length) return;  // Geçerli secilen değerini kontrol et

        if (market.coin >= price[secilen] || color_string[secilen]=='1')
        {
            // Seçilen color güncelle
            if (player == 1) PlayerPrefs.SetInt("color1",secilen);
            else if (player == 2) PlayerPrefs.SetInt("color2",secilen);
            else if (player == 3) PlayerPrefs.SetInt("color3",secilen);
            else if (player == 4) PlayerPrefs.SetInt("color4",secilen);
            color_selected();

            if (color_string[secilen]=='1')
            {
                audioSource.PlayOneShot(click);
                PlayerPrefs.Save(); // Değişiklikleri kaydet
                return;//eger tank onceden satin alindiyse return
            }

            market.Instance.altinCikar(price[secilen]);  // Coin çıkarma
            transform.GetChild(1).GetChild(secilen).GetChild(0).gameObject.SetActive(false);//para yazisini kapat
            color_string = color_string.Substring(0, secilen) + '1' + color_string.Substring(secilen+1);//tank satin alindigini belirtecek
            PlayerPrefs.SetString("color_string",color_string); // karakter değeri her değiştiğinde PlayerPrefs'e kaydedilir
            PlayerPrefs.Save();
            audioSource.PlayOneShot(buying_audio);
        }
        else 
        {
            audioSource.PlayOneShot(not_buying_audio);
            market.shop.SetActive(true);  // Yetersiz coin
            return;
        }

    }
    public void load_color()
    {
        for (int i = 0; i < color_string.Length; i++)
        {
            if (color_string[i]=='1')
            {
                transform.GetChild(1).GetChild(i).GetChild(0).gameObject.SetActive(false);// satin alinan color parasi kapanacak
            }
        }
        
        transform.GetChild(3).gameObject.SetActive(false);//tank menu
        transform.GetChild(2).gameObject.SetActive(false);//knife menu
        transform.GetChild(1).gameObject.SetActive(true);//color menu
        color_selected();
    }
    void color_selected()//colordaki ptichkani to'g'irlaydi
    {
        for (int i = 0; i < color_string.Length; i++)//hamma ptichkalani o'chiradi
        {
            transform.GetChild(1).GetChild(i).transform.GetChild(1).gameObject.SetActive(false);
        }
        for (int i = 1;i <= 5; i++)//keraklisini topadi
        {
            if (player==i)//keraklisini yoqadi
            {   //change color
                player_color.color = transform.GetChild(1).GetChild(PlayerPrefs.GetInt("color"+i.ToString(),player-1)).GetComponent<Image>().color;//changing player color
                transform.GetChild(1).GetChild(PlayerPrefs.GetInt("color"+i.ToString(),player-1)).GetChild(1).gameObject.SetActive(true);//selected color
            }
        }
        players_material[player-1].color = player_color.color;

        for (int i = 0; i < 4; i++)//menu player button color change
        {
            transform.GetChild(4+i).GetComponent<Image>().color = players_material[i].color;
        }
    }
#endregion
    public void player_select(int _player)
    {
        playerSelectButton[player-1].transform.GetChild(1).gameObject.SetActive(false);//secilen player fream
        playerSelectButton[_player-1].transform.GetChild(1).gameObject.SetActive(true);//secilen player fream
        player=_player;
        
        if (transform.GetChild(2).gameObject.activeSelf) knife_selected();
        if (transform.GetChild(3).gameObject.activeSelf) tank_selected();
        color_selected();
        change_player_username();
    }
    void change_player_username()
    {
        username.text = PlayerPrefs.GetString("user_name"+player.ToString(),"Player"+player.ToString());
    }
    public void Create_username()
	{
		obj_text.text = username.text;
		PlayerPrefs.SetString("user_name"+player.ToString(), obj_text.text);
		PlayerPrefs.Save();
	}

    void OnApplicationQuit()
    {
        PlayerPrefs.Save();
    }
}