using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monster : MonoBehaviour
{
    [SerializeField] GameObject box_prefab;
    public static monster Instance;
    float saniye;
    float random_saniye=1;
    float random_box_yeri;
    public Animator animator;
    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.Play("monster animation");
        random_box_yeri = random_sayi_al();
    }
    void FixedUpdate()
    {
        if (game_scripts.Instance.stopTime==true)
        {
            Instance.animator.enabled = false;
            return;
        } 
        else Instance.animator.enabled = true;
        
        saniye+=Time.deltaTime;
        if (saniye>random_saniye)
        {
            saniye=0;

            GameObject box = Instantiate(box_prefab,transform);
            box.transform.position=new Vector3(30,random_box_yeri,1);
            random_box_yeri = random_sayi_al();
        }
    }
    float random_sayi_al()
    {
        random_box_yeri = Random.Range(0,6);
        random_saniye = Random.Range(0.2f,0.7f);

        switch (random_box_yeri)
        {   
            case 0: return -2.5f;
            case 1: return -1.5f;
            case 2: return -0.5f;
            case 3: return 1.5f;
            case 4: return 2.5f;
            default: return 0;
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag=="Player1")
        {
            Destroy(other.gameObject);
            game_scripts.oyuncuSayisi--;
            game_scripts.Instance.kaybeden_oyuncu(0);//0==player1          
        }
        else if (other.tag=="Player2")
        {
            Destroy(other.gameObject);
            game_scripts.oyuncuSayisi--;
            game_scripts.Instance.kaybeden_oyuncu(1);//1==player2          
        }
        else if (other.tag=="Player3")
        {
            Destroy(other.gameObject);
            game_scripts.oyuncuSayisi--;
            game_scripts.Instance.kaybeden_oyuncu(2);//2==player3          
        }
        else if (other.tag=="Player4")
        {
            Destroy(other.gameObject);
            game_scripts.oyuncuSayisi--;
            game_scripts.Instance.kaybeden_oyuncu(3);//3==player4          
        }
        else if (other.tag=="box")
        {
            Destroy(other.gameObject);
            return;
        }
    }
}
