using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class duel_bullet : MonoBehaviour
{
    public duel_other_setting duel_Other_Setting;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (game_scripts.Instance.stopTime==true) return;
        
        if (other.gameObject.tag=="Player1")
        {
            Destroy(other.gameObject);
            game_scripts.oyuncuSayisi--;
            game_scripts.Instance.kaybeden_oyuncu(0);//0==player1      
        }
        else if (other.gameObject.tag=="Player2")
        {
            Destroy(other.gameObject);
            game_scripts.oyuncuSayisi--;
            game_scripts.Instance.kaybeden_oyuncu(1);//1==player2      
        }
        else if (other.gameObject.tag=="Player3")
        {
            Destroy(other.gameObject);
            game_scripts.oyuncuSayisi--;
            game_scripts.Instance.kaybeden_oyuncu(2);//2==player3      
        }
        else if (other.gameObject.tag=="Player4")
        {
            Destroy(other.gameObject);
            game_scripts.oyuncuSayisi--;
            game_scripts.Instance.kaybeden_oyuncu(3);//3==player4      
        }
        else if (other.gameObject.tag=="box")
        {
            Destroy(other.gameObject);
        }
        
        if (game_scripts.oyuncuSayisi<=1)
        {
            duel_Other_Setting.restart_game();
        }
        Destroy(gameObject);
    }
}
