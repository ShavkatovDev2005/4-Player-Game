using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class run_player1 : MonoBehaviour
{
    Rigidbody2D rb;
    bool a;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = transform.GetChild(0).GetComponent<Animator>();
        rb= GetComponent<Rigidbody2D>(); 
        animator.SetBool("isRunning",true);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (game_scripts.Instance.stopTime==true) 
        {
            animator.SetBool("isRunning",false);
            return;
        }
        else animator.SetBool("isRunning",true);

        rb.gravityScale= a ? -2:2;
        animator.SetBool("reverse",a);
    }

    public void button()
    {
        a=!a;
        other_run_script.Instance.play_audioclip();
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag=="die")
        {
            Destroy(gameObject);
            game_scripts.oyuncuSayisi--;
            game_scripts.Instance.kaybeden_oyuncu(0);//0==player1          
        }
    }
}
