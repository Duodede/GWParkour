using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class agreeButton : MonoBehaviour
{
    private GameObject textControl;
    private Text AgreeButtonText;
    private int cnt = 0;
    public List<string> textList = new List<string>();
    void Start()
    {
        textControl = GameObject.Find("TextControl");
        AgreeButtonText = GameObject.Find("Agree").transform.GetChild(0).GetComponent<Text>();
        AgreeButtonText.text = textList[0];
    }
    public void skip()
    {
        if(textControl.GetComponent<LearnTextController>().progress == 23)
        {
            return;
        }
        textControl.GetComponent<LearnTextController>().progress++;
        AgreeButtonText.text = textList[++cnt];
    }
}
