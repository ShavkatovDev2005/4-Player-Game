using Unity.Mathematics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player3_knife : MonoBehaviour
{
    public static player3_knife Instance;
    public int selected_knife;
    public int imkoniyat;
    public bool hayatta=true;
    [SerializeField] GameObject[] _imkoniyat;
    private GameObject shootable;
    private Queue<GameObject> knifesList = new Queue<GameObject>();
    void Start()
    {
        selected_knife = PlayerPrefs.GetInt("knife3",0);
        transform.GetChild(selected_knife).gameObject.SetActive(true);
        Instance = this;
    }
    public void buttonExit()
    {
        if (game_scripts.Instance.stopTime==true) return;

        if (imkoniyat<5 && hayatta) 
        {
            imkoniyat++;
            shootable = Instantiate(other_setting_knife.Instance.knife[selected_knife],transform);
            shootable.transform.rotation = Quaternion.Euler(0,0, -116);
            Rigidbody2D rb = shootable.GetComponent<Rigidbody2D>();
            Collider2D col = shootable.GetComponent<Collider2D>();
            shootable.GetComponent<knife>().hangi_oyuncu_atti=3;
            
            rb.gravityScale = -1;
            col.isTrigger = false;
            rb.AddForce(-transform.localPosition/18,ForceMode2D.Impulse);

            _imkoniyat[imkoniyat-1].gameObject.SetActive(false);//nechta imkoniyat qolganini ko'rsatadi
            if (imkoniyat==5) transform.GetChild(selected_knife).gameObject.SetActive(false);//knife image
            other_setting_knife.Instance.play_audioclipTouch();//ses cal
        }
    }
}
