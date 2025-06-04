using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class top : MonoBehaviour
{
    [SerializeField] Animator goal1,goal2;
    [SerializeField] GameObject patlama_animation;
    GameObject tosiq;
    [SerializeField] BoxCollider2D tosiq_collider1,tosiq_collider2;
    CircleCollider2D topCollider;
    Animator animator;
    void Start()
    {
        topCollider = GetComponent<CircleCollider2D>();
        animator = GetComponent<Animator>();
        Physics2D.IgnoreCollision(topCollider, tosiq_collider1, true);
        Physics2D.IgnoreCollision(topCollider, tosiq_collider2, true);
    }
    void Update()
    {
        if (!transform.parent) animator.SetBool("move",false);
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag=="Player1")
        {
            ParticleSystem particleSystem = patlama_animation.GetComponent<ParticleSystem>();
            patlama_animation.transform.position = transform.position;
            transform.position = transform.position;
            particleSystem.Play();

            transform.parent = other_script_football.Instance.transform; 
            transform.localPosition=new Vector3(0,0,100);
            GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;

            goal2.Play("goal");
            other_script_football.Instance.ballVoiceAndRestart(2);
        }
        else if (other.tag=="Player2")
        {
            ParticleSystem particleSystem = patlama_animation.GetComponent<ParticleSystem>();
            patlama_animation.transform.position = transform.position;
            transform.position = transform.position;
            particleSystem.Play();

            transform.parent = other_script_football.Instance.transform;
            transform.localPosition=new Vector3(0,0,100);
            GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;

            goal1.Play("goal");
            other_script_football.Instance.ballVoiceAndRestart(1);
        }
    }
}
