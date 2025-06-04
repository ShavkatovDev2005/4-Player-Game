using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor;
using System;
public class market : MonoBehaviour
{
    public static market Instance;
    public static GameObject shop;
    public TextMeshProUGUI coin_txt,coin_txt1;
    [SerializeField] GameObject ads_button;
    void Awake()
    {
        Instance=this;
        shop = GameObject.Find("shop");
    }

    void Start()
    {
        shop.SetActive(false);
        coin_txt.text=PlayerPrefs.GetInt("Coin", 0).ToString();
    }
    public void market_back()
    {
        shop.SetActive(false);
    }
    public void market_open()
    {
        if (adsScript.adsRemover==1) ads_button.SetActive(false);
        shop.SetActive(true);
    }
    public void altinEkle(int altin)
    {
        int coin = PlayerPrefs.GetInt("Coin", 0);
        coin+=altin;
        // Coin miktarını güncelle
        coin_txt.text=coin.ToString();
        coin_txt1.text=coin.ToString();
        PlayerPrefs.SetInt("Coin",coin); // Coin değeri her değiştiğinde PlayerPrefs'e kaydedilir
        PlayerPrefs.Save();
    }
    public void altinCikar(int altin)
    {
        int coin = PlayerPrefs.GetInt("Coin", 0);
        coin-=altin;
        // Coin miktarını güncelle
        coin_txt.text=coin.ToString();
        coin_txt1.text=coin.ToString();
        PlayerPrefs.SetInt("Coin",coin); // Coin değeri her değiştiğinde PlayerPrefs'e kaydedilir
        PlayerPrefs.Save();
    }
}
