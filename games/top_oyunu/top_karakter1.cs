using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class top_karakter1 : MonoBehaviour
{
    // public static top_karakter1 Instance;
    public TextMeshProUGUI life_count_text;
    public int life_count=3;
    SpriteRenderer color;
    BoxCollider2D colider;
    bool hareket;
    bool oldu=false;
    bool basla=false;
    void Start()
    {
        // Instance = this;
        color = transform.GetChild(1).GetComponent<SpriteRenderer>();
        colider = GetComponent<BoxCollider2D>();
    }
    void FixedUpdate()
    {
        if (game_scripts.Instance.stopTime==true || oldu) return;

        if (basla) transform.GetChild(0).transform.position+=new Vector3(0, hareket ? -0.1f:0.1f, 0);
    }
    public void button()
    {
        hareket = !hareket;
        basla=true;
    }
    void count()
    {
        life_count--;
        life_count_text.text = life_count.ToString();
        if (life_count<=0)
        {
            Destroy(transform.GetChild(0).gameObject);
            oldu = true;
            color.color = new Color(1f, 0f, 0f, 0.5f);
            colider.isTrigger = false;
            game_scripts.oyuncuSayisi--;
            game_scripts.Instance.kaybeden_oyuncu(0);//0==player1         
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag=="ball" && life_count>=1)
        {
            count();
        }
    }
}
