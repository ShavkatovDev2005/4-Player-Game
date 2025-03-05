using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class daire : MonoBehaviour
{
    [System.Serializable]
    private class RotationElement
    {
        #pragma warning disable 0649
        public float Speed;
        #pragma warning restore 0649
    }
    [SerializeField]
    private RotationElement[] rotationPattern;

    bool minus;
    int intminus;
    int rotationIndex = 0;
    float saniye;
    int random;

    void Start()
    {
        random=UnityEngine.Random.Range(1,5);
    }
    void FixedUpdate()
    {
        if (game_scripts.Instance.stopTime==true) return;
        
        transform.GetChild(0).transform.Rotate(0,0,transform.GetChild(0).transform.rotation.z + rotationPattern[rotationIndex].Speed * (minus ? 1:-1));

        saniye+=Time.deltaTime;
        if (saniye>=random)
        {
            random=UnityEngine.Random.Range(1,4);
            rotationIndex=UnityEngine.Random.Range(0,rotationPattern.Length);
            intminus = UnityEngine.Random.Range(0,2);
            if (intminus==0) minus=!minus; 
            saniye=0;
        }
    }
}
