using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;

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
    private void Start()
    {
        startButton = startButtonUI.GetComponent<Button>();
        Manager.manager = this;
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
        isFileLoaded = !active;
        menu.SetActive(active);
        playerInfoUI.SetActive(!active);
        startButtonUI.SetActive(!active);
        if(active)
        {
            saveManager.Save();
            saveManager.DeleteBuildings();
        }
    }
}
public class Manager
{
    public static GameManager manager;
}


