using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//行走效果
public class WalkEffect : MonoBehaviour
{
    public GameObject effect;//效果
    public float height;//高度
    public float strength;//粒子效果强度
    ParticleSystem particle;//粒子
    private void Start()
    {
        //对粒子效果做初始化，组件，加初始状态
        particle = effect.GetComponent<ParticleSystem>();
        particle.Stop();
    }
    private void OnCollisionStay2D(Collision2D collision)//进入碰撞
    {
        if(collision.gameObject.TryGetComponent<Rigidbody2D>(out Rigidbody2D rg))//如果进入的是刚体
        {
            //Debug.Log(collision.gameObject.tag);
            if(rg.velocity.magnitude != 0)//速度不为零
            {
                effect.active = true;//开始效果
                effect.transform.position = new Vector3(rg.transform.position.x, this.transform.position.y + height, 0);//粒子发生的的位置（rg底下，发生的高度+原本的高度）
                particle.emissionRate = strength* (rg.velocity.magnitude/10f);//强度*速度的0.1倍
            }
            else
            {
                particle.Stop();//速度为零停止发生
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Rigidbody2D>(out Rigidbody2D rg))
        {
            particle.Stop();//退出时停止发生
        }
    }
    //为什么有了这个还要加变量trigger
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Rigidbody2D>(out Rigidbody2D rg))
        {
            //Debug.Log(collision.gameObject.tag);
            if (rg.velocity.magnitude != 0)
            {
                effect.active = true;
                effect.transform.position = new Vector3(rg.transform.position.x, this.transform.position.y + height, 0);
                particle.emissionRate = strength * (rg.velocity.magnitude / 10f);
            }
            else
            {
                particle.Stop();
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Rigidbody2D>(out Rigidbody2D rg))
        {
            particle.Stop();
        }
    }
}
