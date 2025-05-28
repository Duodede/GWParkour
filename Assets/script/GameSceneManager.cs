using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    public Slider loadingValue;
    public Text loadingPercent;
    public float loadingStep;
    string targetSceneName;
    float load;
    public void Start()
    {
        load = 0;
    }
    public void Load(string name)
    {
        targetSceneName = name;
        StartCoroutine(Loading());
    }
    IEnumerator Loading()
    {
        loadingValue.gameObject.SetActive(true);
        while(load < 100)
        {
            if(Random.Range(0,2) == 0)
            {
                load+=loadingStep;
            }
            loadingPercent.text = load.ToString() + "%";
            loadingValue.value = load;
            yield return new WaitForSeconds(0.05f);
        }
        SceneManager.LoadScene(targetSceneName);
        yield break;
    }
}
