using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;

public class purchasSetting : MonoBehaviour
{
    private string coin2000 = "2000coin";
    private string coin4000 = "4000coin";
    private string coin6000 = "6000coin";
    private string coin10000 = "10000coin";
    private string removeAds = "adsRemove";
    public void OnPurchasecomplate(Product product)
    {
        if (product.definition.id == coin2000)
        {
            market.Instance.altinEkle(2000);
        }
        else if (product.definition.id == coin4000)
        {
            market.Instance.altinEkle(4000);
        }
        else if (product.definition.id == coin6000)
        {
            market.Instance.altinEkle(6000);
        }
        else if (product.definition.id == coin10000)
        {
            market.Instance.altinEkle(10000);
        }
        else if (product.definition.id == removeAds)
        {
            adsScript.Ads.removeAds(); 
        }
    }
    public void OnPurchaseFailed(Product product, PurchaseFailureDescription purchaseFailureDescription)
    {
        Debug.Log(product.definition.id + "purchase failure reason" + purchaseFailureDescription);
    }
}
