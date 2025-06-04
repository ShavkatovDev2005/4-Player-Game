using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;

public class ML2_duel : Agent
{
    [SerializeField] GameObject[] bullet;
    public float turnSpeed;
    bool turnRight;
    int bullet_sayac=4;
    float saniye_hesap;
    public LayerMask player;
    float rayLength = 5f;
    bool ates;
    public override void OnEpisodeBegin()
    {

    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(bullet_sayac);
        sensor.AddObservation(turnRight);

        float angleStep = 360 / 10;

        for (int i = 0; i < 15; i++)
        {
            float angle = i * angleStep;
            Vector2 direction = Quaternion.Euler(0, 0, angle) * transform.up;

            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, rayLength, player);
            float distance = hit.collider != null ? hit.distance : rayLength;
            sensor.AddObservation(distance);

            // *** Debug.DrawRay yangilandi ***
            Color rayColor = hit.collider != null ? Color.green : Color.white;
            Debug.DrawRay(transform.position, direction * distance, rayColor, Time.deltaTime);    // **Tegilgan joy ko‘rinadi**
        }
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        int fire = actions.DiscreteActions[0];

        if (fire == 1 && ates)
        {
            ates = false;
            play();
        }
        else if (fire == 0)
        {
            ates = true;
        }
    }

    void play()
    {
        turnRight=!turnRight;
        if (bullet_sayac>0)
        {
            duel_other_setting.Instance.FireBullet(transform, turnRight);
            bullet_sayac--;
        }
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<int> discreteActions = actionsOut.DiscreteActions;
        discreteActions[0] = Input.GetKey(KeyCode.W) ? 1 : 0;
    }


    void Update()
    {
        if (game_scripts.Instance.stopTime==true) return;

        float rotation = turnSpeed * Time.deltaTime * (turnRight ? 1 : -1);// dönme
        transform.Rotate(0, 0, rotation);// dönme
        transform.localPosition=new Vector3(0 ,2.4f,0);
        // Tüm mermileri sırayla kontrol et ve aktif et
        for (int i = 0; i < bullet.Length; i++) 
        {
            // Eğer mermi sayısı i'ye eşitse, aktif yap
            if (i < bullet_sayac) 
            {
                bullet[i].SetActive(true);
            } 
            else 
            {
                bullet[i].SetActive(false);
            }
        }
        saniye_hesap+= Time.deltaTime;
        if (bullet_sayac<=0)
        {
            if (saniye_hesap>=3f)
            {
                saniye_hesap=0f;
                bullet_sayac=4;
                duel_other_setting.Instance.bullet_full_voice();
            }
        }
        else{saniye_hesap=0f;}
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        turnRight=!turnRight;
    }
}
