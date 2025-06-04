using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class food : MonoBehaviour
{
    [SerializeField] BoxCollider2D boxCollider2D;
    // Start is called before the first frame update
    void Start()
    {
        randomizePosition();
    }
    void randomizePosition()
    {
        Bounds bounds = this.boxCollider2D.bounds;

        float x = UnityEngine.Random.Range(bounds.min.x,bounds.max.x);
        float y = UnityEngine.Random.Range(bounds.min.y,bounds.max.y);

        this.transform.position = new Vector3(Mathf.Round(x),Mathf.Round(y),transform.position.z);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        randomizePosition();
    }
}
