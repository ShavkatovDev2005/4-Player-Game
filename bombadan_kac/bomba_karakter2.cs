using UnityEngine;

public class bomba_karakter2 : MonoBehaviour
{
    [SerializeField] FixedJoystick fixedJoystick;
    [SerializeField] Animator animator;
    Rigidbody2D rb;
    float rotationSpeed = 400f;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

        if (gameObject.tag == "tagger") transform.GetChild(0).gameObject.SetActive(true);
        else transform.GetChild(0).gameObject.SetActive(false);

        if (game_scripts.Instance.stopTime==true) return;



        Vector2 moveDirection = new Vector2(fixedJoystick.Horizontal, fixedJoystick.Vertical);
        if (moveDirection.magnitude > 0.1f) // Agar joystick harakat qilayotgan bo‘lsa
        {
            rb.AddForce(moveDirection * 0.08f * Time.deltaTime);
        }

        RotateCharacter();
    }
    void RotateCharacter()
    {
        // Hareket doğrultusunda döndürme
        if (fixedJoystick.Horizontal != 0 || fixedJoystick.Vertical != 0)
        {
            // Hedef yönü hesapla
            float angle = Mathf.Atan2(fixedJoystick.Vertical,fixedJoystick.Horizontal) * Mathf.Rad2Deg;

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
            if (other.gameObject.tag == "tagger")
            {
                tag = "tagger";
                other.gameObject.tag = "Player";
            }
        }
    }
}
