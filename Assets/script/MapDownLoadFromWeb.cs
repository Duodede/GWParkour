using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System;
using UnityEngine.Networking;
using Newtonsoft.Json;

public class MapDownLoadFromWeb : MonoBehaviour
{
    public GameObject warning;
    private const string bucketDM = "https://lazybones-1339804190.cos.ap-guangzhou.myqcloud.com/";
    public void DownLoad(Text Id)
    {
        StartCoroutine(AllDownLoadStep(Directory.GetCurrentDirectory() + @"\GameData\", Id.text));
    }
    IEnumerator AllDownLoadStep(string SavePath, string Id)
    {
        yield return StartCoroutine(Test(bucketDM + "test.txt", (ok) =>
        {
            if (!ok)
            {
                warning.SetActive(true);
                warning.transform.GetChild(1).GetComponent<Text>().text = "ÏÂÔØÊ§°Ü";
            }
        }));
        if (warning.activeSelf == true)
        {
            yield break;
        }
        Map dlMap = new Map(null, null, null);
        Texture2D pic = null;
        string MapData = null;
        byte[] TextureBytes = null;
        yield return StartCoroutine(GetPic(bucketDM + "Image/MID" + Id + ".png", (callbackPic) =>
        {
            pic = callbackPic;
            TextureBytes = pic.EncodeToPNG();
        }));
        yield return StartCoroutine(GetMap(bucketDM + "Text/MID" + Id + ".txt", (callbackMap) =>
        {
            dlMap = JsonConvert.DeserializeObject<Map>(callbackMap);
            MapData = callbackMap;
        }));
        File.WriteAllBytes(SavePath + @"\ScreenshotOfSavedGame\" + dlMap.name + ".png", TextureBytes);
        File.WriteAllText(SavePath + dlMap.name + ".txt", MapData);
    }
    IEnumerator GetPic(string picPath, Action<Texture2D> callbackPic)
    {
        Texture2D pic;
        pic = null;
        UnityWebRequest PIC = UnityWebRequestTexture.GetTexture(picPath);
        PIC.timeout = 5;
        yield return PIC.SendWebRequest();
        if (PIC.isDone)
        {
            if (PIC.error == null)
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
    IEnumerator Test(string TestPath, Action<bool> test)
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
}
