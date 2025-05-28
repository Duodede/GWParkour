using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System.Linq;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    public bool isEditMode;
    public bool isFileLoaded;
    public PlayerInfo playerInfo;
    public GameObject playerPrefab;
    public GameObject player;
    public Transform startPos;
    public Transform editorCameraAim;
    public CinemachineVirtualCamera vcam;
    public float vcamMoveSpeed;
    public SaveManager saveManager;
    [Header("ToolsPad")]
    public Animator partsPadAni;
    [Header("LevelUI")]
    public GameObject playerInfoUI;
    public GameObject startButtonUI;
    private Button startButton;
    [Header("Menu")]
    public GameObject menu;
    public GameObject levelInfoCardPrefab;
    public GameObject content;
    public GameObject deleteConfirmNotice;
    private void Start()
    {
        startButton = startButtonUI.GetComponent<Button>();
        Manager.manager = this;
        StartCoroutine("Load");
        menu.SetActive(true);
    }
    IEnumerator Load()
    {
        yield return new WaitForSeconds(1);
        ReadAllGameData();
    }
    private void Update()
    {
        if(isEditMode)
        {
            //set camera aim
            VCamraMove();
            if(vcam.Follow != editorCameraAim)
            {
                vcam.Follow = editorCameraAim;
                //vcam.LookAt = editorCameraAim;
            }
            startButton.interactable = startPos != null;
        }
        else if(player != null)
        {
            //set camera aim
            if (vcam.Follow != player.transform)
            {
                vcam.Follow = player.transform;
                //vcam.LookAt = player.transform;
            }
        }
        //set tools pad active
        partsPadAni.SetBool("showPad",isEditMode&&isFileLoaded);
    }
    public void StartPlaying()
    {
        if (startPos == null)
            return;
        player = Instantiate(playerPrefab,startPos.position, startPos.rotation);
        playerInfo.ChangeHealthPoint(100);
        isEditMode = false;
    }
    public void EndPlaying()
    {
        isEditMode = true;
        Destroy(player);
    }
    void VCamraMove()
    {
        if(Input.GetButton("Fire2"))
        {
            editorCameraAim.Translate(Vector3.left * Input.GetAxisRaw("Mouse X") * vcamMoveSpeed * Time.deltaTime);
            editorCameraAim.Translate(Vector3.up * -Input.GetAxisRaw("Mouse Y") * vcamMoveSpeed * Time.deltaTime);
        }
    }
    public void SetMenuActive(bool active)
    {
        bool IsSave = false;
        if (active)
        {
            saveManager.Save(out IsSave);
            //saveManager.DeleteBuildings();
        }
        if (!IsSave&&active) return;
        isFileLoaded = !active;
        menu.SetActive(active);
        playerInfoUI.SetActive(!active);
        startButtonUI.SetActive(!active);
        ReadAllGameData();
    }
    public void ReadAllGameData()
    {
        for (int i = 0; i < content.transform.childCount; i++)
        {
            Destroy(content.transform.GetChild(i).gameObject);
        }
        string path = Directory.GetCurrentDirectory()+@"/GameData";
        string[] files = Directory.GetFiles(path, "*.txt", SearchOption.TopDirectoryOnly);
        string[] textures = Directory.GetFiles(path + "/ScreenshotOfSavedGame", "*.png", SearchOption.TopDirectoryOnly);
        foreach(string filePath in files)
        {
            LevelInfoCard card = Instantiate(levelInfoCardPrefab, content.transform).GetComponent<LevelInfoCard>();
            foreach(string texturePath in textures)
            {
                if (texturePath.Contains(Path.GetFileNameWithoutExtension(filePath)))
                {
                    var bytes = File.ReadAllBytes(texturePath);
                    Texture2D texture2D = new Texture2D(Screenshot.manager.rt.width, Screenshot.manager.rt.height);
                    texture2D.LoadImage(bytes);
                    card.image.texture = texture2D;
                    break;
                }
            }
            card.content = content.transform;
            card.levelName = Path.GetFileNameWithoutExtension(filePath);
            card.levelNameText.text = card.levelName;
        }
    }
    public void changeScene(string name)
    {
        SceneManager.LoadScene(name);
    }
}

public class Manager
{
    public static GameManager manager;
    public static SaveManager saveManager;
}


