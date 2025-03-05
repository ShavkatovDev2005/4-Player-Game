using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;

public class ML2_snake : Agent
{
    public TrailRenderer trailRenderer;
    private EdgeCollider2D edgeCollider;
    private List<Vector2> trailPoints = new List<Vector2>();//EdgeCollider2D points
    float speed=3;
    float turnSpeed = 400;
    Animator animator;
    public LayerMask eye,foodEye;
    public float rayLength = 10f; // Raycast uzunligi
    [SerializeField] FixedJoystick fixedJoystick;
    bool move;
    float movex,movey;
    void Start()
    {
        animator = GetComponent<Animator>();
        trailRenderer = transform.GetChild(0).GetComponent<TrailRenderer>();
        edgeCollider = transform.GetChild(0).GetComponent<EdgeCollider2D>();
        edgeCollider.isTrigger = true; // Trigger qilib qo'yamiz
    }
    public override void OnEpisodeBegin()
    {
    }
    public override void CollectObservations(VectorSensor sensor)
    {
        float angleStep = 360 / 10;

        for (int i = 0; i < 10; i++)
        {
            float angle = i * angleStep;
            Vector2 direction = Quaternion.Euler(0, 0, angle) * transform.up;

            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, rayLength, eye);
            float distance = hit.collider != null ? hit.distance : rayLength;

            sensor.AddObservation(distance);

            // *** Debug.DrawRay yangilandi ***
            Color rayColor = hit.collider != null ? Color.green : Color.red;
            Debug.DrawRay(transform.position, direction * distance, rayColor, Time.deltaTime);    // **Tegilgan joy ko‘rinadi**
        }
        for (int i = 0; i < 10; i++)
        {
            float angle = i * angleStep;
            Vector2 direction = Quaternion.Euler(0, 0, angle) * transform.up;

            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, rayLength, foodEye);
            float distance = hit.collider != null ? hit.distance : rayLength;

            sensor.AddObservation(distance);

            // *** Debug.DrawRay yangilandi ***
            Color rayColor = hit.collider != null ? Color.blue : Color.red;
            Debug.DrawRay(transform.position, direction * distance, rayColor, Time.deltaTime);    // **Tegilgan joy ko‘rinadi**
        }
    }
    public override void OnActionReceived(ActionBuffers actions)
    {
        if (game_scripts.Instance.stopTime==true) 
        {
            animator.SetBool("isMoving",false);
            return;
        }

        if (move) animator.SetBool("isMoving",true);
        else animator.SetBool("isMoving",false);

        movex = actions.ContinuousActions[0];
        movey = actions.ContinuousActions[1];

        RotateCharacter();
        UpdateCollider();
    }
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> discreteActions = actionsOut.ContinuousActions;
        discreteActions[0] = fixedJoystick.Horizontal;
        discreteActions[1] = fixedJoystick.Vertical;
    }
    void RotateCharacter()
    {
        transform.position+= transform.right * speed * Time.deltaTime * (move? 1:0) ;
        // Hareket doğrultusunda döndürme
        if (movex != 0 || movey != 0)
        {
            // Hedef yönü hesapla
            float angle = Mathf.Atan2(movey,movex) * Mathf.Rad2Deg;

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
