using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;

public class ML3_bombadan_kac : Agent
{
    [SerializeField] Animator animator;
    Rigidbody2D rb;
    float rotationSpeed = 400f;
    float movex,movey;
    public LayerMask detectableLayers;
    public float rayLength = 10f;
    public int rayCount = 12;
    public override void Initialize()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    // public override void OnEpisodeBegin()
    // {
    //     float x =Random.Range(-400,220);
    //     float y =Random.Range(-300,300);
    //     transform.localPosition = new Vector3(x,y,0);
    // }
    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.GetChild(0).gameObject.activeSelf ? 1:0);
        sensor.AddObservation(new Vector2 (transform.localPosition.x,transform.localPosition.y).normalized);

        for (int i = 0; i < 4; i++) //there have 6 observation 
        {
            if (bombadan_kac_other_script.bombadan_Kac_Other_Script.activeList[i].activeSelf && 
            bombadan_kac_other_script.bombadan_Kac_Other_Script.activeList[i] != gameObject)
            {
                Vector2 V2 = bombadan_kac_other_script.bombadan_Kac_Other_Script.activeList[i].transform.localPosition;
                sensor.AddObservation(V2.normalized);
            }
            else if (bombadan_kac_other_script.bombadan_Kac_Other_Script.activeList[i] != gameObject) sensor.AddObservation(Vector2.zero);
        }

        float angleStep = 360f / rayCount;

        for (int i = 0; i < rayCount; i++)
        {
            float angle = i * angleStep;
            Vector2 direction = Quaternion.Euler(0, 0, angle) * transform.up;

            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, rayLength, detectableLayers);
            float distance = hit.collider != null ? hit.distance : rayLength;
            sensor.AddObservation(distance);

            Color rayColor = hit.collider != null ? Color.green : Color.red;
            Debug.DrawRay(transform.position, direction * distance, rayColor, Time.deltaTime);
        }
    }
    public override void OnActionReceived(ActionBuffers actions)
    {
        movex = actions.ContinuousActions[0];
        movey = actions.ContinuousActions[1];

        if (game_scripts.Instance.stopTime==true) return;


        Vector2 moveDirection = new Vector2(movex, movey);
        if (moveDirection.magnitude > 0.1f) // Agar joystick harakat qilayotgan bo‘lsa
        {
            rb.AddForce(moveDirection * 0.08f * Time.deltaTime);
        }
        
        if (gameObject.layer == LayerMask.NameToLayer("tagger")) transform.GetChild(0).gameObject.SetActive(true);
        else transform.GetChild(0).gameObject.SetActive(false);
        
        RotateCharacter(); 
    }
    // public override void Heuristic(in ActionBuffers actionsOut)
    // {
    //     ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
    //     continuousActions[0] = fixedJoystick.Horizontal;
    //     continuousActions[1] = fixedJoystick.Vertical;
    // }
    void RotateCharacter()
    {
        // Hareket doğrultusunda döndürme
        if (movex != 0 || movey != 0)
        {
            // Hedef yönü hesapla
            float angle = Mathf.Atan2(movey,movex) * Mathf.Rad2Deg;

            // Döndürme işlemini uygula
            Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            animator.SetBool("isMoving",true);
        }
        else animator.SetBool("isMoving",false);
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (!transform.GetChild(0).gameObject.activeSelf)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("tagger"))
            {
                gameObject.layer = LayerMask.NameToLayer("tagger");
                other.gameObject.layer = LayerMask.NameToLayer(other.gameObject.tag);
            }
        }
    }
}
