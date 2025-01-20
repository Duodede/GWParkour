using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeHealthPoint : MonoBehaviour
{
    public int changedValue;
    PlayerInfo pl;
    public void ChangeHealth()
    {
        pl.ChangeHealthPoint(changedValue);
    }
    public void Start()
    {
        pl = GameObject.FindWithTag("PlayerInfo").GetComponent<PlayerInfo>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            pl.ChangeHealthPoint(changedValue);
        }
    }
    
}
