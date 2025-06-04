using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;
public class ML4_coin : Agent
{
    public TextMeshProUGUI TmPro;
    [SerializeField] Animator animator;
    float durma_saniyesi;
    private bool ates;

    void Start()
    {
        durma_saniyesi=3;
    }

    void Update()
    {
        durma_saniyesi+=Time.deltaTime;
    }

    public override void OnEpisodeBegin()
    {
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(SpriteBaraban.Instance.hangi_spritede_durdu);
        sensor.AddObservation(SpriteBaraban.Instance.forObservations() == -1  ?  1: SpriteBaraban.Instance.forObservations() > 0 ? 2 : 3 );
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        int fire = actions.DiscreteActions[0];
        durma_saniyesi+=Time.deltaTime;

        if (game_scripts.Instance.stopTime==true) return;

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

    public void play()
    {
        if (durma_saniyesi<3) return;

        animator.Play("player4");

        int A = SpriteBaraban.Instance.al();
        if (A==-1){
            transform.GetChild(0).GetComponent<Animator>().Play("A");
            durma_saniyesi=0;
        }
        else  coin_other_script.Instance.count4+=A;
        
        TmPro.text=coin_other_script.Instance.count4.ToString();
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<int> discreteActions = actionsOut.DiscreteActions;
        discreteActions[0] = Input.GetKey(KeyCode.W) ? 1 : 0;
    }
}
