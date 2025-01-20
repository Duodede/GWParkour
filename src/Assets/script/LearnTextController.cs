using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LearnTextController : MonoBehaviour
{
    public List<string> textList = new List<string>();
    private GameObject agreeButton;
    private GameObject disButton;
    public Text text;
    public int progress;
    public GameObject panelOne;
    public GameObject panelTwo;
    public GameObject panelThree;
    public SaveManager saveManager;
    public bool flag;
    void Start()
    {
        agreeButton = GameObject.Find("Agree");
        disButton = GameObject.Find("DisAgree");
        disButton.SetActive(false);
        panelTwo.SetActive(false);
    }
    public void toPlay()
    {
        switch (progress)
        {
            case 12:
                progress++;
                text.text = textList[progress];
                break;
            case 17:
                progress++;
                text.text = textList[progress];
                break;
            default:
                break;
        }
    }
    public void Update()
    {
        switch(progress)
        {
            case 1:
                disButton.SetActive(true);
                break;
            case 2:
                disButton.SetActive(false);
                break;
            case 3:
                flag = false;
                break;
            case 6:
                panelOne.SetActive(false);
                break;
            case 8:
                panelOne.SetActive(true);
                break;
            case 10:
                saveManager.nameInput.text = "Teach";
                saveManager.Load();
                break;
            case 12:
                panelThree.SetActive(false);
                agreeButton.SetActive(false);
                break;
            case 13:
                Debug.Log(Input.GetAxis("Horizontal"));
                if (Input.GetAxis("Horizontal") > 0) progress++;
                break;
            case 14:
                if (Input.GetAxis("Horizontal") < 0) progress++;
                break;
            case 17:
                panelThree.SetActive(false);
                panelOne.SetActive(false);
                break;
            case 19:
                agreeButton.SetActive(true);
                break;
            case 21:
                agreeButton.SetActive(false);
                break;
            case 23:
                agreeButton.SetActive(true);
                break;
            default:
                break;  

        }
        if(!flag)text.text = textList[progress];
    }
}
