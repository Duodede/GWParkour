using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class node
{
    public Vector2 pos;
    public string name;
    public node(Vector2 p, string n)
    {
        pos = p;
        name = n;
    }
}
public class checktrigger : MonoBehaviour
{
    public int checkpos = 0;
    public List<node> checkList = new List<node>();
}
