using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Timeline;

public class snike2_karakter : MonoBehaviour
{
    public TrailRenderer trailRenderer;
    private EdgeCollider2D edgeCollider;
    private List<Vector2> trailPoints = new List<Vector2>();//EdgeCollider2D points
    float speed=3;
    float turnSpeed = 400;
    Animator animator;
    [SerializeField] FixedJoystick fixedJoystick;
    bool move;
    void Start()
    {
        animator = GetComponent<Animator>();
        trailRenderer = transform.GetChild(0).GetComponent<TrailRenderer>();
        edgeCollider = transform.GetChild(0).GetComponent<EdgeCollider2D>();
        edgeCollider.isTrigger = true; // Trigger qilib qo'yamiz
    }
    void Update()
    {
        if (game_scripts.Instance.stopTime==true) 
        {
            animator.SetBool("isMoving",false);
            return;
        }

        if (move) animator.SetBool("isMoving",true);
        else animator.SetBool("isMoving",false);

        RotateCharacter();
        UpdateCollider();
    }
    void RotateCharacter()
    {
        transform.position+= transform.right * speed * Time.deltaTime * (move? 1:0) ;
        // Hareket doğrultusunda döndürme
        if (fixedJoystick.Horizontal != 0 || fixedJoystick.Vertical != 0)
        {
            // Hedef yönü hesapla
            float angle = Mathf.Atan2(fixedJoystick.Vertical,fixedJoystick.Horizontal) * Mathf.Rad2Deg;

            // Döndürme işlemini uygula
            Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
            move=true;
        }
    }

    void UpdateCollider()
    {
        trailPoints.Clear();
        int pointCount = trailRenderer.positionCount;

        if (pointCount < 2) return; // Kamida 2 ta nuqta bo'lishi kerak

        for (int i = 0; i < pointCount; i++)
        {
            Vector3 worldPoint = trailRenderer.GetPosition(i); // Dunyo koordinatasi
            Vector2 localPoint = transform.GetChild(0).transform.InverseTransformPoint(worldPoint); // Lokal koordinataga o‘tkazamiz
            trailPoints.Add(localPoint);
        }

        edgeCollider.SetPoints(trailPoints);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag=="wall")
        {
            gameObject.SetActive(false);
            game_scripts.oyuncuSayisi--;
            game_scripts.Instance.kaybeden_oyuncu(1);//1==player2          
            other_setting_snike.Instance.destroy_audio();
        }
        else if (other.tag=="jon")
        {
            other_setting_snike.Instance.take_audio();
            if (trailRenderer.time<=10) trailRenderer.time+=0.25f;
        }
    }
}
