using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class TerrainDrag : MonoBehaviour
{
    public bool trigger;
    public float factor;
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerMove>(out PlayerMove rg) && !trigger)
        {
            Drag(rg);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerMove>(out PlayerMove rg) && !trigger)
        {
            Recover(rg);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerMove>(out PlayerMove rg) && trigger)
        {
            Drag(rg);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerMove>(out PlayerMove rg) && trigger)
        {
            Recover(rg);
        }
    }
    private void Drag(PlayerMove pl)//降低玩家速度的函数
    {
        pl.factor = factor;
    }
    private void Recover(PlayerMove pl)//降低玩家速度的函数
    {
        pl.factor = 1f;
    }
}
