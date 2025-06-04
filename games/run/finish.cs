using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class finish : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag=="Player1")
        {
            game_scripts.Instance.kazanan_oyuncu(0);
            game_scripts.Instance.RestartSceneOnClick(other_run_script.Instance.transform);
        }
        else if (other.gameObject.tag=="Player2")
        {
            game_scripts.Instance.kazanan_oyuncu(1);
            game_scripts.Instance.RestartSceneOnClick(other_run_script.Instance.transform);
        }
        else if (other.gameObject.tag=="Player3")
        {
            game_scripts.Instance.kazanan_oyuncu(2);
            game_scripts.Instance.RestartSceneOnClick(other_run_script.Instance.transform);
        }
        else if (other.gameObject.tag=="Player4")
        {
            game_scripts.Instance.kazanan_oyuncu(3);
            game_scripts.Instance.RestartSceneOnClick(other_run_script.Instance.transform);
        }
    }
}
