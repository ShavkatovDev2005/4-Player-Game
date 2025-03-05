using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class strike_arrow_player1 : MonoBehaviour
{
    public int maxHealth=100;
    public int currentHealth;
    public healthbar healthbar;
    [SerializeField] FixedJoystick fixedJoystick;
    [SerializeField] Animator animator;
    [SerializeField] GameObject bulletObject;
    float rotationSpeed = 400f;
    bool bullet;
    bool is_dead;
    bool ates;
    void Start()
    {
        currentHealth=maxHealth;
        healthbar.setMaxHealth(maxHealth);
    }
    void Update()
    {
        if (is_dead || game_scripts.Instance.stopTime==true) return;
        if (bullet)
        {
            bulletObject.SetActive(true);
        }
        else 
        {
            bulletObject.SetActive(false);
        }
        
        transform.position+= new Vector3(fixedJoystick.Horizontal * 3 * Time.deltaTime,fixedJoystick.Vertical * 3 * Time.deltaTime);
        RotateCharacter();
        if (fixedJoystick.Horizontal == 0 && fixedJoystick.Vertical == 0 && ates && bullet)
        {
            other_setting_arrow.Instance.FireBullet(transform);
            bullet = false;
            ates=false;
        }
    }
    void takeDamage(int damage)
    {
        currentHealth-=damage;
        healthbar.setHealth(currentHealth);

        if (currentHealth<=0)
        {
            game_scripts.Instance.kaybeden_oyuncu(0);//0==player1         
            if (game_scripts.oyuncuSayisi>2 && !is_dead)
            {
                game_scripts.oyuncuSayisi--;
                is_dead=true;
                Destroy(gameObject);
            }
            else 
            {
                other_setting_arrow.Instance.restart_game();
                Destroy(gameObject);
            }
        }
    }
    void restoreDamage(int damage)
    {
        currentHealth+=damage;
        healthbar.setHealth(currentHealth);
    }
    void RotateCharacter()
    {
        // Hareket doğrultusunda döndürme
        if (fixedJoystick.Horizontal != 0 || fixedJoystick.Vertical != 0)
        {
            animator.SetBool("isMoving", true);
            ates=true;
            // Hedef yönü hesapla
            float angle = Mathf.Atan2(fixedJoystick.Vertical,fixedJoystick.Horizontal) * Mathf.Rad2Deg;

            // Döndürme işlemini uygula
            Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
        else{
            animator.SetBool("isMoving", false);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag=="Mermi")
        {
            takeDamage(25);
        }
        else if (other.gameObject.tag=="jon")
        {
            restoreDamage(10);
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag=="bullet")
        {
            bullet=true;
            Destroy(other.gameObject);
        }
    }
}
