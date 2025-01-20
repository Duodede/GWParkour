using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//为什么不写注释啊
public class BuildManager : MonoBehaviour
{
    public Vector3 prePos;//绑定于鼠标位置
    public GameObject prePart;//当前选取的放置物体
    public GameObject toDeletePart;//
    public Transform partParent;//所处坐标系
    public GameObject brokenEffect;//粒子
    public List<GameObject> builtParts;//所有放置
    //批量放置
    public Vector3 startPos;

    GameManager manager;
    Ray2D clickRay;//射线
    RaycastHit2D hitInfo;//射线检测
    Vector3 intPrePos;
    void Start() 
    {
        manager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
    }
    void Update() 
    {
        Vector3 mousePos = Input.mousePosition;//获取鼠标位置
        prePos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x,mousePos.y,10));//屏幕世界坐标系转换
        clickRay = new Ray2D(prePos, Vector2.down);//向下的检测
        intPrePos = new Vector3((int)(prePos.x/2)*2,(int)(prePos.y/2)*2,(int)prePos.z);//去精度
        hitInfo = Physics2D.Raycast(clickRay.origin, clickRay.direction,0.5f);
        if(prePart != null)
        {
            prePart.transform.position = intPrePos;//定下了位置
            if(Input.GetButtonDown("Fire1") && !IsUsed(prePart.transform.position))
            {
                builtParts.Add(prePart);
                prePart = null;
            }
            else if(Input.GetButtonDown("Fire2") || (IsUsed(prePart.transform.position)&&Input.GetButtonDown("Fire1")) )
            {
                Destroy(prePart);
                prePart = null;
            }
        }
        else
        {
            if(Input.GetButtonDown("Fire2")&&hitInfo.collider != null && manager.isEditMode)
            {
                if(hitInfo.collider.gameObject.tag == "Part")
                {
                    toDeletePart = hitInfo.collider.gameObject;
                    builtParts.Remove(toDeletePart);
                    Instantiate(brokenEffect,toDeletePart.transform.position,this.transform.rotation);
                    Destroy(toDeletePart);
                    toDeletePart = null;
                }
            }
        }
    }
    public void Build(GameObject prefab)
    {
        prePart = Instantiate(prefab,prePos,transform.rotation,partParent);
    }
    bool IsUsed(Vector3 newPos)//格子被占用
    {
        foreach (GameObject part in builtParts)
        {
            if(part.transform.position == newPos)
            {
                return true;
            }
        }
        return false;
    }
}
