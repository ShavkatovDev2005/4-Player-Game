using Unity.VisualScripting;
using UnityEngine;

public class player_tank1 : MonoBehaviour
{   
    public static player_tank1 Instance;
    float turnSpeed=100;
    float moveSpeed=3;
    bool turnRight;
    private float saniye_hesap;
    private float saniye_hesap1;
    private int harakat;
    private bool down;
    SpriteRenderer spriteRenderer;
    int mermi_hesap=4;
    const int child_bullet=4;
    public Animator animator;
    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite=other_setting_tank.Instance._tanklar_sprite[PlayerPrefs.GetInt("tank1",0)];//tank sprite sec
        animator.enabled = false;
    }
    void Update()
    {
        if (game_scripts.Instance.stopTime==true) return;

        // Tüm mermileri sırayla kontrol et ve aktif et
        for (int i = 0; i < child_bullet; i++) 
        {
            // Eğer mermi sayısı i'ye eşitse, aktif yap
            if (i < mermi_hesap) 
            {
                transform.GetChild(i).gameObject.SetActive(true);
            } 
            else 
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }

        saniye_hesap+= Time.deltaTime;
        if (mermi_hesap<4)//mermi doldurma islemi
        {
            if (saniye_hesap>=1f)
            {
                saniye_hesap=0f;
                mermi_hesap+=1;
            }
        }
        else{saniye_hesap=0f;}
        
        if (harakat==1)
        {
            if (down)
            {
                if (mermi_hesap>0)
                {
                    other_setting_tank.Instance.FireBullet(transform);// mermi at
                    mermi_hesap--;
                }
                turnRight = !turnRight;// dönüş yönünü değiştirme
                down=false;
            }
            saniye_hesap1+= Time.deltaTime;
            if (saniye_hesap1>=0.1f)
            {
                transform.Translate(Vector2.up * moveSpeed * Time.deltaTime);// i̇leri hareket
            }
        }
        else 
        {
            float rotation = turnSpeed * Time.deltaTime * (turnRight ? 1 : -1);// dönme
            transform.Rotate(0, 0, rotation);// dönme
            saniye_hesap1=0f;
        }
    }
    public void player1(int harakat_button)//button ayar
    {
        harakat=harakat_button;
    }
    public void button_down()
    {
        down=true;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag=="die" && enabled)
        {
            gameObject.layer = LayerMask.NameToLayer("wall");
            game_scripts.Instance.kaybeden_oyuncu(0);     
            enabled=false;//scripti kapatma
            animator.enabled = true;
            animator.SetBool("patlama",true);
            Destroy(gameObject,10);
            game_scripts.oyuncuSayisi--;//oyuncu sayisini azalt
        }
    }
}