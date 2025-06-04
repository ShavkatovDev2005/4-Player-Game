using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
public class top_bullet : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;

    void Update()
    {
        if (game_scripts.Instance.stopTime==true) Destroy(gameObject);

        if (rb.linearVelocity.magnitude <= 2.5f) rb.linearVelocity = rb.linearVelocity.normalized * 3f;  // Merminin hareket hızı
    }
    
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag=="box")
        {
            Destroy(gameObject);
        }
        else other_script_topOyunu.other_Script_TopOyunu.play_knock_voice();
    }
}