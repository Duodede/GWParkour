using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerInfo : MonoBehaviour
{
    public int healthPoint;
    public GameObject deadMenu;
    public Slider slider;
    public Image fill;
    GameManager manager;
    private void Start()
    {
        healthPoint = 100;
        manager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
    }
    private void Update()
    {
        slider.value = healthPoint;
        healthPoint = Mathf.Clamp(healthPoint, 0, 100);//限定范围
        fill.color = Color.Lerp(Color.red,Color.green, healthPoint / 100f);//血条颜色
        IfDie();
    }
    public void ChangeHealthPoint(int changedValue)//改变血量
    {
        healthPoint += changedValue;
    }
    public void IfDie()
    {
        if(healthPoint == 0&& !deadMenu.activeSelf)
        {
            deadMenu.SetActive(true);
            Time.timeScale = 0;
        }
    }
    public void BackToBuildMode()
    {
        healthPoint = 100;
        Time.timeScale = 1f;
        deadMenu.SetActive(false);
        manager.EndPlaying();
    }
    public void ReStart()
    {
        Time.timeScale = 1f;
        healthPoint = 100;
        deadMenu.SetActive(false);
        manager.EndPlaying();
        manager.StartPlaying();
    }
}
