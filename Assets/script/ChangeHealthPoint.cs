using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeHealthPoint : MonoBehaviour
{
    public int changedValue;
    public Transform cameraPos;
    PlayerInfo pl;
    public void ChangeHealth()
    {
        pl.ChangeHealthPoint(changedValue);
    }
    public void Start()
    {
        pl = GameObject.FindWithTag("PlayerInfo").GetComponent<PlayerInfo>();
    }
    public void Update()
    {
        this.transform.position = new Vector3(cameraPos.position.x,cameraPos.position.y-100f,0);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            pl.ChangeHealthPoint(changedValue);
        }
    }
    
}
