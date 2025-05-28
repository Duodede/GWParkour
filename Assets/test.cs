using UnityEngine;
using UnityEngine.UI;
public class test : MonoBehaviour
{
    public string s;
    void Start()
    {
        Debug.Log((int.Parse(s)+1).ToString());
    }


}
