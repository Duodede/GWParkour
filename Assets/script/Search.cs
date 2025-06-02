using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Search : MonoBehaviour
{
    [System.Serializable]
    public class Map
    {
        public string name;
        public List<SaveUnit> datas;
        public long mapid;
        public Map(string n, List<SaveUnit> d, long id)
        {
            name = n;
            datas = d;
            mapid = id;
        }
    }
    public InputField searchText;
    public List<Map> maps;
    public Text warning;
    private long num;
    long ToLong(string n)
    {
        num = 0;
        for(int i=0; i<n.Length; i++)
        {
            num = num * 10 + (n[i] - '0');
        }
        return num;
    }
    void Onclick()
    {
        //maps = SaveSystemByJSON.LoadDataForGame<List<Map>>();
        if (searchText != null)
        {
            foreach(Map map in maps)
            {
                if(map.mapid == ToLong(searchText.text))
                {
                    Debug.Log("找到了");
                    return;
                }
            }
            warning.text = "ID不存在";
        }
    }
}
