using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checktrigger : MonoBehaviour
{
    public int checkProgress;
    public List<Vector2> pos = new List<Vector2>();
    void Start()
    {
        transform.position = pos[checkProgress];
    }
    void OnTriggerEnter2D()
    {
        Debug.Log("ok");
        GameObject.Find("TextControl").GetComponent<LearnTextController>().progress++;
        checkProgress++;
        transform.position = pos[checkProgress];
    }
}
