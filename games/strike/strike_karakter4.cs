using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class strike_karakter4 : MonoBehaviour
{
    public GameObject[] life;
    public int life_count=3;
    public float saniye=2.5f;
    [SerializeField] Transform bullet_image;
    [SerializeField] Animator animator;
    bool durma;
    private bool hareket;
    private int basti;
    void Update()
    {
        if (game_scripts.Instance.stopTime==true) return;

        saniye+=Time.deltaTime;
        if (saniye>1.5f)
        {
            durma=false;
            if (basti==1 && saniye>=3f)
            {
                animator.Play("strike_player");
                StartCoroutine(ates());
                saniye=0f;
            }
            basti=0;
        }
        else {durma=true;}
    }
    IEnumerator ates()
    {
        // Hozirgi animatsiya davomiyligini olish
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        float animationTime = stateInfo.length; // Animatsiyaning uzunligi

        yield return new WaitForSeconds(animationTime - 0.3f); // Animatsiya tugashini kutish

        if (saniye>=animationTime - 0.3f)
        {
            hareket= !hareket;
            other_setting_strike.Instance.FireBullet(transform);// mermi at
            bullet_image.gameObject.SetActive(false);
            durma=true;
        }
    }

    void FixedUpdate()
    {
        if (game_scripts.Instance.stopTime==true) return;

        if (saniye>=3) 
        {
            bullet_image.gameObject.SetActive(true);
        }
        
        if (durma)
        {
            transform.Rotate(0, 0,0f);
        }
        else
        {
            transform.Rotate(0, 0, hareket ? -8f : 8f);
        }
    }
    public void button()
    {
        basti=1;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        life_count--;
        life[life_count].SetActive(false);

        if (life_count<=0)
        {
            game_scripts.oyuncuSayisi--; 
            gameObject.SetActive(false);
            game_scripts.Instance.kaybeden_oyuncu(3);//3==player4          
        }
        saniye=0f;
        animator.Play("strike_player_kayip");
        bullet_image.gameObject.SetActive(false);
        other_setting_strike.Instance.kaybetme();
    }
}