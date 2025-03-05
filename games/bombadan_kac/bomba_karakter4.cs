using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class bomba_karakter4 : MonoBehaviour
{
    [SerializeField] FixedJoystick fixedJoystick;
    [SerializeField] Animator animator;
    public static bool bomba_bende;
    float rotationSpeed = 400f;

    void Start()
    {
        if (bombadan_kac_other_script.oyuncu_sec==4)
        {
            bomba_bende=true;
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (game_scripts.Instance.stopTime==true) return;
        
        bomba_bende=transform.GetChild(0).gameObject.activeSelf;

        transform.position+= new Vector3(fixedJoystick.Horizontal * 3 * Time.deltaTime,fixedJoystick.Vertical * 3 * Time.deltaTime);
        RotateCharacter();
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
            animator.SetBool("isMoving",true);
        }
        else animator.SetBool("isMoving",false);
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag=="Player" && bomba_bende)
        {
            transform.GetChild(0).gameObject.SetActive(false);
            other.transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}
