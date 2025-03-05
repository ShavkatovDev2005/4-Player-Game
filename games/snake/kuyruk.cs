using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kuyruk : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag=="Player1" || other.tag=="Player2" || other.tag=="Player3" || other.tag=="Player4")
        {
            other.gameObject.SetActive(false);
            game_scripts.oyuncuSayisi--;
            other_setting_snike.Instance.destroy_audio();
            if (other.tag=="Player1") game_scripts.Instance.kaybeden_oyuncu(0);//0==player1          
            else if (other.tag=="Player2") game_scripts.Instance.kaybeden_oyuncu(1);//1==player2          
            else if (other.tag=="Player3") game_scripts.Instance.kaybeden_oyuncu(2);//2==player3          
            else if (other.tag=="Player4") game_scripts.Instance.kaybeden_oyuncu(3);//3==player4          
        }
    }
}
