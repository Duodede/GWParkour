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
        text.text = "��������Ҳ��ѧһѧ�ɣ�˵�����պ��õ���";
        TextControl.GetComponent<LearnTextController>().progress = 2;
        TextControl.GetComponent<LearnTextController>().flag = true;
        GameObject.Find("Agree").transform.GetChild(0).GetComponent<Text>().text = "����";
    }
}
