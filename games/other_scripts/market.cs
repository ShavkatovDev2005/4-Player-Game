using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor;
using System;
public class market : MonoBehaviour
{
    public static market Instance;
    public static int coin;
    public static GameObject shop;
    public TextMeshProUGUI coin_txt;
    void Awake()
    {
        Instance=this;
    }

    void Start()
    {
        coin = PlayerPrefs.GetInt("Coin", 0);  // Eğer PlayerPrefs'te "Coin" yoksa varsayılan olarak 0 döner
        shop = GameObject.Find("shop");
        shop.SetActive(false);
        coin_txt.text=coin.ToString();
    }
    public void market_back()
    {
        shop.SetActive(false);
    }
    public void market_open()
    {
        shop.SetActive(true);
    }
    public void altinEkle(int altin)
    {
        coin+=altin;
        // Coin miktarını güncelle
        coin_txt.text=coin.ToString();
        PlayerPrefs.SetInt("Coin",coin); // Coin değeri her değiştiğinde PlayerPrefs'e kaydedilir
        PlayerPrefs.Save();
    }
    public void altinCikar(int altin)
    {
        coin-=altin;
        // Coin miktarını güncelle
        coin_txt.text=coin.ToString();
        PlayerPrefs.SetInt("Coin",coin); // Coin değeri her değiştiğinde PlayerPrefs'e kaydedilir
        PlayerPrefs.Save();
    }
}
