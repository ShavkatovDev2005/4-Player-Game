using UnityEngine;
using GoogleMobileAds;
using GoogleMobileAds.Api;
using Unity.VisualScripting;
using System.Collections;

public class adsScript : MonoBehaviour
{
    public static adsScript Ads;
    private string _adUnitId = "ca-app-pub-3940256099942544/1033173712";
    private InterstitialAd _interstitialAd;
    public static int adsRemover;//ads olib tashlansa Remover 1 bo'ladi
    public void Start()
    {
        adsRemover = PlayerPrefs.GetInt("adsRemove",0);
        if (adsRemover==0)
        {
            DontDestroyOnLoad(gameObject);//sahnalar o'zgarganda obyekt aktif qoladi
            Ads = this;
            // Initialize the Google Mobile Ads SDK.
            MobileAds.Initialize((InitializationStatus initStatus) =>
            {
                // This callback is called once the MobileAds SDK is initialized.
            });
        }
    }

    public void LoadInterstitialAd()
    {
        if (adsRemover==0)
        {
            if (_interstitialAd != null)
            {
                _interstitialAd.Destroy();
                _interstitialAd = null;
            }

            var adRequest = new AdRequest();

            InterstitialAd.Load(_adUnitId, adRequest,(InterstitialAd ad, LoadAdError error) =>
            {
                // if error is not null, the load request failed.
                if (error != null || ad == null)
                {
                    return;
                }
                _interstitialAd = ad;
            });
        }
    }

    public void ShowInterstitialAd()
    {
        if (adsRemover==0)
        {
            if (_interstitialAd != null && _interstitialAd.CanShowAd())
            {
                _interstitialAd.Show();
            }
        }
        StartCoroutine(loadADS());
    }
    IEnumerator loadADS()
    {
        yield return new WaitForSeconds(2);
        LoadInterstitialAd();
    }
    
    public void removeAds()//ads remove yapma ishlemi.  (satin alinacak)
    {
        adsRemover=1;
        PlayerPrefs.SetInt("adsRemove",adsRemover);
        PlayerPrefs.Save();
    }
}