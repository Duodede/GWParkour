using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System;
using UnityEngine.Networking;
using Newtonsoft.Json;

public class WebMaps : MonoBehaviour
{
    public GameObject menu;
    public GameObject content;//上传页面
    public GameObject uptLevelInfoCardPrefab;//上传页面信息卡
    public GameObject mainContent;//推荐页面
    public GameObject mainLevelInfoCardPrefab;//推荐页面信息卡
    public RenderTexture rt;
    public GameObject warning;
    public GameObject panel;
    private bool state;//为0在推荐，为1在搜索
    private string Id;
    private const string bucketDM = "https://lazybones-1339804190.cos.ap-guangzhou.myqcloud.com/";
    private Map uptMap = new Map(null,null,null);
    private void Start()
    {
        MapLoad();
        SaveSystemByJSON.SetFilePath("/GameData/");
    }
    public void back()
    {
        if (state == false)
        {
            SceneManager.LoadScene("SampleScene");
        }
        else if(state == true)
        {
            StartCoroutine(AllLoadStep());
            state = false;
        }
    }
    public void search(Text id)
    {
        if (id.text!="")
        {
            StartCoroutine(Load(id.text, true));
            state = true;
        }
    }
    public void MapLoad()
    {
        StartCoroutine(AllLoadStep());
    }
    IEnumerator AllLoadStep()
    {
        yield return StartCoroutine(Test(bucketDM + "test.txt", (ok) =>
        {
            if (!ok)
            {
                warning.SetActive(true);
                warning.transform.GetChild(1).GetComponent<Text>().text = "加载失败";
            }
        }));
        if (warning.activeSelf == true) yield break;
        yield return StartCoroutine(GetId(bucketDM + "id.txt"));
        StartCoroutine(Load(Id,false));
    }
    IEnumerator GetId(string UptPath)
    {
        UnityWebRequest ID = UnityWebRequest.Get(UptPath);
        ID.timeout = 5;
        yield return ID.SendWebRequest();
        if (ID.isDone)
        {
            if (ID.error != null)
            {
                Debug.LogError(ID.error);
            }
            else
            {
                Id = ID.downloadHandler.text;
            }
        }
    }
    IEnumerator Load(string Id,bool type)//type == 1是单次
    {
        int start = 1;
        if (type) start = int.Parse(Id);
        for (int i = 0; i < mainContent.transform.childCount; i++)
        {
            Destroy(mainContent.transform.GetChild(i).gameObject);
        }
        for (int i = start; i<= int.Parse(Id); i++)
        {
            Map dlMap = new Map(null, null, null);
            Texture2D pic = null;
            yield return StartCoroutine(GetPic(bucketDM + "Image/MID" + i + ".png", (callbackPic) => 
            { 
                pic = callbackPic;
            }));
            yield return StartCoroutine(GetMap(bucketDM + "Text/MID" + i + ".txt", (callbackMap) => 
            {
                dlMap = JsonConvert.DeserializeObject<Map>(callbackMap);
            }));
            Debug.Log(dlMap.name);
            GameObject InfoCard = Instantiate(mainLevelInfoCardPrefab, mainContent.transform);
            InfoCard.GetComponent<MapDownLoadFromWeb>().warning = warning;
            LevelInfoCard cardInfo = InfoCard.GetComponent<LevelInfoCard>();
            cardInfo.levelNameText.text = dlMap.name;
            cardInfo.image.texture = pic;
            cardInfo.Id.text = dlMap.id;
        }
        yield return null;
    }
    IEnumerator GetPic(string picPath, Action<Texture2D> callbackPic)
    {
        Texture2D pic;
        pic = null;
        UnityWebRequest PIC = UnityWebRequestTexture.GetTexture(picPath);
        PIC.timeout = 5;
        yield return PIC.SendWebRequest();
        if(PIC.isDone)
        {
            if(PIC.error == null)
            {
                pic = DownloadHandlerTexture.GetContent(PIC);
                callbackPic(pic);
            }
        }
    }
    IEnumerator GetMap(string MapPath, Action<string> callbackMap)
    {
        string map;
        map = null;
        UnityWebRequest MAP = UnityWebRequest.Get(MapPath);
        MAP.timeout = 5;
        yield return MAP.SendWebRequest();
        if (MAP.isDone)
        {
            if (MAP.error == null)
            {
                map = MAP.downloadHandler.text;
                callbackMap(map);
            }
        }
    }
    public void ReadAllGameData()
    {
        menu.SetActive(true);
        for (int i = 0; i < content.transform.childCount; i++)
        {
            Destroy(content.transform.GetChild(i).gameObject);
        }
        string path = Directory.GetCurrentDirectory() + @"/GameData";
        string[] files = Directory.GetFiles(path, "*.txt", SearchOption.TopDirectoryOnly);
        string[] textures = Directory.GetFiles(path + "/ScreenshotOfSavedGame", "*.png", SearchOption.TopDirectoryOnly);
        foreach (string filePath in files)
        {
            GameObject card = Instantiate(uptLevelInfoCardPrefab, content.transform);
            LevelInfoCard cardInfo = card.GetComponent<LevelInfoCard>();
            WebMaps cardWM = card.GetComponent<WebMaps>();
            foreach (string texturePath in textures)
            {
                if (texturePath.Contains(Path.GetFileNameWithoutExtension(filePath)))
                {
                    var bytes = File.ReadAllBytes(texturePath);
                    Texture2D texture2D = new Texture2D(Screenshot.manager.rt.width, Screenshot.manager.rt.height);
                    texture2D.LoadImage(bytes);
                    cardInfo.image.texture = texture2D;
                    cardWM.warning = warning;
                    cardWM.panel = warning.transform.GetChild(0).gameObject;
                    break;
                }
            }
            cardInfo.content = content.transform;
            cardInfo.levelName = Path.GetFileNameWithoutExtension(filePath);
            cardInfo.levelNameText.text = cardInfo.levelName;
        }
    }
    public void UptMap(Text text)
    {
        string name = text.text;
        string txtPath = "";
        string picPath = "";
        string path = Directory.GetCurrentDirectory() + @"/GameData";
        string[] files = Directory.GetFiles(path, "*.txt", SearchOption.TopDirectoryOnly);
        string[] textures = Directory.GetFiles(path + "/ScreenshotOfSavedGame", "*.png", SearchOption.TopDirectoryOnly);
        foreach (string filePath in files)
        {
            if(filePath == Path.Combine(path,name+".txt"))
            {
                txtPath = Path.Combine(path, name + ".txt");
                break;
            }
        }
        foreach(string filePath in textures)
        {
            if (filePath == Path.Combine(path + "/ScreenshotOfSavedGame", name + ".png"))
            {
                picPath = Path.Combine(path + "/ScreenshotOfSavedGame", name + ".png");
                break;
            }
        }
        StartCoroutine(FullUpt(picPath, txtPath, name));//整个上传的携程
        panel.SetActive(true);
    }
    IEnumerator FullUpt(string picPath, string txtPath, string fileName)
    {
        yield return StartCoroutine(Test(bucketDM + "test.txt", (ok) =>
        {
            if(!ok)
            {
                warning.SetActive(true);
                warning.transform.GetChild(1).GetComponent<Text>().text = "上传失败";
            }
        }));
        if (warning.activeSelf == true) yield break;
        uptMap = SaveSystemByJSON.LoadDataForGame<Map>(fileName);
        string tId = uptMap.id;
        if (tId != "-1")
        {
            Id = tId;
        }
        else
        {
            yield return StartCoroutine(GetAndSaveId(bucketDM + "id.txt", fileName));//获取ID
            StartCoroutine(UptId(bucketDM + "id.txt", Id));//上传ID
        }
        StartCoroutine(UptPic(bucketDM + "Image/MID" + Id + ".png", picPath));//上传图片
        StartCoroutine(UptTxt(bucketDM + "Text/MID" + Id + ".txt", File.ReadAllText(txtPath)));//上传地图文本
    }
    IEnumerator Test(string TestPath,Action<bool> test)
    {
        UnityWebRequest TEST = UnityWebRequest.Get(TestPath);
        TEST.timeout = 5;
        yield return TEST.SendWebRequest();
        if (TEST.isDone)
        {
            if (TEST.error != null)
            {
                Debug.LogError(TEST.error);
                test(false);
            }
            else
            {
                test(true);
            }
        }
    }
    IEnumerator GetAndSaveId(string UptPath,string fileName)
    {
        UnityWebRequest ID = UnityWebRequest.Get(UptPath);
        ID.timeout = 5;
        yield return ID.SendWebRequest();
        if(ID.isDone)
        {
            if (ID.error != null)
            {
                Debug.LogError(ID.error);
            }
            else
            {
                Id = (int.Parse(ID.downloadHandler.text) + 1).ToString();
                uptMap.id = Id;
                SaveSystemByJSON.SaveDataFromGame<Map>(uptMap, fileName);
                Debug.Log(Id);
            }
        }
    }
    IEnumerator UptId(string UptPath, string Id)
    {
        UnityWebRequest ID = UnityWebRequest.Put(UptPath, Id);
        ID.timeout = 5;
        yield return ID.SendWebRequest();
        if (ID.isDone)
        {
            if (ID.error != null)
            {
                Debug.LogError(ID.error);
            }
            else
            {
                Debug.Log("Ok");
            }
        }
    }
    IEnumerator UptPic(string UptPath,string filePath)
    {
        UnityWebRequest PIC = new UnityWebRequest(UptPath, "PUT");//设置类型
        PIC.uploadHandler = new UploadHandlerRaw(File.ReadAllBytes(filePath));//数据
        PIC.downloadHandler = new DownloadHandlerBuffer();//服务器回传
        PIC.timeout = 5;
        yield return PIC.SendWebRequest();
        if (PIC.isDone)
        {
            if (PIC.error != null)
            {
                Debug.LogError(PIC.error);
            }
            else
            {
                Debug.Log("Ok");
            }
        }
    }
    IEnumerator UptTxt(string UptPath, string filePath)
    {
        UnityWebRequest TXT = UnityWebRequest.Put(UptPath, filePath);
        TXT.timeout = 5;
        yield return TXT.SendWebRequest();
        if (TXT.isDone)
        {
            if (TXT.error != null)
            {
                Debug.LogError(TXT.error);
            }
            else
            {
                Debug.Log("Ok");
                panel.SetActive(false);
                MapLoad();
            }
        }
    }
}

