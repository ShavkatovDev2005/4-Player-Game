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

        if (rb.velocity.magnitude <= 2.5f) rb.velocity = rb.velocity.normalized * 3f;  // Merminin hareket hızı
    }
    
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag=="box")
        {
            Destroy(gameObject);
        }
    }
}