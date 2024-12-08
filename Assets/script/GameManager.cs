using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    public bool isEditMode;
    public PlayerInfo playerInfo;
    public GameObject playerPrefab;
    public GameObject player;
    public Transform startPos;
    public Transform editorCameraAim;
    public CinemachineVirtualCamera vcam;
    public float vcamMoveSpeed;
    [Header("ToolsPad")]
    public Animator partsPadAni;
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
        partsPadAni.SetBool("showPad",isEditMode);
    }
    public void StartPlaying()
    {
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
}
public class Manager
{
    
}


