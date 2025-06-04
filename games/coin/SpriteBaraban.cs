using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class SpriteBaraban : MonoBehaviour
{
    public static SpriteBaraban Instance;
    public SpriteRenderer spriteRenderer; // Sprite-ni ko'rsatish uchun
    public Sprite[] sprites; // Sprite massiv
    public float minAylanishVaqti = 3f; // Minimal aylanish vaqti
    public float maxAylanishVaqti = 6f; // Maksimal aylanish vaqti
    public float spriteKorinadiganVaqt = 0.005f; // Sprite almashtirish tezligi
    public float toxtashKorsatishVaqti = 2f; // To'xtagandan keyin sprite qancha vaqt ko'rinadi
    public int hangi_spritede_durdu;
    private bool davomEttirish = true;
    private bool buttonPressed = false; // Tugma bosilganmi 
    AudioSource audiosource;
    public AudioClip audio1,audio2;

    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        audiosource = GetComponent<AudioSource>();
        hangi_spritede_durdu=-1;
        StartCoroutine(BarabanAylanishi());
    }

    private IEnumerator BarabanAylanishi()
    {
        while (davomEttirish)
        {
            // Tasodifiy aylanish vaqtini tanlash
            float aylanishVaqti = Random.Range(minAylanishVaqti, maxAylanishVaqti);
            float otganVaqt = 0f;
            hangi_spritede_durdu=-1;

            // Barabanni aylantirish
            while (otganVaqt < aylanishVaqti)
            {
                int index = Random.Range(0, sprites.Length);
                spriteRenderer.sprite = sprites[index];
                yield return new WaitForSeconds(spriteKorinadiganVaqt);
                otganVaqt += spriteKorinadiganVaqt;
            }

            // Tasodifiy sprite tanlash va to'xtash
            int randomIndex = Random.Range(0, sprites.Length);
            hangi_spritede_durdu=randomIndex;
            spriteRenderer.sprite = sprites[randomIndex];

            // 5 soniya kutish yoki tugma bosilishini kutish
            float kutishVaqti = 0f;
            buttonPressed = false; // Tugma holatini qayta o'rnatish
            while (kutishVaqti < toxtashKorsatishVaqti)
            {
                if (buttonPressed) // Agar tugma bosilsa, kutishni tugatish
                {
                    break;
                }
                kutishVaqti += Time.deltaTime;
                yield return null;
            }

            // Keyingi aylanishni boshlash uchun davom etish
            yield return null;
        }
    }


    public int al()
    {
        buttonPressed=true;//devametmek icin
        
        int result;
        switch (hangi_spritede_durdu)
        {
            case 0: result = 1; break;
            case 1: result = 3; break;
            case 2: result = 5; break;
            case 3: result = 10; break;
            case 4:
            case 5: result = -10; break;
            default: 
                audiosource.PlayOneShot(audio2);
                return -1;
        }
        audiosource.PlayOneShot(audio1);
        
        hangi_spritede_durdu = -1;
        return result;
    }


    public int forObservations()
    {
        switch (hangi_spritede_durdu)
        {
            case 0: return 1;
            case 1: return 3;
            case 2: return 5;
            case 3: return 10;
            case 4:
            case 5: return -10;
            default: return -1;
        }
    }
}
