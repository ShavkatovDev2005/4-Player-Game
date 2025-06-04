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
        AsyncOperation scene1 = SceneManager.LoadSceneAsync("Menu&Game", LoadSceneMode.Additive);

        while (!scene1.isDone)
        {
            float progress1 = scene1.progress;
            progressBar.value = Mathf.Clamp01(progress1 / 0.9f);
            yield return null;
        }

        // Yuklash sahna o'chirilishi
        SceneManager.UnloadSceneAsync("loadScene");
    }
}
