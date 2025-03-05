using Unity.VisualScripting;
using UnityEngine;


public class mermi : MonoBehaviour
{
    [SerializeField] GameObject patlama;
    private other_setting_tank other_setting_tank;
    void Start()
    {
        other_setting_tank = FindObjectOfType<other_setting_tank>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "box")
        {
            other.gameObject.SetActive(false);
        }
        other_setting_tank.Instance.bullet_touched_voice();
        Instantiate(patlama,transform.position,Quaternion.identity);//patlama effekt 
        Destroy(gameObject);  // Mermiyi yok et
    }
}