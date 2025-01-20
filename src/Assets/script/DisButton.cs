using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DisButton : MonoBehaviour
{
    private GameObject TextControl;
    private Text text;
    void Start()
    {
        TextControl = GameObject.Find("TextControl");
        text = GameObject.Find("TeachText").GetComponent<Text>();
    }
    public void Onclick()
    {
        text.text = "呃，那你也来学一学吧，说不定日后用得上";
        TextControl.GetComponent<LearnTextController>().progress = 2;
        TextControl.GetComponent<LearnTextController>().flag = true;
        GameObject.Find("Agree").transform.GetChild(0).GetComponent<Text>().text = "跳过";
    }
}
