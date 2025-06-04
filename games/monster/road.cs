using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class road : MonoBehaviour
{
    void FixedUpdate()
    {
        if (game_scripts.Instance.stopTime==true) return;

        transform.position+= new Vector3(-0.06f,0,0);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag=="Respawn")
        {
            GameObject A = Instantiate(gameObject,transform.parent.transform);
            A.transform.localPosition= new Vector3(1921,0,400);
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag=="Respawn")
        {
            Destroy(gameObject);
        }
    }
}
