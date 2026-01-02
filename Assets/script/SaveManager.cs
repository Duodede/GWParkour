using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEditor;
using System.Diagnostics.Contracts;

public class SaveManager : MonoBehaviour
{
    public List<GameObject> partPrefabs;
    public List<SaveUnit> mapdatas;
    public List<Map> maps;
    public InputField nameInput;
    public string fileName;
    public GameObject wrongTab;
    public Text WrongMessage;
    public BuildManager bm;
    public InputField pauseMenuSaveInputField;
    public string screenshotFolderPath;
    public string fileToDelete;
    public void Start()
    {
        Manager.saveManager = this;
        SaveSystemByJSON.SetFilePath("");
        //创建存档文件夹
        string folderPath = SaveSystemByJSON.filePath+@"\GameData\";
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }
        //创建存档截图文件夹
        screenshotFolderPath = SaveSystemByJSON.filePath + @"\GameData\ScreenshotOfSavedGame";
        if(!Directory.Exists(screenshotFolderPath))
        {
            Directory.CreateDirectory(screenshotFolderPath);
        }
        SaveSystemByJSON.SetFilePath(@"\GameData\");
    }
    public void GetBuilding()//获取存档地图
    {
        mapdatas.Clear();
        for (int i = 0; i < transform.childCount; i++)
        {
            string name = transform.GetChild(i).gameObject.name;
            SaveUnit su = new SaveUnit(name.Replace("(Clone)",""), transform.GetChild(i).position,GetFlipX(transform.GetChild(i).gameObject));//添加地图建筑
            mapdatas.Add(su);
        }
    }
    public void LoadBuiding()//加载存档地图
    {
        foreach (SaveUnit su in mapdatas)
        {
            foreach (GameObject part in partPrefabs)
            {
                if (su.partName == part.name)
                {
                    GameObject newPart = Instantiate(part, new Vector3(su.x, su.y, su.z), transform.rotation, transform);
                    newPart.transform.localScale = new Vector3(su.flipX, 1, 1);
                    bm.builtParts.Add(newPart);
                    break;
                }
            }
        }
    }
    public void UseSave()
    {
        Save(out bool flag);
    }
    public void Save(out bool IsSave)//保存地图
    {
        IsSave = false;
        GetBuilding();
        if (CheatSystem.isTestPadOn)
        {
            fileName = nameInput.text;
        }
        else
        {
            fileName = pauseMenuSaveInputField.text;
        }
        if (fileName == ""||fileName.Contains(" "))
        {
            wrongTab.SetActive(true);
            WrongMessage.text = "存档名不正确";
            return;
        }
        Debug.Log(fileName);
        if (mapdatas.Count == 0)
        {
            wrongTab.SetActive(true);
            WrongMessage.text = "不能保存空文件";
            return;
        }
        foreach (Map map in maps)
        {
            if (fileName == map.name)
            {
                maps.Remove(map);
                break;
            }
        }
        Map currentMap = new Map(fileName, mapdatas,"-1");
        SaveSystemByJSON.SaveDataFromGame<Map>(currentMap,fileName);
        Screenshot.Capture(screenshotFolderPath+"/"+fileName+".png");
        //mapdatas.Clear();
        //maps.Clear();
        IsSave = true;
    }
    public void Load()//加载地图
    {
        if (CheatSystem.isTestPadOn)
        {
            fileName = nameInput.text;
        }
        Map currentMap = null;
        currentMap = SaveSystemByJSON.LoadDataForGame<Map>(fileName);
        foreach (Map map in maps)
        {
            if (fileName == map.name)
            {
                currentMap = map;
                break;
            }
        }
        if (currentMap != null)
        {
            mapdatas = currentMap.datas;
            DeleteBuildings();
            LoadBuiding();
        }
        pauseMenuSaveInputField.text = fileName;
        //mapdatas.Clear();
        //maps.Clear();
        Manager.manager.isFileLoaded = true;
    }
    public void DeleteBuildings()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            bm.builtParts.Remove(transform.GetChild(i).gameObject);
            Destroy(transform.GetChild(i).gameObject);
        }
    }
    public void Delete()
    {
        File.Delete(Path.Combine(SaveSystemByJSON.filePath, fileToDelete+".txt"));
        File.Delete(Path.Combine(screenshotFolderPath, fileToDelete+".png"));
        Manager.manager.ReadAllGameData();
    }
    public void Quit()
    {
        Application.Quit();
    }
    public float GetFlipX(GameObject target)
    {
        return target.transform.localScale.x;
    }
}

[System.Serializable]
public class SaveUnit
{
    public string partName;
    public float x;
    public float y;
    public float z;
    public float flipX;
    public SaveUnit(string pn,Vector3 pos,float flipX)
    {
        partName = pn;
        x = pos.x;
        y = pos.y;
        z = pos.z;
        this.flipX = flipX;
    }
}
[System.Serializable]
public class Map
{
    public string name;
    public string id;
    public List<SaveUnit> datas;
    public Map(string n, List<SaveUnit> d,string i)
    {
        name = n;
        datas = d;
        id = i;
    }
}
