using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public Vector3 prePos;
    public GameObject prePart;
    public GameObject toDeletePart;
    public Transform partParent;
    public GameObject brokenEffect;
    public List<GameObject> builtParts;
    //批量放置
    public enum EditMode
    {
        Single,
        Constant,
    }
    public EditMode editMode;

    GameManager manager;
    Ray2D clickRay;
    RaycastHit2D hitInfo;
    Vector3 intPrePos;
    void Start() 
    {
        manager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
    }
    void Update() 
    {
        Vector3 mousePos = Input.mousePosition;
        prePos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x,mousePos.y,10));
        clickRay = new Ray2D(prePos, Vector2.down);
        intPrePos = new Vector3((int)(prePos.x/2)*2,(int)(prePos.y/2)*2,(int)prePos.z);
        hitInfo = Physics2D.Raycast(clickRay.origin, clickRay.direction,0.5f);
        if(prePart != null)
        {
            prePart.transform.position = intPrePos;
            if(Input.GetButtonDown("Fire1") && !IsUsed(prePart.transform.position))
            {
                Building();
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
    /// <summary>
    /// 创建一个预览对象
    /// </summary>
    /// <param name="prefab"></param>
    public void Build(GameObject prefab)
    {
        prePart = Instantiate(prefab,prePos,transform.rotation,partParent);
    }
    void Building()
    {
        switch(editMode)
        {
            case EditMode.Single://单独放置
                builtParts.Add(prePart);
                prePart = null;
                break;
            case EditMode.Constant://连续放置
                builtParts.Add(prePart);
                prePart = Instantiate(prePart, prePos, transform.rotation, partParent);
                break;
            default:
                break;
        }
    }
    bool IsUsed(Vector3 newPos)
    {
        bool a = false;
        foreach (GameObject part in builtParts)
        {
            if(part.transform.position == newPos)
            {
                a = true;
                break;
            }
        }
        return a;
    }
    /// <summary>
    /// 切换编辑模式
    /// </summary>
    /// <param name="mode"></param>
    public void SwitchEditMode(int num)
    {
        switch (num)
        {
            case 0:
                editMode = EditMode.Single;
                break;
            case 1:
                editMode = EditMode.Constant;
                break;
            default:
                break;
        }
    }
}
