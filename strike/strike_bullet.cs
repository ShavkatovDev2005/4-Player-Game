using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class strike_bullet : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject);
    }
}
