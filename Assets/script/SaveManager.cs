using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEditor;

public class SaveManager : MonoBehaviour
{
    public List<GameObject> partPrefabs;
    public List<SaveUnit> mapdatas;
    public List<Map> maps;
    public InputField nameInput;
    public string fileName;
    public BuildManager bm;
    public InputField pauseMenuSaveInputField;
    public void Start()
    {
        SaveSystemByJSON.SetFilePath();
        nameInput.text = "测试地图2";
        fileName = "测试地图2";
        Load();
    }
    public void GetBuilding()//获取存档地图
    {
        mapdatas.Clear();
        for (int i = 0; i < transform.childCount; i++)
        {
            string name = transform.GetChild(i).gameObject.name;
            SaveUnit su = new SaveUnit(name.Remove(name.Length - 7, 7), transform.GetChild(i).position);//添加地图建筑
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
                    bm.builtParts.Add(newPart);
                    break;
                }
            }
        }
    }
    public void Save()//保存地图
    {
        GetBuilding();
        if (CheatSystem.isTestPadOn)
        {
            fileName = nameInput.text;
        }
        else
        {
            fileName = pauseMenuSaveInputField.text;
        }
        foreach (Map map in maps)
        {
            if (fileName == map.name)
            {
                maps.Remove(map);
                break;
            }
        }
        Map currentMap = new Map(fileName, mapdatas);
        maps.Add(currentMap);
        SaveSystemByJSON.SaveDataFromGame<List<Map>>(maps);
        //mapdatas.Clear();
        //maps.Clear();
    }
    public void Load()//加载地图
    {
        maps = SaveSystemByJSON.LoadDataForGame<List<Map>>();
        if (CheatSystem.isTestPadOn)
        {
            fileName = nameInput.text;
        }
        Map currentMap = null;
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
    public void Quit()
    {
        Application.Quit();
    }
}

[System.Serializable]
public class SaveUnit
{
    public string partName;
    public float x;
    public float y;
    public float z;
    public SaveUnit(string pn,Vector3 pos)
    {
        partName = pn;
        x = pos.x;
        y = pos.y;
        z = pos.z;
    }
}
[System.Serializable]
public class Map
{
    public string name;
    public List<SaveUnit> datas;
    public Map(string n, List<SaveUnit> d)
    {
        name = n;
        datas = d;
    }
}