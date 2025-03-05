using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class player4_coin : MonoBehaviour
{
    public static player4_coin Instance;
    public TextMeshProUGUI TmPro;
    public int count;
    Animator animator;
    float durma_saniyesi;
    void Awake()
    {
        Instance = this;
    }
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
        if (durma_saniyesi<3) return;

        animator.Play("player4");

        int A = SpriteBaraban.Instance.al();
        if (A==-1){
            transform.GetChild(0).GetComponent<Animator>().Play("A");
            durma_saniyesi=0;
        }
        else  count+=A;

        TmPro.text=count.ToString();
    }
}
