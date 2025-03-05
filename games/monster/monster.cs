using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monster : MonoBehaviour
{
    [SerializeField] GameObject prefab;
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
            random_saniye = Random.Range(0.7f,2);

            GameObject A = Instantiate(prefab);
            A.transform.position=new Vector3(transform.position.x+21,random_box_yeri,1);
            random_box_yeri = random_sayi_al();
        }
    }
    float random_sayi_al()
    {
        random_box_yeri = Random.Range(0,6);
        random_saniye = Random.Range(0.5f,2);
        if (random_box_yeri==0) return 42;
        else if (random_box_yeri==1) return 41.2f;
        else if (random_box_yeri==2) return 40.4f;
        else if (random_box_yeri==3) return 39.62f;
        else if (random_box_yeri==4) return 38.78f;
        else return 37.93f;
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
