using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using Unity.Burst.Intrinsics;

public class ML3_tank : Agent
{
    private float turnSpeed = 100f;
    private float moveSpeed = 3f;
    private bool turnRight;
    private int mermi_hesap = 4;
    private float saniye_hesap1,saniye_hesap;
    public LayerMask detectableLayers,player;
    public float rayLength = 10f;
    public int rayCount = 10;
    private bool down = false;
    private int action;

    public static ML3_tank Instance;
    SpriteRenderer spriteRenderer;
    const int child_bullet=4;
    public Animator animator;
    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite=other_setting_tank.Instance._tanklar_sprite[PlayerPrefs.GetInt("tank3",2)];//tank sprite sec
        animator.enabled = false;
    }

    public override void OnEpisodeBegin()
    {
        // // O'yin boshida agentni qayta joylash
        // transform.localPosition = new Vector3(-500,-300,0);
        // transform.localRotation = new Quaternion(0,0,-90,0);
        // mermi_hesap = 4;
        // down = false;
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        float angleStep = 360f / rayCount;

        for (int i = 0; i < rayCount; i++)
        {
            float angle = i * angleStep;
            Vector2 direction = Quaternion.Euler(0, 0, angle) * transform.up;

            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, rayLength, detectableLayers);
            float distance = hit.collider != null ? hit.distance : rayLength;
            sensor.AddObservation(distance);

            // *** Debug.DrawRay yangilandi ***
            Color rayColor = hit.collider != null ? Color.green : Color.red;
            Debug.DrawRay(transform.position, direction * distance, rayColor, Time.deltaTime);    // **Tegilgan joy ko‘rinadi**
        }
        for (int i = 0; i < rayCount; i++)
        {
            float angle = i * angleStep;
            Vector2 direction = Quaternion.Euler(0, 0, angle) * transform.up;

            RaycastHit2D bir = Physics2D.Raycast(transform.position, direction, rayLength, player);
            float distance = bir.collider != null ? bir.distance : rayLength;
            sensor.AddObservation(distance);

            // *** Debug.DrawRay yangilandi ***
            Color rayColor = bir.collider != null ? Color.red : Color.green;
            Debug.DrawRay(transform.position, direction * distance, rayColor, Time.deltaTime);    // **Tegilgan joy ko‘rinadi**
        }


        // Qo‘shimcha observatsiyalar
        sensor.AddObservation(down ? 1f : 0f);
        sensor.AddObservation(mermi_hesap);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        if (game_scripts.Instance.stopTime==true) return;

        action = actions.DiscreteActions[0];

        // Tüm mermileri sırayla kontrol et ve aktif et
        for (int i = 0; i < child_bullet; i++) 
        {
            // Eğer mermi sayısı i'ye eşitse, aktif yap
            if (i < mermi_hesap) 
            {
                transform.GetChild(i).gameObject.SetActive(true);
            } 
            else 
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }

        if (action==1)
        {
            saniye_hesap1+= Time.deltaTime;
            if (saniye_hesap1>=0.1f) transform.Translate(Vector2.up * moveSpeed * action * Time.deltaTime);// i̇leri hareket
            if (down)
            {
                if (mermi_hesap>0)
                {
                    other_setting_tank.Instance.FireBullet(transform);// mermi at
                    mermi_hesap--;
                }
                turnRight = !turnRight;// dönüş yönünü değiştirme
                down=false;
            }
        }
        else 
        {
            float rotation = turnSpeed * Time.deltaTime * (turnRight ? 1 : -1);// dönme
            transform.Rotate(0, 0, rotation);// dönme
            saniye_hesap1=0f;
            down=true;
        }

        saniye_hesap+= Time.deltaTime;
        if (mermi_hesap<4)//mermi doldurma islemi
        {
            if (saniye_hesap>=1f)
            {
                saniye_hesap=0f;
                mermi_hesap+=1;
            }
        }
        else{saniye_hesap=0f;}
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<int> discreteActions = actionsOut.DiscreteActions;
        discreteActions[0] = Input.GetKey(KeyCode.W) ? 1 : 0;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag=="die" && enabled)
        {
            gameObject.layer = LayerMask.NameToLayer("wall");
            game_scripts.Instance.kaybeden_oyuncu(2);     
            enabled=false;//scripti kapatma
            animator.enabled = true;
            animator.SetBool("patlama",true);
            Destroy(gameObject,10);
            game_scripts.oyuncuSayisi--;//oyuncu sayisini azalt
        }
    }
}