using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;

public class ML2_top_oyunu : Agent
{
    public TextMeshProUGUI life_count_text;
    public int life_count=3;
    SpriteRenderer color;
    BoxCollider2D colider;
    bool hareket;
    bool oldu=false;
    public LayerMask eye;
    public float rayLength = 10f; // Raycast uzunligi
    void Start()
    {
        color = transform.GetChild(1).GetComponent<SpriteRenderer>();
        colider = GetComponent<BoxCollider2D>();
    }

    public override void OnEpisodeBegin()
    {

    }
    public override void CollectObservations(VectorSensor sensor)
    {
        if (game_scripts.Instance.stopTime==true || oldu) return;

        float angleStep = -180 / 10;

        for (int i = 0; i < 11; i++)
        {
            float angle = i * angleStep;
            Vector2 direction = Quaternion.Euler(0, 0, angle) * transform.GetChild(1).up;

            RaycastHit2D hit = Physics2D.Raycast(transform.GetChild(0).position, direction, rayLength, eye);
            float distance = hit.collider != null ? hit.distance : rayLength;
            sensor.AddObservation(distance);

            // *** Debug.DrawRay yangilandi ***
            Color rayColor = hit.collider != null ? Color.green : Color.red;
            Debug.DrawRay(transform.GetChild(0).position, direction * distance, rayColor, Time.deltaTime);    // **Tegilgan joy ko‘rinadi**
        }
    }
    public override void OnActionReceived(ActionBuffers actions)
    {
        int move = actions.DiscreteActions[0];

        if (game_scripts.Instance.stopTime==true || oldu) return;

        if (move==1) hareket=true;
        else hareket=false;

        transform.GetChild(0).transform.position+=new Vector3(0, (hareket ? 3f:-3f)*Time.deltaTime, 0);
    }
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<int> discreteActions = actionsOut.DiscreteActions;
        discreteActions[0] = Input.GetKey(KeyCode.W) ? 1 : 0;
    }
    void count()
    {
        life_count--;
        life_count_text.text = life_count.ToString();
        if (life_count<=0)
        {
            Destroy(transform.GetChild(0).gameObject);
            oldu = true;
            color.color = new Color(1f, 0f, 0f, 0.5f);
            colider.isTrigger = false;
            game_scripts.oyuncuSayisi--;
            game_scripts.Instance.kaybeden_oyuncu(1);//1==player2         
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag=="ball" && life_count>=1)
        {
            count();
        }
    }
}
