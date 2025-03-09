using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;

public class ML2_monster : Agent
{
    Animator animator;
    float rotationSpeed = 400f;
    float saniye;
    public LayerMask box_view,wall_view;
    public float rayLength = 10f;
    float action;
    void Start()
    {
        animator = transform.GetChild(0).GetComponent<Animator>();
        animator.SetBool("isMoving",true);
    }
    public override void OnEpisodeBegin()
    {
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        float angleStep = -90f / 10;

        for (int i = 0; i < 11; i++)
        {
            float angle = i * angleStep;
            Vector2 direction = Quaternion.Euler(0, 0, angle) * new Vector2(1,1);

            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, rayLength, box_view);
            float distance = hit.collider != null ? hit.distance : rayLength;
            sensor.AddObservation(distance);

            // *** Debug.DrawRay yangilandi ***
            Color rayColor = hit.collider != null ? Color.green : Color.red;
            Debug.DrawRay(transform.position, direction * distance, rayColor, Time.deltaTime);    // **Tegilgan joy ko‘rinadi**
        }
        
        angleStep = -180 / 10;

        for (int i = 0; i < 11; i++)
        {
            float angle = i * angleStep;
            Vector2 direction = Quaternion.Euler(0, 0, angle) * Vector2.up;

            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, rayLength, wall_view);
            float distance = hit.collider != null ? hit.distance : rayLength;
            sensor.AddObservation(distance);

            // *** Debug.DrawRay yangilandi ***
            Color rayColor = hit.collider != null ? Color.green : Color.red;
            Debug.DrawRay(transform.position, direction * distance, rayColor, Time.deltaTime);    // **Tegilgan joy ko‘rinadi**
        }
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        action = actions.ContinuousActions[0];
    }

    void Update()
    {
        if (game_scripts.Instance.stopTime==true) return;

        saniye+=Time.deltaTime;
        if (saniye<=2) return;

        transform.position+= new Vector3(0,action * 3 * Time.deltaTime);
        
        RotateCharacter();
    }

    void RotateCharacter()
    {
        // Hedef yönü hesapla
        float angle = Mathf.Atan2(action , 0.5f ) * Mathf.Rad2Deg;

        // Döndürme işlemini uygula
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> ContinuousActions = actionsOut.ContinuousActions;
        ContinuousActions[0] = Input.GetKey(KeyCode.A) ? 1.0f : Input.GetKey(KeyCode.D) ? -1.0f : 0.0f;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag=="Finish")
        {
            game_scripts.Instance.kazanan_oyuncu(1);
            monster_other_script.Instance.restart_game();
        }
    }
}
