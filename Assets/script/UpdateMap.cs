using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Networking;
public class UpdateMap : MonoBehaviour
{
    public string fileName;
    private string bucketDM = "https://lazybones-1339804190.cos.ap-guangzhou.myqcloud.com/";
    public void download()
    {
        StartCoroutine("Download", fileName);
    }
    IEnumerator Download(string fileName)
    {
        string formPath = "https://lazybones-1339804190.cos.ap-guangzhou.myqcloud.com/" + fileName;
        UnityWebRequest request = UnityWebRequest.Get(formPath);
        request.timeout = 5;
        yield return request.SendWebRequest();
        if (request.isDone)
        {
            if (request.error == null)
            {
                Debug.Log(request.downloadHandler.text);
            }
            else
            {
                Debug.Log(request.error);
            }
        }
    }
    public void update()
    {
        StartCoroutine("updatet","updtest.txt");
    }
    IEnumerator updatet(string fileName)
    {
        byte[] data = System.Text.Encoding.UTF8.GetBytes("If you can see this sentence,you are succeed in update! ");
        UnityWebRequest request = UnityWebRequest.Put("https://lazybones-1339804190.cos.ap-guangzhou.myqcloud.com/"+fileName, data);
        yield return request.SendWebRequest();
        if (request.isDone)
        {
            if (request.error == null)
            {
                Debug.Log("If you can see this sentence,you are succeed in update!");
            }
            else
            {
                Debug.Log(request.error);
            }
        }
    }
}
