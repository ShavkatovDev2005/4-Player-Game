using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class knife : MonoBehaviour
{
    bool carpisti;
    Rigidbody2D _rb;
    public int hangi_oyuncu_atti;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag=="knife" && !carpisti)
        {
            carpisti=true;
            game_scripts.oyuncuSayisi--;
            _rb.velocity = new Vector2(_rb.velocity.x,-2);
            other_setting_knife.Instance.play_audioclipUnsuccessful();
            if (hangi_oyuncu_atti==1){
                player1_knife.Instance.transform.GetChild(player1_knife.Instance.selected_knife).gameObject.SetActive(false);
                player1_knife.Instance.hayatta=false;
                game_scripts.Instance.kaybeden_oyuncu(0);//3==player1          
            }
            else if (hangi_oyuncu_atti==2){
                player2_knife.Instance.transform.GetChild(player2_knife.Instance.selected_knife).gameObject.SetActive(false);
                player2_knife.Instance.hayatta=false;
                game_scripts.Instance.kaybeden_oyuncu(1);//player2          
            }
            else if (hangi_oyuncu_atti==3){
                player3_knife.Instance.transform.GetChild(player3_knife.Instance.selected_knife).gameObject.SetActive(false);
                player3_knife.Instance.hayatta=false;
                game_scripts.Instance.kaybeden_oyuncu(2);//player3          
            }
            else if (hangi_oyuncu_atti==4){
                player4_knife.Instance.transform.GetChild(player4_knife.Instance.selected_knife).gameObject.SetActive(false);
                player4_knife.Instance.hayatta=false;
                game_scripts.Instance.kaybeden_oyuncu(3);//player4          
            }
            Destroy(gameObject,2);
        }
        else if (other.gameObject.tag=="daire" && !carpisti)
        {
            carpisti=true;
            _rb.bodyType= RigidbodyType2D.Static;
            _rb.velocity = new Vector2(0,0);
            transform.SetParent(other.collider.transform);
            other_setting_knife.Instance.play_audioclipSuccsessful();
            if (player1_knife.Instance.imkoniyat==5 && player1_knife.Instance.hayatta)
            {
                game_scripts.Instance.kazanan_oyuncu(0);
                game_scripts.oyuncuSayisi=1;
            }
            else if (player2_knife.Instance.imkoniyat==5 && player2_knife.Instance.hayatta)
            {
                game_scripts.Instance.kazanan_oyuncu(1);
                game_scripts.oyuncuSayisi=1;
            }
            else if (player3_knife.Instance.imkoniyat==5 && player3_knife.Instance.hayatta)
            {
                game_scripts.Instance.kazanan_oyuncu(2);
                game_scripts.oyuncuSayisi=1;
            }
            else if (player4_knife.Instance.imkoniyat==5 && player4_knife.Instance.hayatta)
            {
                game_scripts.Instance.kazanan_oyuncu(3);
                game_scripts.oyuncuSayisi=1;
            }
        }
        else if (other.gameObject.tag=="box")
        {
            Destroy(gameObject);
        }
    }
}
