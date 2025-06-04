using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class player2_coin : MonoBehaviour
{
    public TextMeshProUGUI TmPro;
    Animator animator;

    float durma_saniyesi;
    void Start()
    {
        durma_saniyesi=3;
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        durma_saniyesi+=Time.deltaTime;
    }
    public void button_passed()
    {
        if (durma_saniyesi<3 || game_scripts.Instance.stopTime==true) return;

        animator.Play("player2");

        int A = SpriteBaraban.Instance.al();
        if (A==-1){
            transform.GetChild(0).GetComponent<Animator>().Play("A");
            durma_saniyesi=0;
        }
        else  coin_other_script.Instance.count2+=A;
        
        TmPro.text=coin_other_script.Instance.count2.ToString();
    }
}
