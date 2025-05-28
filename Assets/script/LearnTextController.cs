using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
public class rayCheck
{
    public Vector2 pos;
    public string name;
    public rayCheck(Vector2 p, string n)
    {
        pos = p;
        name = n;
    }
}
public class LearnTextController : MonoBehaviour
{
    public List<string> textList = new List<string>();
    public List<string> abtextList = new List<string>();
    public List<rayCheck> checkList = new List<rayCheck>(); 
    public GameObject agreeButton;
    public GameObject disButton;
    public GameObject tipsSprite;
    public Text AgreeButtonText;
    public Text text;
    public int progress;
    public int AbutProgress;
    public int BbutProgress;
    public int rayProgress;
    public GameObject panelOne;
    public GameObject panelTwo;
    public bool flag;
    public RaycastHit2D hit;
    public SaveManager saveManager;
    public bool check(Vector2 pos,string name)
    {
        Debug.DrawRay(pos, Vector3.forward, Color.red);
        hit = Physics2D.Raycast(pos, Vector3.forward, 100f);
        Debug.Log(hit.collider.name);
        if (hit.collider != null && hit.collider.name == name)
        {
            return true;
        }
        return false;
    }
    public void BChoice()
    {
        progress++;
        flag = true;
        text.text = "呃……那你也来试试吧";
        AgreeButtonText.text = abtextList[++AbutProgress];
    }
    public void skip()
    {
        if (progress == 20) return;
        if(progress == 22)
        {
            SceneManager.LoadScene("StartScene");
        }
        progress++;
        AgreeButtonText.text = abtextList[++AbutProgress];
    }
    public void Update()
    {
        switch (progress)
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
            case 5:
                panelOne.SetActive(false);
                agreeButton.SetActive(false);
                tipsSprite.SetActive(true);
                if(check(checkList[rayProgress].pos, checkList[rayProgress].name))
                {
                    progress++;
                    rayProgress++;
                    tipsSprite.transform.position = new Vector3(checkList[rayProgress].pos.x, checkList[rayProgress].pos.y,-5);
                }
                break;
            case 6:
                if (check(checkList[rayProgress].pos, checkList[rayProgress].name))
                {
                    progress++;
                    rayProgress++;
                    tipsSprite.transform.position = new Vector3(checkList[rayProgress].pos.x, checkList[rayProgress].pos.y, -5);
                    tipsSprite.SetActive(false);
                }
                break;
            case 7:
                panelOne.SetActive(true);
                agreeButton.SetActive(true);
                break;
            case 8:
                panelOne.SetActive(true);
                break;
            case 10:
                saveManager.fileName = "教学关卡";
                saveManager.Load();
                break;
            case 11:
                agreeButton.SetActive(false);  
                panelTwo.SetActive(false);
                break;
            case 12:
                panelTwo.SetActive(true);
                if (Input.GetAxis("Horizontal") > 0) progress++;
                break;
            case 13:
                if (Input.GetAxis("Horizontal") < 0) progress++;
                tipsSprite.SetActive(true);
                break;
            case 16:
                panelTwo.SetActive(false);
                panelOne.SetActive(false);
                break;
            case 17:
                panelOne.SetActive(true);
                panelTwo.SetActive(true);
                break;
            case 18:
                agreeButton.SetActive(true);
                break;
            case 20:
                agreeButton.SetActive(false);
                panelTwo.SetActive(false);
                panelOne.SetActive(false);
                break;
            case 21:
                panelTwo.SetActive(true);
                panelOne.SetActive(true);
                break;
            case 22:
                agreeButton.SetActive(true);
                AgreeButtonText.text = "回到主界面";
                break;
            default:
                break;  

        }
        if(!flag)text.text = textList[progress];
    }
}
