using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.PlayerLoop;

public class AIMove : MonoBehaviour
{
    public List<string> moveableBrickTypes;
    public float moveSpeed;
    public Vector2 movement;
    public int state;
    [Header("preference settings")]
    public bool hangAround;
    public float findRateByDistance;

    RaycastHit2D ray;
    Rigidbody2D rb;
    float waitTime;
    private void Start()
    {
        TryGetComponent<Rigidbody2D>(out rb);
        waitTime = findRateByDistance / moveSpeed;//
        StartCoroutine(FindWay());
    }
    ///<summary>
    /// 检测是否有可行方块
    /// </summary>
    private bool IfBricksMoveable(Vector2 dir)
    {
        bool able;
        string brick = GetEnvironment(dir);
        able = moveableBrickTypes.Contains(brick);
        return able;
    }
    private string GetEnvironment(Vector2 dir)
    {
        string tag;
        //射线检测左右方块
        ray = Physics2D.Raycast(new Vector3(transform.position.x, transform.position.y-1.5f,0), dir,2);
        //返回读取结果
        tag = ray.collider?.gameObject.name;
        //Debug.Log(tag);
        return tag;
    }
    IEnumerator Move(Vector2 dir)
    {
        rb.velocity = new Vector2(dir.x * moveSpeed, rb.velocity.y);
        if(dir.x == 1)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        yield break;
    }
    /// <summary>
    /// 寻路
    /// </summary>
    /// <returns></returns>
    IEnumerator FindWay()
    {
        while(hangAround)
        {
            yield return new WaitForSeconds(waitTime);
            if(IfBricksMoveable(Vector2.left)&& IfBricksMoveable(-Vector2.left))
            {
                movement = new Vector2(Random.Range(-1, 2), 0);
            }
            else if(IfBricksMoveable(Vector2.left))
            {
            movement = new Vector2(-1, 0);
            }
            else if (IfBricksMoveable(-Vector2.left))
            {
                movement = new Vector2(1, 0);
            }
            else
            {
                movement = new Vector2(0, 0);
            }
        }
    }
    private void HangAround()
    {
        StartCoroutine(Move(movement));
    }
    private void Update()
    {
        HangAround();
    }

}
