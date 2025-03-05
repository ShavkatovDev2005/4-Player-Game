using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class loading_script : MonoBehaviour
{
    public Slider progressBar; // Yuklanish progressi uchun Slider

    void Start()
    {
        StartCoroutine(LoadAllScenes());
    }

    IEnumerator LoadAllScenes()
    {
        // Ikkala sahnani yuklashni boshlash
        AsyncOperation scene2 = SceneManager.LoadSceneAsync("menu", LoadSceneMode.Additive);

        // Sahna yuklanayotgan vaqtda kutish
        while (!scene2.isDone)
        {
            float progress = scene2.progress;
            progressBar.value = Mathf.Clamp01(progress / 0.9f); // 0.9 dan keyin to'liq yuklangan hisoblanadi
            yield return null;
        }

        // Asosiy sahnalarga o‘tgandan keyin yuklash sahnasini yopish
        SceneManager.UnloadSceneAsync("loadScene");
    }
}
