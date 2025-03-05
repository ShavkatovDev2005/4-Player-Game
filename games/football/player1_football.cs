using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player1_football : MonoBehaviour
{
    public static player1_football Instance;
    [SerializeField] FixedJoystick fixedJoystick;
    Animator animator;
    float rotationSpeed = 400f;
    bool shoot;
    bool topu_alabilirsin = true;
    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        animator = transform.GetChild(0).GetComponent<Animator>();
    }
    void Update()
    {
        if (other_script_football.Instance.waitingTime<=2 || game_scripts.Instance.stopTime==true) return;
        
        move();
        RotateCharacter();
        Shoot();
        play_animator();
    }
    void play_animator()
    {
        if (fixedJoystick.Horizontal == 0 && fixedJoystick.Vertical == 0)
        {
            animator.SetBool("isMoving", false);
            if (transform.childCount==2)
            {
                Animator A = transform.GetChild(1).gameObject.GetComponent<Animator>();
                A.SetBool("move",false);
            }
        }
        else // animator calistir
        {
            float speed = new Vector2(fixedJoystick.Horizontal, fixedJoystick.Vertical).magnitude;
            animator.speed = speed;
            animator.SetBool("isMoving", true);

            if (transform.childCount==2)
            {
                Animator A = transform.GetChild(1).gameObject.GetComponent<Animator>();
                A.speed = speed;
                A.SetBool("move",true);
            }
        }
    }
    void move()
    {
        if (fixedJoystick.Horizontal != 0 || fixedJoystick.Vertical != 0) 
        {
            transform.position+= new Vector3(fixedJoystick.Horizontal * 3 * Time.deltaTime,fixedJoystick.Vertical * 3 * Time.deltaTime);
            animator.SetBool("isMoving",true);
        }
        else animator.SetBool("isMoving",false);
    }
    void Shoot()
    {
        if (fixedJoystick.Horizontal == 0 && fixedJoystick.Vertical == 0 && transform.childCount==2 && shoot)
        {
            // Rotationdan yo'nalishni hisoblash
            float angle = transform.eulerAngles.z; // Z o'qi bo'yicha burchak
            Vector2 direction = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
            
            float forceAmount = 0.0015f; // Kuch miqdori

            transform.GetChild(1).gameObject.GetComponent<Rigidbody2D>().bodyType=RigidbodyType2D.Dynamic;
            // Kuchni qo'llash
            transform.GetChild(1).gameObject.GetComponent<Rigidbody2D>().AddForce(direction * forceAmount, ForceMode2D.Impulse);

            transform.GetChild(1).SetParent(null);
            other_script_football.Instance.voice();//ses
            shoot=false;
        }
    }
    void RotateCharacter()
    {
        if (fixedJoystick.Horizontal != 0 || fixedJoystick.Vertical != 0)
        {
            // Hedef yönü hesapla
            float angle = Mathf.Atan2(fixedJoystick.Vertical,fixedJoystick.Horizontal) * Mathf.Rad2Deg;

            // Döndürme işlemini uygula
            Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            shoot=true;
        }
    }



    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag=="ball" && topu_alabilirsin)
        {
            other.gameObject.transform.SetParent(transform);
            other.gameObject.transform.localPosition=new Vector3(0.8f,0,other.gameObject.transform.localPosition.z);

            // Har qanday mavjud tezlik va aylanishni to‘xtatish
            Rigidbody2D rb = other.gameObject.GetComponent<Rigidbody2D>();
            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;
            topu_alabilirsin = false;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag=="ball") topu_alabilirsin = true;
    }
}
