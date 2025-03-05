using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class menu_prefab_animation : MonoBehaviour
{
    [SerializeField] Transform A;
    void Update()
    {
        if (A.gameObject.activeSelf) A.transform.Rotate(0, 0, 10f * Time.deltaTime);// donen isik
    }
    public void open_animation()
    {
        game_scripts.Instance.stopTime=true;
        transform.GetChild(5).gameObject.SetActive(true);//pausemenu
        transform.GetChild(6).gameObject.SetActive(false);//pause button
        
        if (game_scripts.Instance.oyun_bitti) transform.GetChild(5).GetChild(1).gameObject.SetActive(false);//continiu button
        else transform.GetChild(5).GetChild(1).gameObject.SetActive(true);//continiu button

        if (game_scripts.fazla_oyun)//agar fazla oyun true bolsa restart buttun false boladi
        {
            transform.GetChild(5).GetChild(2).gameObject.SetActive(false);//restart button
        }

        transform.GetChild(0).GetComponent<Image>().enabled = true;//background
    }
    public void close2()
    {
        transform.GetChild(5).gameObject.SetActive(false);
    }
    public void close1()
    {
        game_scripts.Instance.stopTime=false;

        transform.GetChild(0).GetComponent<Image>().enabled = false;//background
        transform.GetChild(6).gameObject.SetActive(true);//pause button
    }
}