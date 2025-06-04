using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine.UIElements;

public class ML3_strike : Agent
{
    public GameObject[] life;
    public int life_count=3;
    public float saniye=2.5f;
    [SerializeField] Transform bullet_image;
    [SerializeField] Animator animator;
    bool durma;
    private bool hareket;
    private int basti;
    public float rayLength = 10f; // Raycast uzunligi
    public LayerMask eye;
    public override void OnEpisodeBegin()
    {

    }
    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(saniye);
        sensor.AddObservation(hareket ? 1f : 0f);
        sensor.AddObservation(durma ? 1f : 0f);

        float angleStep = 360f / 10;

        for (int i = 0; i < 10; i++)
        {
            float angle = i * angleStep;
            Vector2 direction = Quaternion.Euler(0, 0, angle) * transform.up;

            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, rayLength, eye);
            float distance = hit.collider != null ? 1 : 0;
            sensor.AddObservation(distance);

            // *** Debug.DrawRay yangilandi ***
            Color rayColor = hit.collider != null ? Color.green : Color.red;
            Debug.DrawRay(transform.position, direction * distance, rayColor, Time.deltaTime);    // **Tegilgan joy ko‘rinadi**
        }
    } 
    public override void OnActionReceived(ActionBuffers actions)
    {
        if (game_scripts.Instance.stopTime==true) return;

        int move = actions.DiscreteActions[0];
        basti=move;

        saniye+=Time.deltaTime;
        if (saniye>1.5f)
        {
            durma=false;
            if (basti==1 && saniye>=3f)
            {
                animator.Play("strike_player");
                StartCoroutine(ates());
                saniye=0f;
            }
            basti=0;
        }
        else {durma=true;}

        if (saniye>=30)
        {
            AddReward(-1);
            EndEpisode();
            saniye=0;
        }
        if (saniye>=3) 
        {
            bullet_image.gameObject.SetActive(true);
        }
        
        if (durma)
        {
            transform.Rotate(0, 0,0f);
        }
        else
        {
            transform.Rotate(0, 0,(hareket ? -3f : 3f) * 100 * Time.deltaTime);
        }
    }

    IEnumerator ates()
    {
        // Hozirgi animatsiya davomiyligini olish
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        float animationTime = stateInfo.length; // Animatsiyaning uzunligi

        yield return new WaitForSeconds(animationTime - 0.3f); // Animatsiya tugashini kutish

        hareket= !hareket;
        other_setting_strike.Instance.FireBullet(transform);// mermi at
        bullet_image.gameObject.SetActive(false);
        durma=true;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        life_count--;
        life[life_count].SetActive(false);

        if (life_count<=0)
        {
            game_scripts.oyuncuSayisi--; 
            gameObject.SetActive(false);
            game_scripts.Instance.kaybeden_oyuncu(2);//2==player3          
        }
        saniye=0f;
        animator.Play("strike_player_kayip");
        bullet_image.gameObject.SetActive(false);
        other_setting_strike.Instance.kaybetme();
    }
}