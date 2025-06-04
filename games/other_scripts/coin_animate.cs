using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;

public class menuCoinMove : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI coinText;
    [SerializeField] AudioClip  coin_audio;
    [SerializeField] float moveSpeed;
    AudioSource audioSource;
    public GameObject coinObj;
    public GameObject targetOBJ;
    public Ease ease;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void verilen_parayi_al()
    {
        StartCoroutine(_collect());
    }
    IEnumerator _collect()
    {
        var parent = transform.parent;
        for (int i = 0; i < 20; i++)
        {
            audioSource.PlayOneShot(coin_audio);
            GameObject coinObject = Instantiate(coinObj , parent);//parent == trasnform pos
            coinObject.transform.localPosition = new Vector3(Random.Range(-100,100) ,Random.Range(-100,100), -200);
            coinObject.transform.DOMove(targetOBJ.transform.position,moveSpeed).SetEase(ease);
            Destroy(coinObject,2);
            yield return new WaitForSeconds(0.1f);
        }

        market.Instance.altinEkle(menu.kazanilan_para);
        coinText.text = PlayerPrefs.GetInt("Coin", 0).ToString();
        gameObject.SetActive(false);
    }
}
