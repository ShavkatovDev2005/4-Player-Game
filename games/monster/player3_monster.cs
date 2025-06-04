using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player3_monster : MonoBehaviour
{
    [SerializeField] FixedJoystick fixedJoystick;
    Animator animator;
    float rotationSpeed = 400f;
    void Start()
    {
        animator = transform.GetChild(0).GetComponent<Animator>();
    }
    void Update()
    {
        if (game_scripts.Instance.stopTime==true) return;

        transform.position+= new Vector3(0,fixedJoystick.Vertical * 3 * Time.deltaTime);
        
        RotateCharacter();
    }
    void FixedUpdate()
    {
        if (game_scripts.Instance.stopTime==true) return;

        if (fixedJoystick.Horizontal == 0 && fixedJoystick.Vertical == 0)
        {
            transform.position+=new Vector3(-0.06f,0,0);
            animator.SetBool("isMoving",false);
        }
        else animator.SetBool("isMoving",true);
    }
    void RotateCharacter()
    {
        // Hareket doğrultusunda döndürme
        if (fixedJoystick.Horizontal != 0 || fixedJoystick.Vertical != 0)
        {
            // Hedef yönü hesapla
            float angle = Mathf.Atan2(fixedJoystick.Vertical,fixedJoystick.Horizontal) * Mathf.Rad2Deg;

            // Döndürme işlemini uygula
            Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag=="Finish")
        {
            game_scripts.Instance.kazanan_oyuncu(2);
            monster_other_script.Instance.restart_game();
        }
    }
}
