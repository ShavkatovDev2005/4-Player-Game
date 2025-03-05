using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class box : MonoBehaviour
{
    // Update is called once per frame
    void FixedUpdate()
    {
        if (game_scripts.Instance.stopTime==true) return;

        transform.position+=new Vector3(-0.06f,0,0);
    }
}
