using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;

public class ML3_football : Agent
{
    float rotationSpeed = 400f;
    bool shoot;
    bool topu_alabilirsin = true;
    float movex,movey,kick;
    [SerializeField] GameObject top;
    Animator animator;
    void Start()
    {
        animator = transform.GetChild(0).GetComponent<Animator>();
    }
    public override void OnEpisodeBegin()
    {

    }
    public override void CollectObservations(VectorSensor sensor)
    {

        sensor.AddObservation(shoot);
        sensor.AddObservation(topu_alabilirsin);
        sensor.AddObservation(kick);
        sensor.AddObservation((top.transform.localPosition - transform.localPosition).normalized);
    }
    public override void OnActionReceived(ActionBuffers actions)
    {
        movex = actions.ContinuousActions[0];
        movey = actions.ContinuousActions[1];
        kick = actions.DiscreteActions[0];
    }
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        // ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
        // ActionSegment<int> discreteActions = actionsOut.DiscreteActions;
        // continuousActions[0] = fixedJoystick.Horizontal;
        // continuousActions[1] = fixedJoystick.Vertical;
        // discreteActions[0] = Input.GetKey(KeyCode.W) ? 1:0;
    }

    void play_animator()
    {
        if (movex == 0 && movey == 0)
        {
            animator.SetBool("isMoving", false);
            if (transform.childCount==2)
            {
                Animator A = transform.GetChild(1).gameObject.GetComponent<Animator>();
                A.SetBool("move",false);
            }
        }
        else // animator calistir
        {
            float speed = new Vector2(movex, movey).magnitude;
            animator.speed = speed;
            animator.SetBool("isMoving", true);

            if (transform.childCount==2)
            {
                Animator A = transform.GetChild(1).gameObject.GetComponent<Animator>();
                A.speed = speed;
                A.SetBool("move",true);
            }
        }
    }
    void Update()
    {
        if (other_script_football.Instance.waitingTime<=2 || game_scripts.Instance.stopTime==true) return;

        move();
        RotateCharacter();
        Shoot();
    }
    void move()
    {
        if (movex != 0 || movey != 0) 
        {
            transform.position+= new Vector3(movex * 3 * Time.deltaTime, movey * 3 * Time.deltaTime);
            animator.SetBool("isMoving",true);
        }
        else animator.SetBool("isMoving",false);
    }
    void Shoot()
    {
        if (kick == 1 && transform.childCount==2 && shoot)
        {
            AddReward(1f);
            // Rotationdan yo'nalishni hisoblash
            float angle = transform.eulerAngles.z; // Z o'qi bo'yicha burchak
            Vector2 direction = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
            
            float forceAmount = 0.0015f; // Kuch miqdori

            transform.GetChild(1).gameObject.GetComponent<Rigidbody2D>().bodyType=RigidbodyType2D.Dynamic;
            // Kuchni qo'llash
            transform.GetChild(1).gameObject.GetComponent<Rigidbody2D>().AddForce(direction * forceAmount, ForceMode2D.Impulse);
            string A = tag.ToString();

            transform.GetChild(1).SetParent(transform.parent);
            // other_script_football.Instance.voice();//ses
            shoot=false;
        }
    }
    void RotateCharacter()
    {
        if (movex != 0 || movey != 0)
        {
            // Hedef yönü hesapla
            float angle = Mathf.Atan2(movey,movex) * Mathf.Rad2Deg;

            // Döndürme işlemini uygula
            Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            shoot=true;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag=="ball" && topu_alabilirsin)
        {
            other.gameObject.transform.SetParent(transform);
            other.gameObject.transform.localPosition=new Vector3(0.8f,0,other.gameObject.transform.localPosition.z);

            // Har qanday mavjud tezlik va aylanishni to‘xtatish
            Rigidbody2D rb = other.gameObject.GetComponent<Rigidbody2D>();
            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
            topu_alabilirsin = false;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag=="ball") topu_alabilirsin = true;
    }
}
