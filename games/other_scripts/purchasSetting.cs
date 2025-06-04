using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;

public class purchasSetting : MonoBehaviour
{
    private string coin2000 = "2000coin";
    private string coin5000 = "5000coin";
    private string coin10000 = "10000coin";
    private string coin20000 = "20000coin";
    private string coin100000 = "100000coin";
    private string removeAds = "adsRemove";
    public void OnPurchasecomplate(Product product)
    {
        if (product.definition.id == coin2000)
        {
            market.Instance.altinEkle(2000);
        }
        else if (product.definition.id == coin5000)
        {
            market.Instance.altinEkle(5000);
        }
        else if (product.definition.id == coin10000)
        {
            market.Instance.altinEkle(10000);
        }
        else if (product.definition.id == coin20000)
        {
            market.Instance.altinEkle(20000);
        }
        else if (product.definition.id == coin100000)
        {
            market.Instance.altinEkle(100000);
        }
        else if (product.definition.id == removeAds)
        {
            adsScript.Ads.removeAds();
            Debug.Log("ads remove ended");
        }

        StartCoroutine(close_screen());
    }
    public void OnPurchaseFailed(Product product, PurchaseFailureDescription purchaseFailureDescription)
    {
        Debug.Log(product.definition.id + "purchase failure reason" + purchaseFailureDescription);
    }
    IEnumerator close_screen()
    {
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
    }
}
