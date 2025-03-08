using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class ScreenshotManager : MonoBehaviour
{
    public RenderTexture rt;
    public Camera cam;
    private Texture2D m_Texture;
    public void Start()
    {
        Screenshot.manager = this;
        cam.backgroundColor = Camera.main.backgroundColor;
    }
    /// <summary>
    /// 截图并保存到指定路径
    /// </summary>
    /// <param name="path"></param>
    public void Capture(string path)
    {
        cam.gameObject.SetActive(true);
        cam.transform.position = Camera.main.transform.position;
        RenderTexture.active = rt;
        m_Texture = new Texture2D(rt.width,rt.height,TextureFormat.RGBA32,false);
        m_Texture.ReadPixels(new Rect(0, 0, rt.width, rt.height), 0, 0);
        var bytes = ImageConversion.EncodeToPNG(m_Texture);
        File.WriteAllBytes(path, bytes);
        cam.gameObject.SetActive(false);
    }
}
public class Screenshot
{
    public static ScreenshotManager manager;
    public static void Capture(string path)
    {
        manager.Capture(path);
    }
}