using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEditor;

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

[System.Serializable]
public class SaveUnit
{
    public string partName;
    public float x;
    public float y;
    public float z;
    public SaveUnit(string pn, Vector3 pos)
    {
        partName = pn;
        x = pos.x;
        y = pos.y;
        z = pos.z;
    }
}

public class SaveManager : MonoBehaviour
{
    public List<GameObject> partPrefabs;
    public List<SaveUnit> mapdatas;
    public List<Map> maps;
    public InputField nameInput;
    public string fileName;
    public BuildManager bm;
    public void Start()
    {
        SaveSystemByJSON.SetFilePath();
        nameInput.text = "���Ե�ͼ1";
        Load();
    }
    public void GetBuilding()
    {
        mapdatas.Clear();
        for (int i = 0; i < transform.childCount; i++)
        {
            string name = transform.GetChild(i).gameObject.name;
            SaveUnit su = new SaveUnit(name.Remove(name.Length-7,7), transform.GetChild(i).position);//��ӵ�ͼ����
            mapdatas.Add(su);
        }
    }
    public void LoadBuiding()//���ش浵��ͼ
    {
        foreach(SaveUnit su in mapdatas)
        {
            foreach (GameObject part in partPrefabs)
            {
                if(su.partName == part.name)
                {
                    GameObject newPart = Instantiate(part,new Vector3(su.x,su.y,su.z), transform.rotation, transform);
                    bm.builtParts.Add(newPart);
                    break;
                }
            }
        }
    }
    public void Save()//�����ͼ
    {
        GetBuilding();
        fileName = nameInput.text;
        foreach (Map map in maps)
        {
            if(fileName == map.name)
            {
                maps.Remove(map);
                break;
            }
        }
        Map currentMap = new Map(fileName, mapdatas);
        maps.Add(currentMap);
        SaveSystemByJSON.SaveDataFromGame<List<Map>>(maps);
        mapdatas.Clear();
        maps.Clear();
    }
    public void Load()//���ص�ͼ
    {
        maps = SaveSystemByJSON.LoadDataForGame<List<Map>>();
        fileName = nameInput.text;
        Map currentMap = null;
        foreach (Map map in maps)
        {
            if (fileName == map.name)
            {
                currentMap = map;
                break;
            }
        }
        if(currentMap != null)
        {
            mapdatas = currentMap.datas;
            for (int i = 0; i < transform.childCount; i++)
            {
                bm.builtParts.Remove(transform.GetChild(i).gameObject);
                Destroy(transform.GetChild(i).gameObject);
            }
            LoadBuiding();
        }
        mapdatas.Clear();
        maps.Clear();
    }
    public void Quit()
    {
        Application.Quit();
    }
}