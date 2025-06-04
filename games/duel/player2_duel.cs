using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player2_duel : MonoBehaviour
{
    [SerializeField] GameObject[] bullet;
    public float turnSpeed;
    bool turnRight;
    int bullet_sayac=4;
    float saniye_hesap;
    void Update()
    {
        if (game_scripts.Instance.stopTime==true) return;

        float rotation = turnSpeed * Time.deltaTime * (turnRight ? 1 : -1);// dönme
        transform.Rotate(0, 0, rotation);// dönme
        transform.localPosition=new Vector3(0 ,2.4f,0);
        // Tüm mermileri sırayla kontrol et ve aktif et
        for (int i = 0; i < bullet.Length; i++) 
        {
            // Eğer mermi sayısı i'ye eşitse, aktif yap
            if (i < bullet_sayac) 
            {
                bullet[i].SetActive(true);
            } 
            else 
            {
                bullet[i].SetActive(false);
            }
        }
        saniye_hesap+= Time.deltaTime;
        if (bullet_sayac<=0)
        {
            if (saniye_hesap>=3f)
            {
                saniye_hesap=0f;
                bullet_sayac=4;
                duel_other_setting.Instance.bullet_full_voice();
            }
        }
        else{saniye_hesap=0f;}
    }
    public void turn()
    {
        turnRight=!turnRight;
        if (bullet_sayac>0)
        {
            duel_other_setting.Instance.FireBullet(transform, turnRight);
            bullet_sayac--;
        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        turnRight=!turnRight;
    }
}
